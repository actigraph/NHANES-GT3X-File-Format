using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Crc;
using Ionic.Zip;

namespace GT3X.Parsing.Library
{
    /// <summary>  The Gt3XFile type represents an ActiGraph .gt3x file. </summary>
    /// <remarks>
    ///     This is the main type in the GT3X.Parsing.Library class library. This class
    ///     parses activity and lux data from ActiGraph .gt3x files. This is specifically
    ///     designed for files used in NHANES data collection.
    /// </remarks>
    public class Gt3XFile
    {
        /// <summary> The types of ActiGraph devices used in collecting data. </summary>
        public enum DeviceType
        {
            Unknown = 0,
            GT3XPlus,
            ActiSleepPlus,
        };

        /// <summary> Creates a new Gt3xFile instance, using the specified filename. </summary>
        /// <param name="fileName">The filename to parse.</param>
        /// <exception cref="FileDoesNotExist">Thrown if the fileName doesn't exist.</exception>
        /// <exception cref="FileIsNotAZipFile">Thrown if the file is not a valid zip (archive).</exception>
        /// <exception cref="MissingRequiredFiles">Thrown if the zip file is missing required files.</exception>
        /// <exception cref="InvalidInfoFile">Thrown if the info.txt file in the zip file is missing required information.</exception>
        public Gt3XFile(string fileName)
        {
            FileName = fileName;

            if (string.IsNullOrEmpty(FileName) || !File.Exists(FileName))
                throw new FileDoesNotExist("You must pass in a valid filename.");

            if (!ZipFile.IsZipFile(FileName))
                throw new FileIsNotAZipFile("You must pass in a zip file.", FileName);

            ValidateAndParseZipFile();
        }

        /// <summary> The fileName of the Gt3x file. </summary>
        public string FileName { get; private set; }

        /// <summary> Sample rate of the data in Hz. </summary>
        public int SampleRateInHz { get; private set; }

        /// <summary> The date/time of the first sample recorded by the device. </summary>
        public DateTime FirstSample { get; private set; }

        /// <summary> The type of ActiGraph activity monitor used to record data. </summary>
        public DeviceType TypeOfDevice { get; private set; }

        /// <summary> The serial number of the ActiGraph activity monitor used to record data. </summary>
        public string SerialNumber { get; private set; }

        /// <summary> The number of samples in the .gt3x file. </summary>
        public long SampleCount { get; protected set; }

        /// <summary> The acceleration scale factor to convert values from the bit packed data to Gs. </summary>
        public const float ACCELERATION_SCALE_FACTOR = 341.0f;

        /// <summary> The lux scale factor used to calculate lux values. </summary>
        public double LuxScaleFactor { get; private set; }

        /// <summary> The maximum lux value for a specific device. ActiSleep devices tend to have a higher value. </summary>
        public double LuxMaxValue { get; private set; }

        /// <summary>
        ///     Parse the passed in gt3x file and make sure it contains all the needed information
        /// </summary>
        private void ValidateAndParseZipFile()
        {
            using (var zip = new ZipFile(FileName))
            {
                //make sure we have all the files we need
                if (!zip.ContainsEntry("activity.bin"))
                    throw new MissingRequiredFiles("Missing activity.bin in zip file.", FileName);
                if (!zip.ContainsEntry("lux.bin"))
                    throw new MissingRequiredFiles("Missing lux.bin in zip file.", FileName);
                if (!zip.ContainsEntry("info.txt"))
                    throw new MissingRequiredFiles("Missing info.txt in zip file.", FileName);

                //total samples is equal to the number of BITS in the activity.bin file divided by the number of bits per sample
                const int BITS_PER_BYTE = 8;
                const int BITS_PER_SAMPLE = 36;
                long bytesInActivityFile = zip["activity.bin"].UncompressedSize;
                SampleCount = bytesInActivityFile * BITS_PER_BYTE / BITS_PER_SAMPLE;

                //parse and validate info.txt file
                ZipEntry info = zip["info.txt"];

                if (info.UncompressedSize == 0)
                    throw new InvalidInfoFile("Info.txt in zip file has no data.", FileName);

                //values in info.txt are in format of: "{key}: {value}"
                var infoValues = new Dictionary<string, string>();
                using (var stream = new StreamReader(info.OpenReader()))
                {
                    while (stream.EndOfStream == false)
                    {
                        string line = stream.ReadLine();
                        if (line != null)
                        {
                            string[] temp = line.Split(':');
                            if (temp.Length > 1)
                                if (temp.Length == 2)
                                    infoValues.Add(temp[0].Trim(), temp[1].Trim());
                        }
                    }
                }

                //make sure we have the following: sample rate, start date and serial number
                int sampleRate;
                if (infoValues.ContainsKey("Sample Rate") && int.TryParse(infoValues["Sample Rate"], out sampleRate))
                    SampleRateInHz = sampleRate;
                else
                    throw new InvalidInfoFile("Missing 'Sample Rate' parameter from file (invalid format)", FileName);

                long startDate;
                if (infoValues.ContainsKey("Start Date") && long.TryParse(infoValues["Start Date"], out startDate))
                    FirstSample = new DateTime(startDate);
                else
                    throw new InvalidInfoFile("Missing 'Start Date' parameter from file (invalid format)", FileName);

                if (infoValues.ContainsKey("Serial Number"))
                {
                    SerialNumber = infoValues["Serial Number"];
                    TypeOfDevice = GetDeviceTypeFromSerial(SerialNumber);

                    if (TypeOfDevice == DeviceType.Unknown)
                        throw new InvalidInfoFile(
                            "File created by unknown device! (cannot detect device type from serial number)", FileName);
                }
                else
                    throw new InvalidInfoFile("Missing 'Serial Number' parameter from file (invalid format)", FileName);

                LuxScaleFactor = TypeOfDevice == DeviceType.ActiSleepPlus ? 3.25f : 1.25f;
                LuxMaxValue = TypeOfDevice == DeviceType.ActiSleepPlus ? 6000 : 2500;
            }
        }

        /// <summary>
        ///     Get the type of device used for a gt3x file based on a serial number.
        /// </summary>
        /// <param name="serialNumber">The serial number to use.</param>
        /// <returns>The type of device used. If it is unable to determine a device, it will return DeviceType.Unkonwn. </returns>
        private static DeviceType GetDeviceTypeFromSerial(string serialNumber)
        {
            serialNumber = serialNumber.Trim().ToUpper();

            if (serialNumber.StartsWith("NEO"))
                return DeviceType.GT3XPlus;
            if (serialNumber.StartsWith("MRA"))
                return DeviceType.ActiSleepPlus;

            return DeviceType.Unknown;
        }

        /// <summary> Get all activity samples for a gt3x file. </summary>
        /// <returns>Activity samples.</returns>
        public IEnumerable<AccelerationSample> ActivityEnumerator()
        {
            if (TypeOfDevice == DeviceType.Unknown)
                yield break;

            //zipdotnet let's us access the activity.bin file without extracting it to the filesystem.
            using (ZipFile zip = ZipFile.Read(FileName))
            {
                ZipEntry activity = zip["activity.bin"];
                using (CrcCalculatorStream activityStream = activity.OpenReader())
                    foreach (var sample in ParseAcceleration(activityStream))
                        yield return sample;
            }
        }

        /// <summary> Get all lux samples for a gt3x file. </summary>
        /// <returns>Lux samples.</returns>
        public IEnumerable<double> LuxEnumerator()
        {
            if (TypeOfDevice == DeviceType.Unknown)
                yield break;

            //zipdotnet let's us access the activity.bin file without extracting it to the filesystem.
            using (ZipFile zip = ZipFile.Read(FileName))
            {
                ZipEntry luxZip = zip["lux.bin"];
                using (CrcCalculatorStream luxStream = luxZip.OpenReader())
                    foreach (var lux in ParseLux(luxStream))
                        yield return lux;
            }
        }
        
        /// <summary> Parse activity data from a stream of data </summary>
        /// <param name="stream">The activity.bin stream of data to parse.</param>
        /// <returns>All of the activity samples in a stream.</returns>
        private IEnumerable<AccelerationSample> ParseAcceleration(Stream stream)
        {
            if (!stream.CanRead)
                throw new Exception("Unable to read from activity stream.");

            double[] sample = new double[3];
            int offset = 0;

            int current = 0;

            while (true)
            {
                for (int axis = 0; axis < 3; ++axis)
                {
                    UInt16 shifter;

                    if (0 == (offset & 0x7))
                    {
                        current = stream.ReadByte();
                        if (current == -1)
                        {
                            yield break;
                        }
                        shifter = (UInt16)((current & 0xFF) << 4);
                        offset += 8;

                        current = stream.ReadByte();
                        if (current == -1)
                        {
                            yield break;
                        }
                        shifter |= (UInt16)((current & 0xF0) >> 4);
                        offset += 4;
                    }
                    else
                    {
                        shifter = (UInt16)((current & 0x0F) << 8);
                        offset += 4;

                        current = stream.ReadByte();
                        if (current == -1)
                        {
                            yield break;
                        }
                        shifter |= (UInt16)(current & 0xFF);
                        offset += 8;
                    }
                    if (0 != (shifter & 0x0800))
                        shifter |= 0xF000;

                    sample[axis] = (Int16)shifter / ACCELERATION_SCALE_FACTOR;
                }

                sample[0] = Math.Round(sample[0], 3, MidpointRounding.AwayFromZero);
                sample[1] = Math.Round(sample[1], 3, MidpointRounding.AwayFromZero);
                sample[2] = Math.Round(sample[2], 3, MidpointRounding.AwayFromZero);

                yield return new AccelerationSample(sample);
            }
        }

        /// <summary> Parse lux data from a stream of data. </summary>
        /// <param name="stream">The lux.bin stream to parse.</param>
        /// <returns>All lux values in a stream.</returns>
        private IEnumerable<double> ParseLux(Stream stream)
        {
            if (!stream.CanRead)
                throw new Exception("Unable to read from lux stream.");

            var luxBytes = new byte[2];

            while (stream.Position < stream.Length)
            {
                if (stream.Position < stream.Length - 1)
                    luxBytes[0] = (byte) stream.ReadByte();
                else
                    yield break;

                if (stream.Position < stream.Length - 1)
                    luxBytes[1] = (byte)stream.ReadByte();
                else
                    yield break;

                if (luxBytes[0] == 255 && luxBytes[1] == 255)
                {
                    yield return 0;
                    continue;
                }

                double lux = BitConverter.ToUInt16(luxBytes, 0);

                if (lux < 20.0)
                    lux = 0.0;
                else
                    lux = Math.Min(lux * LuxScaleFactor, LuxMaxValue);
                yield return lux;
            }
        }
    }
}
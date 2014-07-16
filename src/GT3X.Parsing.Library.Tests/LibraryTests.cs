using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Ionic.Zip;

namespace GT3X.Parsing.Library.Tests
{
    public class LibraryTests : IDisposable
    {
        private string _filename;
        const string ACTIVITY_BIN = "activity.bin";
        const string LUX_BIN = "lux.bin";
        const string INFO_TXT = "info.txt";

        [Fact]
        public void EmptyFileNameIsNotParsable()
        {
            Assert.Throws<FileDoesNotExist>(() => new Gt3XFile(string.Empty));
        }

        [Fact]
        public void NullFileNameIsNotParsable()
        {
            Assert.Throws<FileDoesNotExist>(() => new Gt3XFile(null));
        }

        [Fact]
        public void NonExistentFileIsNotParsable()
        {
            Assert.Throws<FileDoesNotExist>(() => new Gt3XFile("thisfiledoesntexist"));
        }

        [Fact]
        public void NonZipFileIsNotParsable()
        {
            _filename = Path.GetTempFileName();

            Assert.Throws<FileIsNotAZipFile>(() => new Gt3XFile(_filename));
        }

        [Fact]
        public void ZipFileWithNoFilesIsNotParsable()
        {
            _filename = Path.GetTempFileName();

            using (ZipFile zip = new ZipFile())
                zip.Save(_filename);

            Assert.Throws<MissingRequiredFiles>(() => new Gt3XFile(_filename));
        }

        [Fact]
        public void ZipFileWithNoActivityBinIsNotParsable()
        {
            _filename = Path.GetTempFileName();

            using (File.Create(LUX_BIN)) { }
            using (File.Create(INFO_TXT)) { }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(LUX_BIN);
                zip.AddFile(INFO_TXT);
                zip.Save(_filename);
            }

            Assert.Throws<MissingRequiredFiles>(() => new Gt3XFile(_filename));
        }

        [Fact]
        public void ZipFileWithNoLuxBinIsNotParsable()
        {
            _filename = Path.GetTempFileName();

            using (File.Create(ACTIVITY_BIN)) { }
            using (File.Create(INFO_TXT)) { }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(ACTIVITY_BIN);
                zip.AddFile(INFO_TXT);
                zip.Save(_filename);
            }

            Assert.Throws<MissingRequiredFiles>(() => new Gt3XFile(_filename));
        }

        [Fact]
        public void ZipFileWithNoInfoTxtIsNotParsable()
        {
            _filename = Path.GetTempFileName();

            using (File.Create(ACTIVITY_BIN)) { }
            using (File.Create(LUX_BIN)) { }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(ACTIVITY_BIN);
                zip.AddFile(LUX_BIN);
                zip.Save(_filename);
            }
            
            Assert.Throws<MissingRequiredFiles>(() => new Gt3XFile(_filename));
        }

        [Fact]
        public void ZipFileWithEmptyInfoTxtIsNotParsable()
        {
            _filename = Path.GetTempFileName();

            using (File.Create(ACTIVITY_BIN)) { }
            using (File.Create(LUX_BIN)) { }
            using (File.Create(INFO_TXT)) { }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(ACTIVITY_BIN);
                zip.AddFile(LUX_BIN);
                zip.AddFile(INFO_TXT);
                zip.Save(_filename);
            }

            Assert.Throws<InvalidInfoFile>(() => new Gt3XFile(_filename));
        }

        [Fact]
        public void ZipFileWithMissingSerialNumberInfoTxtIsNotParsable()
        {
            _filename = Path.GetTempFileName();

            using (File.Create(ACTIVITY_BIN)) { }
            using (File.Create(LUX_BIN)) { }
            CreateInfoTxtFile(true, true, false);

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(ACTIVITY_BIN);
                zip.AddFile(LUX_BIN);
                zip.AddFile(INFO_TXT);
                zip.Save(_filename);
            }

            Assert.Throws<InvalidInfoFile>(() => new Gt3XFile(_filename));
        }

        [Fact]
        public void ZipFileWithMissingSampleRateInfoTxtIsNotParsable()
        {
            _filename = Path.GetTempFileName();

            using (File.Create(ACTIVITY_BIN)) { }
            using (File.Create(LUX_BIN)) { }
            CreateInfoTxtFile(false, true, true);

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(ACTIVITY_BIN);
                zip.AddFile(LUX_BIN);
                zip.AddFile(INFO_TXT);
                zip.Save(_filename);
            }

            Assert.Throws<InvalidInfoFile>(() => new Gt3XFile(_filename));
        }

        [Fact]
        public void ZipFileWithMissingStartDateInfoTxtIsNotParsable()
        {
            _filename = Path.GetTempFileName();

            using (File.Create(ACTIVITY_BIN)) { }
            using (File.Create(LUX_BIN)) { }
            CreateInfoTxtFile(true, false, true);

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(ACTIVITY_BIN);
                zip.AddFile(LUX_BIN);
                zip.AddFile(INFO_TXT);
                zip.Save(_filename);
            }

            Assert.Throws<InvalidInfoFile>(() => new Gt3XFile(_filename));
        }

        [Fact]
        public void ZipFileWithValidInfoTxtIsParsable()
        {
            _filename = Path.GetTempFileName();
            
            using (File.Create(ACTIVITY_BIN)) { }
            using (File.Create(LUX_BIN)) { }
            
            CreateInfoTxtFile(true, true, true);

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(ACTIVITY_BIN);
                zip.AddFile(LUX_BIN);
                zip.AddFile(INFO_TXT);
                zip.Save(_filename);
            }

            Assert.DoesNotThrow(() => new Gt3XFile(_filename));
        }

        [Fact]
        public void ValidZipFileAllZeroActivityParses()
        {
            _filename = Path.GetTempFileName();

            using (var file = File.Create(ACTIVITY_BIN))
                for (int i = 0; i < 27; i++)
                    file.WriteByte(0);

            using (File.Create(LUX_BIN)) { }

            CreateInfoTxtFile(true, true, true);

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(ACTIVITY_BIN);
                zip.AddFile(LUX_BIN);
                zip.AddFile(INFO_TXT);
                zip.Save(_filename);
            }

            var g = new Gt3XFile(_filename);

            foreach (AccelerationSample sample in g.ActivityEnumerator())
            {
                Assert.Equal(0, sample.X);
                Assert.Equal(0, sample.Y);
                Assert.Equal(0, sample.Z);
            }
        }

        
        [Fact]
        public void ValidZipFileSampleActivityParses()
        {
            _filename = Path.GetTempFileName();

            List<byte> bytes = new List<byte>(14)
            {
                0x00,
                0x60,
                0x08,
                0xEB,
                0xD0,
                0x07,
                0x00,
                0x9E,
                0xBF,
                0x00,
                0x70,
                0x08,
                0xEB,
                0xF0
            };

            using (var file = File.Create(ACTIVITY_BIN))
                foreach (var b in bytes)
                    file.WriteByte(b);

            using (File.Create(LUX_BIN)) { }

            CreateInfoTxtFile(true, true, true);

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(ACTIVITY_BIN);
                zip.AddFile(LUX_BIN);
                zip.AddFile(INFO_TXT);
                zip.Save(_filename);
            }

            var g = new Gt3XFile(_filename);

            int counter = 0;

            List<AccelerationSample> expectedSamples = new List<AccelerationSample>(3)
            {
                new AccelerationSample(0.023, 0.018, -0.947, DateTime.MinValue),
                new AccelerationSample(0.026, 0.021, -0.941, DateTime.MinValue),
                new AccelerationSample(0.023, 0.021, -0.941, DateTime.MinValue)
            };

            foreach (AccelerationSample actualSample in g.ActivityEnumerator())
            {
                Assert.Equal(expectedSamples[counter].X, actualSample.X);
                Assert.Equal(expectedSamples[counter].Y, actualSample.Y);
                Assert.Equal(expectedSamples[counter].Z, actualSample.Z);

                counter++;
            }
        }

        [Fact]
        public void ValidZipFileAllZeroLuxParses()
        {
            _filename = Path.GetTempFileName();

            using (var file = File.Create(LUX_BIN))
                for (int i = 0; i < 27; i++)
                    file.WriteByte(0);

            using (File.Create(ACTIVITY_BIN)) { }

            CreateInfoTxtFile(true, true, true);

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(ACTIVITY_BIN);
                zip.AddFile(LUX_BIN);
                zip.AddFile(INFO_TXT);
                zip.Save(_filename);
            }

            var g = new Gt3XFile(_filename);

            foreach (var sample in g.LuxEnumerator())
                Assert.Equal(0, sample.Lux);
        }

        [Fact]
        public void ValidZipFileSampleLuxParses()
        {
            _filename = Path.GetTempFileName();

            List<byte> bytes = new List<byte>(10)
            {
                0x00,
                0x00,
                0x13,
                0x00,
                0xFF,
                0xFF,
                0x98,
                0x08,
                0x4C,
                0x04
            };

            using (var file = File.Create(LUX_BIN))
                foreach (var b in bytes)
                    file.WriteByte(b);

            using (File.Create(ACTIVITY_BIN)) { }

            CreateInfoTxtFile(true, true, true);

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(ACTIVITY_BIN);
                zip.AddFile(LUX_BIN);
                zip.AddFile(INFO_TXT);
                zip.Save(_filename);
            }

            var g = new Gt3XFile(_filename);

            List<double> expectedLux = new List<double>(5) 
            {
                0, 
                0, 
                0, 
                2500, 
                1375
            };

            int counter = 0;
            foreach (var luxSample in g.LuxEnumerator())
                Assert.Equal(expectedLux[counter++], luxSample.Lux);
        }

        private static void CreateInfoTxtFile(bool addSampleRate, bool addStartDate, bool addSerialNumber)
        {
            using (var file = new StreamWriter(INFO_TXT))
            {
                if (addSerialNumber) file.WriteLine("Serial Number: NEO1C16110020");
                file.WriteLine("Firmware: 2.2.0");
                file.WriteLine("Battery Voltage: 4.25");
                if (addSampleRate) file.WriteLine("Sample Rate: 30");
                if (addStartDate) file.WriteLine("Start Date: 634556532600000000");
                file.WriteLine("Stop Date: 0");
                file.WriteLine("Download Date: 634570285571111563");
                file.WriteLine("Board Revision: 2");
                file.WriteLine("Subject Name: john doe");
            }
        }

        public void Dispose()
        {
            try { File.Delete(_filename); } catch { }
            try { File.Delete(ACTIVITY_BIN); } catch { }
            try { File.Delete(LUX_BIN); } catch { }
            try { File.Delete(INFO_TXT); } catch { }
        }
    }
}

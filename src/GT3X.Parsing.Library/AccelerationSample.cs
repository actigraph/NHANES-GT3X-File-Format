using System;
using CsvHelper.Configuration;

namespace GT3X.Parsing.Library
{
    public class AccelerationSample : IEquatable<AccelerationSample>
    {
        public DateTime Timestamp { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }


        public override string ToString()
        {
            return string.Format("{3:G} - Y = {0} | X = {1} | Z = {2}", Y, X, Z, Timestamp);
        }

        bool IEquatable<AccelerationSample>.Equals(AccelerationSample other)
        {
            return Equals(other);
        }

        public AccelerationSample(double x, double y, double z, DateTime timestamp)
        {
            X = x;
            Y = y;
            Z = z;
            Timestamp = timestamp;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X.GetHashCode();
                hashCode = (hashCode*397) ^ Y.GetHashCode();
                hashCode = (hashCode*397) ^ Z.GetHashCode();
                hashCode = (hashCode*397) ^ Timestamp.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(AccelerationSample left, AccelerationSample right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AccelerationSample left, AccelerationSample right)
        {
            return !Equals(left, right);
        }
    }

    /// <summary> This helps create the CSV output in the format we want </summary>
    public sealed class AcclererationClassMap : CsvClassMap<AccelerationSample>
    {
        public AcclererationClassMap()
        {
            //set the timestamp format to include milliseconds
            Map(m => m.Timestamp).Index(0).TypeConverterOption("yyyy-MM-dd HH:mm:ss.fff");
            Map(m => m.Y).Index(1);
            Map(m => m.X).Index(2);
            Map(m => m.Z).Index(3);

        }
    }
}
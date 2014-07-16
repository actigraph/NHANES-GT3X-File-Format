using System;
using CsvHelper.Configuration;

namespace GT3X.Parsing.Library
{
    public class LuxSample : IEquatable<LuxSample>
    {
        public double Lux { get; private set; }
        public DateTime Timestamp { get; private set; }

        public LuxSample(double lux, DateTime timestamp)
        {
            Lux = lux;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return string.Format("{0:G} - {1}", Timestamp, Lux);
        }

        public bool Equals(LuxSample other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Lux.Equals(other.Lux) && Timestamp.Equals(other.Timestamp);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LuxSample) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Lux.GetHashCode()*397) ^ Timestamp.GetHashCode();
            }
        }

        public static bool operator ==(LuxSample left, LuxSample right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LuxSample left, LuxSample right)
        {
            return !Equals(left, right);
        }
    }
    
    /// <summary> This helps create the CSV output in the format we want </summary>
    public sealed class LuxClassMap : CsvClassMap<LuxSample>
    {
        public LuxClassMap()
        {
            Map(m => m.Timestamp).Index(0);
            Map(m => m.Lux).Index(1);
        }
    }
}
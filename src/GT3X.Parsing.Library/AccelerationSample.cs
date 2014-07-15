using System;

namespace GT3X.Parsing.Library
{
    public class AccelerationSample : IEquatable<AccelerationSample>
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public override string ToString()
        {
            return string.Format("Y = {0} | X = {1} | Z = {2}", Y, X, Z);
        }

        bool IEquatable<AccelerationSample>.Equals(AccelerationSample other)
        {
            return Equals(other);
        }

        public AccelerationSample()
        {
            X = Y = Z = 0.0;
        }

        public AccelerationSample(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public AccelerationSample(double[] sample)
        {
            X = sample[1];
            Y = sample[0];
            Z = sample[2];
        }

        public bool Equals(AccelerationSample other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AccelerationSample) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X.GetHashCode();
                hashCode = (hashCode*397) ^ Y.GetHashCode();
                hashCode = (hashCode*397) ^ Z.GetHashCode();
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
}
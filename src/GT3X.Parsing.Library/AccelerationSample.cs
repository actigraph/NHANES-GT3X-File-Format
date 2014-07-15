namespace GT3X.Parsing.Library
{
    public class AccelerationSample
    {
        public double X, Y, Z;

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
    }
}
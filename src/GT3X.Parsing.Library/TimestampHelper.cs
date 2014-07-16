namespace GT3X.Parsing.Library
{
    public class TimestampHelper
    {
        private readonly int _denominator;
        private readonly int _numerator;
        private readonly int _quotient;
        private int _count;

        public TimestampHelper(int dividend, int divisor)
        {
            int quotient = dividend/divisor;
            int remainder = dividend%divisor;
            int gcd = GreatestCommonDivisor(divisor, remainder);

            _quotient = quotient;
            _numerator = remainder/gcd;
            _denominator = divisor/gcd;
            _count = 0;

            Reset();
        }

        private int GreatestCommonDivisor(int a, int b)
        {
            while (0 != a)
            {
                int c = a;
                a = b%a;
                b = c;
            }

            return b;
        }

        public int Next()
        {
            int tick = _quotient + (_count < _numerator ? 1 : 0);

            if (_denominator == ++_count)
                Reset();

            return tick;
        }

        public void Reset()
        {
            _count = 0;
        }
    }
}
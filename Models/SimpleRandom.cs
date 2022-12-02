namespace AddressGenerator.Models
{
    public class SimpleRandom
    {
        private readonly int seed;
        private int counter;

        public SimpleRandom(int seed) { this.seed = (seed + ushort.MaxValue); }
        public SimpleRandom() : this(new Random().Next()) { }

        public int Next() => Next(int.MaxValue);

        public int Next(int maxValue) => Next(default, maxValue);

        public int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                (minValue, maxValue) = (maxValue, minValue);
            }
            int _seed = seed > 0 ? seed : System.Math.Abs(seed + 1);
            if (_seed == 0)
            {
                _seed += short.MaxValue;
            }
            int result;
            int diff = maxValue - minValue;
            do
            {
                counter++;
                counter += (counter * _seed * (_seed + 1) * (_seed + 2)) + (int)System.Math.Pow(counter, 5);
                int i = counter % diff;
                result = minValue + System.Math.Abs(i) - 1;
            }
            while (result < minValue || result >= maxValue);
            return result;
        }
    }
}

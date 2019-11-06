using System;

namespace GameEngine
{
    public static class SeededRandom
    {
        private static Random Random;

        public static int Seed
        {
            set { SetSeed(value); }
        }

        static SeededRandom()
        {
            int seed = new Random().Next(int.MinValue, int.MaxValue);
            SetSeed(seed);
        }

        public static int Next()
        {
            return Random.Next();
        }

        public static int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        public static void NextBytes(byte[] buffer)
        {
            Random.NextBytes(buffer);
        }

        public static double NextDouble()
        {
            return Random.NextDouble();
        }

        private static void SetSeed(int seed)
        {
            Random = new Random(seed);
            Console.Out.WriteLine("New random seed: " + seed);
        }
    }
}

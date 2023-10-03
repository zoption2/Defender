using System;
using System.Collections.Generic;


namespace Tools
{
    public static class RandomExtensions
    {
        public static TValue GetRandomValue<TValue>(this Random randomizer, List<TValue> range)
        {
            int maxIndexOfRange = range.Count;
            var randomIndex = randomizer.Next(0, maxIndexOfRange);
            var result = range[randomIndex];
            return result;
        }

        public static TValue GetRandomValue<TValue>(this Random randomizer, TValue[] range)
        {
            int maxIndexOfRange = range.Length;
            var randomIndex = randomizer.Next(0, maxIndexOfRange);
            var result = range[randomIndex];
            return result;
        }

        public static int GetRandomValue(this Random randomizer, int minIncluded, int maxExcluded)
        {
            var result = randomizer.Next(minIncluded, maxExcluded);
            return result;
        }
    }
}


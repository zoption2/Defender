using System;
using System.Collections.Generic;


namespace Tools
{
    public interface IRandomizeService
    {
        Random GetNewRandomizer(int seed = -1);
        void ReleaseRandomizer(Random randomizer);
    }


    public class RandomizeService : IRandomizeService
    {
        private readonly HashSet<Random> _randoms;

        public RandomizeService()
        {
            _randoms = new HashSet<Random>();
        }


        public Random GetNewRandomizer(int seed = -1)
        {
            var random = seed == -1
                ? new Random()
                : new Random(seed);

            _randoms.Add(random);
            return random;
        }

        public void ReleaseRandomizer(Random randomizer)
        {
            if (_randoms.Contains(randomizer))
            {
                _randoms.Remove(randomizer);
            }
        }
    }
}


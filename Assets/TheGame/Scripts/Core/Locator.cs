using System.Collections.Generic;
using Tools;
using Vector2Int = UnityEngine.Vector2Int;
using Random = System.Random;

namespace TheGame
{
    public class Locator
    {
        private readonly IRandomizeService _randomizeService;
        private bool[,] _positions = new bool[4, 4];
        private Random _randomizer;

        public Locator(IRandomizeService randomizeService)
        {
            _randomizeService = randomizeService;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _positions[i, j] = true;
                }
            }
        }

        public void Init(int seed)
        {
            _randomizer = _randomizeService.GetNewRandomizer(seed);
        }

        public Vector2Int GetRandomFreePosition()
        {
            List<Vector2Int> freePositions = new List<Vector2Int>();

            // Find all free positions
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (_positions[i, j])
                    {
                        freePositions.Add(new Vector2Int(i, j));
                        break;
                    }
                }
            }

            // If there are no free positions, return (-1, -1) to indicate none available
            if (freePositions.Count == 0)
            {
                return new Vector2Int(-1, -1);
            }

            // Randomly select a free position
            Vector2Int selectedPosition = _randomizer.GetRandomValue(freePositions);
            _positions[selectedPosition.x, selectedPosition.y] = false; // Mark as busy
            return selectedPosition;
        }

        public void FreePosition(Vector2Int position)
        {
            if (IsValidPosition(position))
            {
                _positions[position.x, position.y] = true; // Mark as free
            }
        }

        private bool IsValidPosition(Vector2Int position)
        {
            return position.x >= 0 && position.x < 4 && position.y >= 0 && position.y < 4;
        }
    }

}


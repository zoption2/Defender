using UnityEngine;

namespace Gameplay
{
    internal class CardPosition : MonoBehaviour
    {
        private int[,] _position = new int[1,1];
        [field: SerializeField] public Vector2 Position { get; private set; }

        public int[,] GetPosition()
        {
            _position[0, 0] = (int)Position.x;
            _position[0, 1] = (int)Position.y;
            return _position;
        }
    }
}


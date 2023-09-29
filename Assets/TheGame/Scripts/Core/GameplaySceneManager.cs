using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;
using Gameplay;
using Zenject;
using Cysharp.Threading.Tasks;

namespace TheGame
{
    public class GameplaySceneManager : MonoBehaviour
    {
        [Inject] private IInputService _inputService;
        [Inject] private ISpellCardMediator _spellCardMediator;
        [SerializeField] private SpellCardView _prefab;
        [SerializeField] private Transform[] _pointers;
        [SerializeField] private CardPosition[] _cardPositions;
        [SerializeField] private Transform _cardHolder;

        private List<SpellCardController> _controllers = new();
        private System.Random _random;
        private Locator _locator;
        private Dictionary<Vector2Int, Transform> _positions;

        private async void Start()
        {
            _random = new System.Random();
            _inputService.Initialize();
            _spellCardMediator.Initialize();
            BuildPositions();
            _locator = new Locator();
            for (int i = 0; i < 16; i++)
            {
                var location = _locator.GetRandomFreePosition();
                Transform point = _positions[location];
                var controller = new SpellCardController(_inputService, _spellCardMediator);
                var view = Instantiate(_prefab, point.transform.position, Quaternion.identity, _cardHolder);
                var randomData = _testDatas[_random.Next(_testDatas.Count)];
                var model = new SpellCardModel(randomData);
                await controller.Init(view, model);
                _controllers.Add(controller);
                await UniTask.Delay(1000);
            }
        }

        private List<CardData> _testDatas = new()
        {
            new CardData()
            {
                Type = CardType.Spell,
                Spell = SpellType.none,
                Element = ElementType.Fire,
                Power = 5,
                CritChance = 10,
                CritMultiplier = 2,
                ElementalPower = 2
            },
            new CardData()
            {
                Type = CardType.Spell,
                Spell = SpellType.none,
                Element = ElementType.Water,
                Power = 5,
                CritChance = 10,
                CritMultiplier = 2,
                ElementalPower = 2
            },
        };

        private void BuildPositions()
        {
            _positions = new Dictionary<Vector2Int, Transform>(16);
            int counter = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Vector2Int position = new Vector2Int(i, j);
                    _positions[position] = _pointers[counter];
                    counter++;
                }
            }
        }
    }

    public class Locator
    {
        private bool[,] _positions = new bool[4, 4];
        private System.Random _random;

        public Locator()
        {
            _random = new System.Random(1);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _positions[i, j] = true;
                }
            }
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
            int randomIndex = _random.Next(0, freePositions.Count);
            Vector2Int selectedPosition = freePositions[randomIndex];
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


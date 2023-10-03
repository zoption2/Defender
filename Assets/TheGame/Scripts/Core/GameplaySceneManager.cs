using Tools;
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
        [Inject] private IRandomizeService _randomizeService;
        [Inject] private Locator _locator;
        [SerializeField] private SpellCardView _prefab;
        [SerializeField] private Transform[] _pointers;
        [SerializeField] private CardPosition[] _cardPositions;
        [SerializeField] private Transform _cardHolder;

        private List<SpellCardController> _controllers = new();
        private System.Random _random;

        private Dictionary<Vector2Int, Transform> _positions;

        private async void Start()
        {
            _random = _randomizeService.GetNewRandomizer(1);
            _inputService.Initialize();
            _spellCardMediator.Initialize();
            BuildPositions();
            _locator.Init(1);

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
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;
using Gameplay;
using Zenject;

namespace TheGame
{
    public class Landing : MonoBehaviour
    {
        [Inject] private IInputService _inputService;
        [Inject] private ISpellCardMediator _spellCardMediator;
        [SerializeField] private SpellCardView _prefab;
        [SerializeField] private Transform _parent;
        private List<SpellCardController> _controllers = new();
        private System.Random _random;

        private async void Start()
        {
            _random = new System.Random();
            _inputService.Initialize();
            _spellCardMediator.Initialize();
            for (int i = 0; i < 16; i++)
            {
                var controller = new SpellCardController(_inputService, _spellCardMediator);
                var view = Instantiate(_prefab, _parent);
                var randomData = _testDatas[_random.Next(_testDatas.Count)];
                var model = new SpellCardModel(randomData);
                await controller.Init(view, model);
                _controllers.Add(controller);
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
    }
}


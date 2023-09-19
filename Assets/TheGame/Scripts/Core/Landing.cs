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

        private async void Start()
        {
            _inputService.Initialize();
            _spellCardMediator.Initialize();
            for (int i = 0; i < 16; i++)
            {
                var controller = new SpellCardController(_inputService, _spellCardMediator);
                var view = Instantiate(_prefab, _parent);
                var model = new SpellCardModel();
                await controller.Init(view, model);
                _controllers.Add(controller);
            }
        }
    }
}


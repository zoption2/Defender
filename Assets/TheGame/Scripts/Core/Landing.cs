using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;
using TheGame.Gameplay;

namespace TheGame
{
    public class Landing : MonoBehaviour
    {
        private IInputService _inputService;
        [SerializeField] private SpellCardView _prefab;
        [SerializeField] private Transform _parent;
        private List<SpellCardController> _controllers = new();

        private void Start()
        {
            _inputService = new InputService();
            _inputService.Initialize();

            //for (int i = 0; i < 16; i++)
            //{
            //    var controller = new SpellCardController(_inputService);
            //    var view = Instantiate(_prefab, _parent);
            //    var model = new SpellCardModel();
            //    await controller.Init(view, model);
            //    _controllers.Add(controller);
            //}
        }
    }
}


using System.Collections.Generic;
using TheGame.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TheGame.Core;
using TheGame.Gameplay;

namespace TheGame.Services
{
    public class InputService : IInputService
    {
        private Camera _camera;
        private EventSystem _eventSystem;
        private List<IInteractable> _selected;
        private bool _isInputPressed;


        public InputService()
        {
            _selected = new List<IInteractable>();
        }

        public void Initialize()
        {
            _camera = Camera.main;
            Debug.Log("InputService inited");
        }

        public void RegisterClick(IInteractable interactable)
        {
            if (!_selected.Contains(interactable))
            {
                _selected.Add(interactable);
                interactable.Select();
            }
        }

        public void RegisterEnter(IInteractable interactable)
        {
            if (_isInputPressed && !_selected.Contains(interactable))
            {
                _selected.Add(interactable);
                interactable.Select();
            }
            else
            {
                interactable.Highlight();
            }
        }

        public void RegisterExit(IInteractable interactable)
        {
            if (!_isInputPressed)
            {
                interactable.Unhighlight();
            }
        }
    }

}


using System.Collections.Generic;
using TheGame.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
using TheGame.Gameplay;

namespace TheGame.Services
{
    public interface IInputService
    {
        void Initialize();
        void RegisterClick(IInteractable interactable);
        void RegisterEnter(IInteractable interactable);
        void RegisterExit(IInteractable interactable);
    }


    public class InputSystemService : IInputService
    {
        private Inputs _inputs;
        private Camera _camera;
        private InputSystemUIInputModule _inputModule;
        private List<IInteractable> _selected;
        private bool _isInputPressed;

        public InputSystemUIInputModule InputModule
        {
            get
            {
                if (_inputModule == null)
                {
                    var go = new GameObject("NewEventSystem");
                    _inputModule = go.AddComponent<InputSystemUIInputModule>();
                    _inputModule.actionsAsset = _inputs.asset;
                }
                return _inputModule;
            }
        }

        public InputSystemService()
        {
            _selected = new List<IInteractable>();
        }

        public void Initialize()
        {
            _inputs = new Inputs();
            _inputs.Enable();
            _camera = Camera.main;
            BindInputs();
            InputModule.ActivateModule();
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

        private void BindInputs()
        {
            var map = _inputs.Main;
            _inputs.Main.Contact.started += ContactStarted;
            _inputs.Main.Contact.canceled += ContactCanceled;
            _inputs.Main.Tap.started += TapStarted;
            _inputs.Main.TouchPosition.started += PointerStarted;
        }


        private void PointerStarted(InputAction.CallbackContext obj)
        {
            Debug.Log("Pointer started");
            _isInputPressed = true;
            Physics.Raycast(Camera.main.ScreenPointToRay(obj.ReadValue<Vector2>()), out RaycastHit hit);
            if (!hit.collider)
            {
                return;
            }

            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.Select();
            }
        }

        private void TapStarted(InputAction.CallbackContext obj)
        {
            Debug.Log("Tap started");
            _isInputPressed = true;
            Physics.Raycast(Camera.main.ScreenPointToRay(obj.ReadValue<Vector2>()), out RaycastHit hit);
            if (!hit.collider)
            {
                return;
            }

            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.Select();
            }
        }

        private void ContactStarted(InputAction.CallbackContext obj)
        {
            Debug.Log("Contact started");
            _isInputPressed = true;
            Physics.Raycast(Camera.main.ScreenPointToRay(obj.ReadValue<Vector2>()), out RaycastHit hit);
            if (!hit.collider)
            {
                return;
            }

            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.Select();
            }
        }

        private void ContactCanceled(InputAction.CallbackContext obj)
        {
            Debug.Log("Contact canceled");
            _isInputPressed = false;
        }
    }
}


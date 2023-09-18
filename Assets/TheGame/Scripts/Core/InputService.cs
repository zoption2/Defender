using System.Collections.Generic;
using TheGame.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TheGame.Core
{
    public interface IInputService
    {
        void Initialize();
        void RegisterClick(IInteractable interactable);
        void RegisterEnter(IInteractable interactable);
        void RegisterExit(IInteractable interactable);
    }


    public class InputService : IInputService
    {
        private Inputs _inputs;
        private Camera _camera;
        private InputSystemUIInputModule _inputModule;
        private List<IInteractable> _selected;
        private bool _isInputPressed;
        private Vector2 _poinerPosition;
        private RaycastHit[] _raycastHits = new RaycastHit[1];
        private IInteractable _current;

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

        public InputService()
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
            if (_isInputPressed)
            {
                if (!_selected.Contains(interactable))
                {
                    _selected.Add(interactable);
                    interactable.Select();
                }
            }
            else
            {
                interactable.Highlight();
            }
            _current = interactable;
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
            _inputs.Main.Contact.Enable();
            //_inputs.Main.Tap.started += TapStarted;
            _inputs.Main.Tap.Enable();
            //_inputs.Main.TouchPosition.started += PointerStarted;
            _inputs.Main.TouchPosition.performed += PositionPerformed;
            _inputs.Main.TouchPosition.Enable();
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
            /*Physics.Raycast(Camera.main.ScreenPointToRay(obj.ReadValue<Vector2>()), out RaycastHit hit);
            if (!hit.collider)
            {
                return;
            }

            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.Select();
            }*/
        }

        private void ContactStarted(InputAction.CallbackContext obj)
        {
            Debug.Log("Contact started");
            _isInputPressed = true;
            Ray ray = _camera.ScreenPointToRay(_poinerPosition);
            Physics.Raycast(ray, out RaycastHit hit);
            //var hit = _raycastHits[0];
            if (!hit.collider)
            {
                return;
            }

            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                RegisterEnter(interactable);
            }
        }

        private void ContactCanceled(InputAction.CallbackContext obj)
        {
            Debug.Log("Contact canceled");
            _isInputPressed = false;

            Ray ray = _camera.ScreenPointToRay(_poinerPosition);
            Physics.Raycast(ray, out RaycastHit hit);
            //var hit = _raycastHits[0];
            if (!hit.collider)
            {
                for (int i = 0, j = _selected.Count; i < j; i++)
                {
                    _selected[i].UnSelect();
                }
                _current = null;
                _selected.Clear();
                return;
            }

            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                for (int i = 0, j = _selected.Count; i < j; i++)
                {
                    _selected[i].Activate();
                }
                _selected.Clear();
            }
        }

        private void PositionPerformed(InputAction.CallbackContext obj)
        {
            _poinerPosition = obj.ReadValue<Vector2>();
            Debug.Log("Position performing" + obj.ReadValue<Vector2>());

            Ray ray = _camera.ScreenPointToRay(_poinerPosition);
            Physics.Raycast(ray, out RaycastHit hit);
            //var hit = _raycastHits[0];
            if (!hit.collider)
            {
                if (_current != null)
                {
                    RegisterExit(_current);
                    _current = null;
                }
                return;
            }

            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                RegisterEnter(interactable);
            }
        }
    }

    public interface IInteractable
    {
        void Activate();
        void Highlight();
        void Unhighlight();
        void Select();
        void UnSelect();
    }

}


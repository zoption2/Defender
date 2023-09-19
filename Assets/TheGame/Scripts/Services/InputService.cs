using System.Collections.Generic;
using TheGame.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Gameplay;
using Services.InputEvents;

namespace Services
{
    public interface IInputService
    {
        event System.Action OnInputPressed;
        void Initialize();

    }


    public class InputService : IInputService
    {
        public event System.Action OnInputPressed;

        private Inputs _inputs;
        private Camera _camera;
        private InputSystemUIInputModule _inputModule;
        private List<IInteractable> _selected;
        private bool _isInputPressed;
        private Vector2 _poinerPosition;
        private RaycastHit[] _raycastHits = new RaycastHit[3];
        private IInteractable _last;
        private IInteractable _current;
        private InteractionInfo _info;

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
            _info = new InteractionInfo();
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

        private void BindInputs()
        {
            var map = _inputs.Main;
            _inputs.Main.Contact.started += OnPressStarted;
            _inputs.Main.Contact.canceled += OnPressCanceled;
            _inputs.Main.Contact.Enable();
            _inputs.Main.TouchPosition.performed += OnInputTrackingPerformed;
            _inputs.Main.TouchPosition.Enable();
        }

        private void OnPressStarted(InputAction.CallbackContext obj)
        {
            Debug.Log("Press started");
            _isInputPressed = true;
            Ray ray = _camera.ScreenPointToRay(_poinerPosition);
            var hits = Physics.RaycastNonAlloc(ray, _raycastHits);
            if (hits == 0)
            {
                return;
            }

            if (_raycastHits[0].transform.TryGetComponent(out IInteractable interactable))
            {
                UpdateInfo();
                interactable.Select(_info);
            }
        }

        private void OnPressCanceled(InputAction.CallbackContext obj)
        {
            Debug.Log("Press canceled");
            _isInputPressed = false;
            UpdateInfo();
            Ray ray = _camera.ScreenPointToRay(_poinerPosition);
            var hits = Physics.RaycastNonAlloc(ray, _raycastHits);
            if (hits == 0)
            {
                _last?.RejectSelected();
                return;
            }

            if (_raycastHits[0].transform.TryGetComponent(out IInteractable interactable))
            {
                _current.ApproveSelected();
            }

            _selected.Clear();
        }

        private void OnInputTrackingPerformed(InputAction.CallbackContext obj)
        {
            _poinerPosition = obj.ReadValue<Vector2>();
            Debug.Log("Position performing" + obj.ReadValue<Vector2>());

            Ray ray = _camera.ScreenPointToRay(_poinerPosition);
            var hits = Physics.RaycastNonAlloc(ray, _raycastHits);
            if (hits == 0)
            {
                if (_current != null)
                {
                    UpdateInfo();
                    _current.Deselect(_info);
                    _current = null;
                }
                return;
            }

            if (_raycastHits[0].transform.TryGetComponent(out IInteractable interactable))
            {
                UpdateInfo();
                if (interactable != _current)
                {
                    interactable.Select(_info);
                    _current = interactable;
                    _last = interactable;
                }

                //{
                //    if (_isInputPressed && !_selected.Contains(interactable))
                //    {
                //        _selected.Add(interactable);
                //    }
                //}

            }
        }

        private void UpdateInfo()
        {
            var tempInfo = _info;
            tempInfo.ScreenPosition = _poinerPosition;
            tempInfo.IsPressed = _isInputPressed;
            _info = tempInfo;
        }
    }
}


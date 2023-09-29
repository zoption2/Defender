using System.Collections.Generic;
using TheGame.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Gameplay;
using Services.InputEvents;
using System;

namespace Services
{
    public interface IInputService
    {
        event System.Action OnInputPressed;
        event System.Action OnInputUp;
        void Initialize();
        void SetCurrentListener(IInputListener listener);
    }

    public interface IInputListener
    {
        void Notify();
    }

    public class InputService : IInputService
    {
        public event Action OnInputPressed;
        public event Action OnInputUp;

        private Inputs _inputs;
        private Camera _camera;
        private InputSystemUIInputModule _inputModule;
        private bool _isInputPressed;
        private Vector2 _poinerPosition;
        private RaycastHit[] _raycastHits = new RaycastHit[3];
        private IInteractable _last;
        private IInteractable _current;
        private InteractionInfo _info;
        private IInputListener _currentListener;

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
            _info = new InteractionInfo();
        }

        public void Initialize()
        {
            _inputs = new Inputs();
            _inputs.Enable();
            BindInputs();
            _camera = Camera.main;
            InputModule.ActivateModule();
            Debug.Log("InputService inited");
        }

        public void SetCurrentListener(IInputListener listener)
        {
            _currentListener = listener;
        }

        private void BindInputs()
        {
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
                interactable.OnPointerEnter(_info);
            }
        }

        private void OnPressCanceled(InputAction.CallbackContext obj)
        {
            Debug.Log("Press canceled");
            _isInputPressed = false;
            UpdateInfo();

            if (_currentListener != null)
            {
                _currentListener.Notify();
            }

            Ray ray = _camera.ScreenPointToRay(_poinerPosition);
            var hits = Physics.RaycastNonAlloc(ray, _raycastHits);
            if (hits == 0)
            {
                return;
            }

            if (_raycastHits[0].transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.OnPointerUp(_info);
            }
        }

        private void OnInputTrackingPerformed(InputAction.CallbackContext obj)
        {
            _poinerPosition = obj.ReadValue<Vector2>();
            UpdateInfo();
            Ray ray = _camera.ScreenPointToRay(_poinerPosition);
            var hits = Physics.RaycastNonAlloc(ray, _raycastHits);
            if (hits == 0)
            {
                if (_current != null)
                {
                    _current.OnPointerExit(_info);
                    _current = null;
                }
                return;
            }

            if (_raycastHits[0].transform.TryGetComponent(out IInteractable interactable))
            {
                if (interactable != _current)
                {
                    interactable.OnPointerEnter(_info);
                    _current = interactable;
                    //_last = interactable;
                }
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


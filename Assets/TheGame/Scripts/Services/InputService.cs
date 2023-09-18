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
        private IInputTargetHandler _currentTarget;
        private InputInfo _info;

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
            _info = new InputInfo();
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
                RegisterEnter(interactable);
            }
        }

        private void OnPressCanceled(InputAction.CallbackContext obj)
        {
            Debug.Log("Press canceled");
            _isInputPressed = false;

            Ray ray = _camera.ScreenPointToRay(_poinerPosition);
            var hits = Physics.RaycastNonAlloc(ray, _raycastHits);
            if (hits == 0)
            {
                for (int i = 0, j = _selected.Count; i < j; i++)
                {
                    _selected[i].UnSelect();
                }
                _currentTarget = null;
                _selected.Clear();
                return;
            }

            if (_raycastHits[0].transform.TryGetComponent(out IInteractable interactable))
            {
                for (int i = 0, j = _selected.Count; i < j; i++)
                {
                    _selected[i].Activate();
                }
                _selected.Clear();
            }
        }

        private void OnInputTrackingPerformed(InputAction.CallbackContext obj)
        {
            _poinerPosition = obj.ReadValue<Vector2>();
            Debug.Log("Position performing" + obj.ReadValue<Vector2>());

            Ray ray = _camera.ScreenPointToRay(_poinerPosition);
            var hits = Physics.RaycastNonAlloc(ray, _raycastHits);
            if (hits == 0)
            {
                if (_currentTarget != null)
                {
                    UpdateInfo();
                    _currentTarget.OnInputTargetLost(_info);
                    _currentTarget = null;
                }
                return;
            }

            if (_raycastHits[0].transform.TryGetComponent(out IInputTargetHandler target))
            {
                UpdateInfo();
                target.OnInputTarget(_info);
                _currentTarget = target;
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


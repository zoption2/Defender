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
        private EventSystem _eventSystem;
        private InputSystemUIInputModule _inputModule;
        private GraphicRaycaster _raycaster;
        private List<IInteractable> _selected;
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
                    _eventSystem = go.AddComponent<EventSystem>();
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
            //_raycaster = GameObject.FindObjectOfType<Canvas>().GetComponent<GraphicRaycaster>();
            BindInputs();
            InputModule.ActivateModule();
            Debug.Log("InputService inited");
        }

        public void SetCurrentListener(IInputListener listener)
        {
            _currentListener = listener;
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

            //PointerEventData eventData = new PointerEventData(_eventSystem);
            //eventData.position = _poinerPosition;
            //var results = new List<RaycastResult>();
            //_raycaster.Raycast(eventData, results);

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

            //PointerEventData eventData = new PointerEventData(_eventSystem);
            //eventData.position = _poinerPosition;
            //var results = new List<RaycastResult>();
            //_raycaster.Raycast(eventData, results);

            _selected.Clear();

            if (_currentListener != null)
            {
                _currentListener.Notify();
            }

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


        }

        private void OnInputTrackingPerformed(InputAction.CallbackContext obj)
        {
            _poinerPosition = obj.ReadValue<Vector2>();

            //PointerEventData eventData = new PointerEventData(_eventSystem);
            //eventData.position = _poinerPosition;
            //var results = new List<RaycastResult>();
            //_raycaster.Raycast(eventData, results);

            Ray ray = _camera.ScreenPointToRay(_poinerPosition);
            var hits = Physics.RaycastNonAlloc(ray, _raycastHits);
            if (hits == 0)
            {
                if (_current != null)
                {
                    UpdateInfo();
                    _current.OnPointerExit(_info);
                    _current = null;
                }
                return;
            }

            if (_raycastHits[0].transform.TryGetComponent(out IInteractable interactable))
            {
                UpdateInfo();
                if (interactable != _current)
                {
                    interactable.OnPointerEnter(_info);
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


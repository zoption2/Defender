using System;
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
    }


    public class InputService : IInputService
    {
        private Inputs _inputs;
        private Camera _camera;
        private InputSystemUIInputModule _inputModule;

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
            _inputs.Main.Tap.started += TapStarted;
            _inputs.Main.Tap.canceled += ctx => TapCanceled(ctx);
            _inputs.Main.Contact.started += ContactStarted;
            _inputs.Main.Contact.canceled += ContactCanceled;
            _inputs.Main.TouchPosition.started += ctx => TouchStarted(ctx);
        }

        private void TouchStarted(InputAction.CallbackContext obj)
        {
            Debug.Log("Touch started");
            Ray ray = _camera.ScreenPointToRay(obj.ReadValue<Vector2>());
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.TryGetComponent<IClickable>(out IClickable clickable))
                {
                    clickable.OnClick();
                }
            }
        }


        private void ContactStarted(InputAction.CallbackContext obj)
        {
            Debug.Log("Contact started");
            //var worldPoint = _camera.ScreenToWorldPoint(obj.ReadValue<Vector2>());
        }

        private void ContactCanceled(InputAction.CallbackContext obj)
        {
            Debug.Log("Contact canceled");
        }


        private void TapStarted(InputAction.CallbackContext context)
        {
            Debug.Log("Tap started");

        }

        private void TapCanceled(InputAction.CallbackContext context)
        {
            Debug.Log("Tap canceled");
        }
    }

    public interface IClickable
    {
        void OnClick();
    }

}


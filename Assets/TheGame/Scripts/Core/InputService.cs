using System;
using TheGame.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheGame.Core
{
    public interface IInputService
    {
        void Initialize();
    }


    public class InputService : IInputService
    {
        private Inputs _inputs;

        public void Initialize()
        {
            _inputs = new Inputs();
            _inputs.Enable();
            BindInputs();
            Debug.Log("InputService inited");
        }

        private void BindInputs()
        {
            var map = _inputs.Main;
            _inputs.Main.Tap.started += ctx => TapStarted(ctx);
            _inputs.Main.Tap.canceled += ctx => TapCanceled(ctx);
            _inputs.Main.Contact.started += ctx => ContactStarted(ctx);
            _inputs.Main.TouchPosition.started += ctx => TouchStarted(ctx);
        }

        private void TouchStarted(InputAction.CallbackContext obj)
        {
            Debug.Log("Touch started");
        }

        private void ContactStarted(InputAction.CallbackContext obj)
        {
            Debug.Log("Contact started");
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
}


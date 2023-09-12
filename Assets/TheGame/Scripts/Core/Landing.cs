using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheGame.Core
{
    public class Landing : MonoBehaviour
    {
        private IInputService _inputService;

        private void Start()
        {
            _inputService = new InputService();
            _inputService.Initialize();
        }
    }
}


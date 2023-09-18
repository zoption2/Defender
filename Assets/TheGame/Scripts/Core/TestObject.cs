using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using System.Threading.Tasks;
using Services;
using Services.InputEvents;

namespace TheGame.Gameplay
{
    public class TestObject : MonoBehaviour, IInteractable, IInputTargetHandler
    {
        [SerializeField] private Renderer _renderer;
        [Inject] private readonly IInputService _inputService;
        private bool _isActivate;

        private void Start()
        {
            _renderer.material.color = Color.gray;
        }

        public void Select()
        {
            if (!_isActivate)
            {
                Debug.LogFormat("Click on {0} detected", nameof(TestObject));
                _renderer.material.color = Color.green;
            }
        }

        public async void Activate()
        {
            _isActivate = true;
            _renderer.material.color = Color.red;
            await Task.Delay(2000);
            UnSelect();
            gameObject.SetActive(false);
        }

        public void Highlight()
        {
            if (!_isActivate)
            {
                _renderer.material.color = Color.blue;
            }
        }

        public void Unhighlight()
        {
            if (!_isActivate)
            {
                _renderer.material.color = Color.gray;
            }
        }

        public void UnSelect()
        {
            _renderer.material.color = Color.gray;
        }

        public void OnPointerClick()
        {
            Debug.Log("Click detected");
            _inputService.RegisterClick(this);
        }

        public void OnPointerEnter()
        {
            Debug.Log("Enter detected");
            _inputService.RegisterEnter(this);
        }

        public void OnPointerExit()
        {
            Debug.Log("Exit detected");
            _inputService.RegisterExit(this);
        }

        public void OnInputTarget(InputInfo inputInfo)
        {
            throw new System.NotImplementedException();
        }

        public void OnInputTargetLost(InputInfo inputInfo)
        {
            throw new System.NotImplementedException();
        }
    }

}


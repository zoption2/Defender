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

        public async void ApproveSelected()
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

        public void OnInputTarget(InteractionInfo inputInfo)
        {
            throw new System.NotImplementedException();
        }

        public void OnInputTargetLost(InteractionInfo inputInfo)
        {
            throw new System.NotImplementedException();
        }

        public void RejectSelected()
        {
            throw new System.NotImplementedException();
        }

        public void Select(InteractionInfo info)
        {
            throw new System.NotImplementedException();
        }

        public void Deselect(InteractionInfo info)
        {
            throw new System.NotImplementedException();
        }
    }

}


using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using TheGame.Services;
using TheGame.Gameplay;

namespace TheGame.Core
{
    public class TestObject : MonoBehaviour, IInteractable, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Renderer _renderer;
        [Inject] private readonly IInputService _inputService;

        private void Start()
        {
            _renderer.material.color = Color.gray;
        }

        public void Select()
        {
            Debug.LogFormat("Click on {0} detected", nameof(TestObject));
            _renderer.material.color = Color.green;
        }

        public void Highlight()
        {
            _renderer.material.color = Color.blue;
        }

        public void Unhighlight()
        {
            _renderer.material.color = Color.gray;
        }

        public void UnSelect()
        {
            _renderer.material.color = Color.gray;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Click detected");
            _inputService.RegisterClick(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Enter detected");
            _inputService.RegisterEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Exit detected");
            _inputService.RegisterExit(this);
        }
    }

}


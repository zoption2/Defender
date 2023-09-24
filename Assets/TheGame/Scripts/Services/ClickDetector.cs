using UnityEngine;
using UnityEngine.EventSystems;

namespace Services
{
    public class ClickDetector : MonoBehaviour
        , IPointerDownHandler
        , IPointerEnterHandler
        , IPointerExitHandler
        , IPointerUpHandler
    {
        private OldInputService _inputSystem;

        public void Init(OldInputService inputSystem)
        {
            _inputSystem = inputSystem;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _inputSystem.OnPointerDown(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputSystem.OnPointerUp(eventData);
        }
    }
}


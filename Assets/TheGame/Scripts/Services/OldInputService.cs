using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Services
{
    public class OldInputService : IInputService
        , IPointerDownHandler
        , IPointerEnterHandler
        , IPointerExitHandler
        , IPointerUpHandler
    {
        public event Action OnInputPressed;
        public event Action OnInputUp;

        public void Initialize()
        {
            var canvasGO = new GameObject("ClickAnalizer");
            var canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
            canvasGO.AddComponent<GraphicRaycaster>();
            var scaler = canvasGO.AddComponent<CanvasScaler>();
            var image = canvasGO.AddComponent<Image>();
            image.rectTransform.anchorMax = Vector2.one;
            image.rectTransform.anchorMin = Vector2.zero;
            image.rectTransform.sizeDelta = Vector2.zero;
            image.maskable = false;
            image.sprite = null;
            image.color = Vector4.zero;
            var clickDetector = canvasGO.AddComponent<ClickDetector>();
            clickDetector.Init(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Input Down detected!");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Input Up detected!");
            OnInputUp?.Invoke();
        }

        public void SetCurrentListener(IInputListener listener)
        {
            throw new NotImplementedException();
        }
    }
}


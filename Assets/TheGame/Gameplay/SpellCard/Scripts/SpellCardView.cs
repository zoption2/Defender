using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Services.InputEvents;
using TMPro;


namespace Gameplay
{
    public interface ISpellCardView : IView
    {

    }

    public class SpellCardView : MonoBehaviour
        , ISpellCardView
        , IPointerDownHandler
        , IPointerUpHandler
        , IPointerEnterHandler
        , IPointerExitHandler
        , IInteractable
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private TMP_Text _text;
        private ISpellCardInputs _inputsHandler;
        private Color _originColor;

        public void Init(ISpellCardInputs inputsHandler, Color color)
        {
            _inputsHandler = inputsHandler;
            _originColor = color;
            _renderer.color = color;
        }

        public void Show(Action onShow)
        {
            throw new NotImplementedException();
        }

        public void Hide(Action onHide)
        {
            gameObject.SetActive(false);
            onHide?.Invoke();
        }

        public void Release()
        {
            throw new NotImplementedException();
        }

        public void Highlight()
        {
            _renderer.color = Color.yellow;
        }

        public void Unhighlight()
        {
            _renderer.color = _originColor;
        }

        public void Select(int number)
        {
            _renderer.color = Color.green;
            string value = number.ToString();
            _text.text = value;
        }

        public void Deselect()
        {
            _renderer.color = _originColor;
            _text.text = "";
        }

        public void Activate()
        {
            _renderer.color = Color.red;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _inputsHandler.OnPointerEnter(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _inputsHandler.OnPointerExit(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _inputsHandler.OnPointerDown(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputsHandler.OnPointerUp(eventData);
        }

        public void ApproveSelected()
        {
            throw new NotImplementedException();
        }

        public void RejectSelected()
        {
            throw new NotImplementedException();
        }

        public void OnPointerEnter(InteractionInfo info)
        {
            _inputsHandler.OnPointerEnter(info);
        }

        public void OnPointerExit(InteractionInfo info)
        {
            _inputsHandler.OnPointerExit(info);
        }

        public void OnPointerDown(InteractionInfo info)
        {
            _inputsHandler.OnPointerDown(info);
        }

        public void OnPointerUp(InteractionInfo info)
        {
            _inputsHandler.OnPointerUp(info);
        }
    }
}


using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using TheGame.Services;

namespace TheGame.Gameplay
{
    public interface ISpellCardView : IView
    {

    }

    public class SpellCardView : MonoBehaviour
        , ISpellCardView
        , IPointerClickHandler
        , IPointerEnterHandler
        , IPointerExitHandler
        , IInteractable
    {
        [SerializeField] private Image _icon;
        private ISpellCardInputs _inputsHandler;

        public void Init(ISpellCardInputs inputsHandler)
        {
            _inputsHandler = inputsHandler;
        }

        public void Show(Action onShow)
        {
            throw new NotImplementedException();
        }

        public void Hide(Action onHide)
        {
            throw new NotImplementedException();
        }

        public void Release()
        {
            throw new NotImplementedException();
        }

        public void Highlight()
        {
            _icon.color = Color.yellow;
        }

        public void Unhighlight()
        {
            _icon.color = Color.gray;
        }

        public void Select()
        {
            _icon.color = Color.green;
        }

        public void UnSelect()
        {
            _icon.color = Color.gray;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _inputsHandler.OnClick();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _inputsHandler.OnPointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _inputsHandler.OnPointerExit();
        }
    }


    public class SpellCardModel : IModel
    {

    }

    public interface ISpellCardController
    {

    }

    public interface ISpellCardInputs
    {
        void OnClick();
        void OnPointerEnter();
        void OnPointerExit();
    }

    public class SpellCardController 
        : BaseController<SpellCardView, SpellCardModel>
        , ISpellCardController
        , ISpellCardInputs
    {
        private readonly IInputService _inputService;

        public SpellCardController(IInputService inputService)
        {
            _inputService = inputService;
        }


        public void OnClick()
        {
            _inputService.RegisterClick(_view);
        }

        public void OnPointerEnter()
        {
            _inputService.RegisterEnter(_view);
        }

        public void OnPointerExit()
        {
            _inputService.RegisterExit(_view);
        }


        protected override void DoOnInit()
        {
            base.DoOnInit();
            _view.Init(this);
        }
    }
}


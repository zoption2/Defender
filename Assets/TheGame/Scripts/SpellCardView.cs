using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Services;
using Core;
using Services.InputEvents;

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

        public void Deselect()
        {
            _icon.color = Color.gray;
        }

        public void Activate()
        {
            _icon.color = Color.red;
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
    }



    public interface ISpellCardModel : IModel
    {
        void Activate();
        void Disactivate();
    }

    public class SpellCardModel : IModel
    {
        public void Activate()
        {

        }
    }

    public interface ISpellCardController
    {
        void Activate();
        void Prepare();
        void Chill();
    }

    public interface ISpellCardInputs : IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {

    }

    public class SpellCardController 
        : BaseController<SpellCardView, SpellCardModel>
        , ISpellCardController
        , ISpellCardInputs
        , IInputListener
    {
        private readonly IInputService _inputService;
        private readonly ISpellCardMediator _mediator;
        private bool _isSelectable;
        private bool _isSelected;

        public SpellCardController(IInputService inputService, ISpellCardMediator mediator)
        {
            _inputService = inputService;
            _mediator = mediator;
            _isSelectable = true;
        }


        protected override void DoOnInit()
        {
            base.DoOnInit();
            _view.Init(this);
        }

        public void Activate()
        {
            _view.Activate();
            _isSelectable = false;
        }

        public void Prepare()
        {
            _isSelected = true;
            _view.Select();
        }

        public void Chill()
        {
            _isSelectable = true;
            _isSelected = false;
            _view.Deselect();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isSelectable)
            {
                return;
            }

            if (eventData.eligibleForClick)
            {
                _mediator.AddSpellCard(this);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isSelectable)
            {
                return;
            }

            if (eventData.eligibleForClick)
            {
                _mediator.AddSpellCard(this);
            }
            else
            {
                _view.Highlight();
            }
            _inputService.SetCurrentListener(null);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isSelected)
            {
                _inputService.SetCurrentListener(this);
            }

            if (eventData.eligibleForClick)
            {
                
            }
            else
            {
                _view.Unhighlight();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _mediator.ConfirmActivation(this);
        }

        public void Notify()
        {
            _mediator.CancelActivation(this);
            _inputService.SetCurrentListener(null);
        }
    }
}


using UnityEngine.EventSystems;
using Services;
using Core;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Services.InputEvents;

namespace Gameplay
{
    public interface ISpellCardController
    {
        void Activate();
        void Disactivate();
        void Prepare(int number);
        void Chill();
        void SetPosition(Vector2 position, System.Action callback);
    }

    public interface ISpellCardInputs : IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IInteractable
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
            Color color = _model.Data.Element == ElementType.Fire ? Color.magenta : Color.blue;
            _view.Init(this, color);
        }

        public async void Activate()
        {
            _view.Activate();
            _isSelectable = false;
            await UniTask.Delay(1000);
            _view.Hide(null);
        }

        public void Disactivate()
        {
            _isSelectable = false;
        }

        public void Prepare(int number)
        {
            _isSelected = true;
            _view.Select(number);
        }

        public void Chill()
        {
            _isSelectable = true;
            _isSelected = false;
            _view.Deselect();
        }

        public void SetPosition(Vector2 position, System.Action callback)
        {

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
            _inputService.SetCurrentListener(null);
        }

        public void Notify()
        {
            _mediator.CancelActivation(this);
            _inputService.SetCurrentListener(null);
        }

        public void ApproveSelected()
        {
            throw new System.NotImplementedException();
        }

        public void RejectSelected()
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerEnter(InteractionInfo info)
        {
            if (!_isSelectable)
            {
                return;
            }

            if (info.IsPressed)
            {
                _mediator.AddSpellCard(this);
            }
            else
            {
                _view.Highlight();
            }
            _inputService.SetCurrentListener(null);
        }

        public void OnPointerExit(InteractionInfo info)
        {
            if (_isSelected)
            {
                _inputService.SetCurrentListener(this);
            }

            if (info.IsPressed)
            {

            }
            else
            {
                _view.Unhighlight();
            }
        }

        public void OnPointerDown(InteractionInfo info)
        {
            if (!_isSelectable)
            {
                return;
            }

            if (info.IsPressed)
            {
                _mediator.AddSpellCard(this);
            }
        }

        public void OnPointerUp(InteractionInfo info)
        {
            _mediator.ConfirmActivation(this);
            _inputService.SetCurrentListener(null);
        }
    }
}


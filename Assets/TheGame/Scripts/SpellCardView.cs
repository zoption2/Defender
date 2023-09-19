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

        public void Deselect()
        {
            _icon.color = Color.gray;
        }

        public void ApproveSelected()
        {
            _inputsHandler.OnApproveSelection();
        }

        public void RejectSelected()
        {
            _inputsHandler.OnRejectSelection();
        }

        public void Select(InteractionInfo info)
        {
            _inputsHandler.OnSelect(info);
        }

        public void Deselect(InteractionInfo info)
        {
            _inputsHandler.OnDeselect(info);
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

    public interface ISpellCardInputs
    {
        public void OnApproveSelection();
        public void OnRejectSelection();
        public void OnSelect(InteractionInfo info);
        public void OnDeselect(InteractionInfo info);
    }

    public class SpellCardController 
        : BaseController<SpellCardView, SpellCardModel>
        , ISpellCardController
        , ISpellCardInputs
    {
        private readonly IInputService _inputService;
        private readonly ISpellCardMediator _mediator;
        private bool _isSelectable;

        public SpellCardController(IInputService inputService, ISpellCardMediator mediator)
        {
            _inputService = inputService;
            _mediator = mediator;
        }

        public void OnApproveSelection()
        {
            _mediator.ConfirmActivation();
        }

        public void OnRejectSelection()
        {
            _mediator.CancelActivation();
        }

        public void OnSelect(InteractionInfo info)
        {
            if (!_isSelectable)
            {
                return;
            }

            if (info.IsPressed)
            {
                _view.Select();
            }
            else
            {
                _view.Highlight();
            }
        }

        public void OnDeselect(InteractionInfo info)
        {
            if (info.IsPressed)
            {
                
            }
            else
            {
                _view.Unhighlight();
            }
        }

        protected override void DoOnInit()
        {
            base.DoOnInit();
            _view.Init(this);
        }

        public void Activate()
        {
            Debug.Log("Activation detected!");
        }

        public void Prepare()
        {
            _view.Select();
        }

        public void Chill()
        {
            _view.Deselect();
        }
    }
}


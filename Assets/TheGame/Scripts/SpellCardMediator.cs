using System.Collections.Generic;

namespace Gameplay
{
    public interface ISpellCardMediator
    {
        void Initialize();
        void ConfirmActivation();
        void CancelActivation();
        void AddSpellCard(ISpellCardController interactable);
        void RemoveSpellCard(ISpellCardController interactable);
    }


    public class SpellCardMediator : ISpellCardMediator
    {
        private Stack<ISpellCardController> _selected = new();


        public void Initialize()
        {

        }

        public void AddSpellCard(ISpellCardController interactable)
        {
            if (!_selected.Contains(interactable))
            {
                _selected.Push(interactable);
                interactable.Prepare();
            }
        }

        public void RemoveSpellCard(ISpellCardController interactable)
        {
            if (_selected.TryPeek(out ISpellCardController lastSpellCard))
            {
                if(lastSpellCard.Equals(interactable))
                {
                    interactable.Chill();
                    _selected.Pop();
                }
            }
        }

        public void ConfirmActivation()
        {
            for (int i = 0, j = _selected.Count; i < j; i++)
            {
                var spellCard = _selected.Pop();
                spellCard.Activate();
            }
        }

        public void CancelActivation()
        {
            for (int i = 0, j = _selected.Count; i < j; i++)
            {
                var spellCard = _selected.Pop();
                spellCard.Chill();
            }
        }
    }
}


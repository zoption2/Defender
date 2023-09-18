namespace Gameplay
{
    public interface ISpellCardBuilder
    {
        void AddCard(ISpellCardController spellCard);
        void RemoveCard(ISpellCardController spellCard);
    }


    public class SpellCardBuilder : ISpellCardBuilder
    {
        public void AddCard(ISpellCardController spellCard)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveCard(ISpellCardController spellCard)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterClick(IInteractable interactable)
        {
            if (!_selected.Contains(interactable))
            {
                _selected.Add(interactable);
                interactable.Select();
            }
        }

        public void RegisterEnter(IInteractable interactable)
        {
            if (_isInputPressed)
            {
                if (!_selected.Contains(interactable))
                {
                    _selected.Add(interactable);
                    interactable.Select();
                }
            }
            else
            {
                interactable.Highlight();
            }
            _currentTarget = interactable;
        }
    }
}


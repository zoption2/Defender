using System.Collections.Generic;


namespace Gameplay
{
    public interface ISpellCardMediator
    {
        bool IsInitSpellSelected { get; }
        void Initialize();

        void ConfirmActivation(ISpellCardController controller);
        void CancelActivation(ISpellCardController controller);
        void AddSpellCard(ISpellCardController controller);
        void RemoveSpellCard(ISpellCardController controller);
    }


    public class SpellCardMediator : ISpellCardMediator
    {
        private bool _isInitSpellSelected;
        private List<ISpellCardController> _selected = new();

        public bool IsInitSpellSelected => _isInitSpellSelected;


        public void Initialize()
        {

        }

        public void AddSpellCard(ISpellCardController controller)
        {
            int selectedCount = _selected.Count;
            if (!_selected.Contains(controller))
            {
                _selected.Add(controller);
                controller.Prepare(selectedCount + 1);
                UnityEngine.Debug.LogFormat("Controller added! ToTal count: {0}", _selected.Count);
            }
            else if(selectedCount > 1)
            {
                if (_selected[^2].Equals(controller))
                {
                    var lastController = _selected[^1];
                    lastController.Chill();
                    _selected.Remove(lastController);
                    UnityEngine.Debug.LogFormat("Controller Removed! ToTal count: {0}", _selected.Count);
                }
            }

            selectedCount = _selected.Count;
            if (selectedCount == 1)
            {
                _isInitSpellSelected = true;
            }
        }

        public void RemoveSpellCard(ISpellCardController controller)
        {
            if (_selected.Contains(controller))
            {
                _selected.Remove(controller);
                UnityEngine.Debug.LogFormat("Controller Removed! ToTal count: {0}", _selected.Count);
            }

            int selectedCount = _selected.Count;
            if (selectedCount == 0)
            {
                _isInitSpellSelected = false;
            }
        }

        public void ConfirmActivation(ISpellCardController controller)
        {
            if (_selected.Contains(controller))
            {
                ActivateSpell();
            }
            _selected.Clear();
            _isInitSpellSelected = false;
        }

        public void CancelActivation(ISpellCardController controller)
        {
            for (int i = _selected.Count - 1, j = -1; i > j; i--)
            {
                _selected[i].Chill();
            }

            _selected.Clear();
            _isInitSpellSelected = false;
        }

        private void ActivateSpell()
        {
            for (int i = 0, j = _selected.Count; i < j; i++)
            {
                _selected[i].Activate();
            }
            UnityEngine.Debug.Log("Spell activated!");
        }
    }
}


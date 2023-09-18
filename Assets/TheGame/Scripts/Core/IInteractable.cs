namespace Gameplay
{
    public interface IInteractable
    {
        void Activate();
        void Highlight();
        void Unhighlight();
        void Select();
        void UnSelect();
    }

}


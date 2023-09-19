namespace Services.InputEvents
{
    public interface IInteractable
    {
        void ApproveSelected();
        void RejectSelected();
        void Select(InteractionInfo info);
        void Deselect(InteractionInfo info);
    }

}


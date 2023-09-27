namespace Services.InputEvents
{
    public interface IInteractable
    {
        void ApproveSelected();
        void RejectSelected();
        void OnPointerEnter(InteractionInfo info);
        void OnPointerExit(InteractionInfo info);
        void OnPointerDown(InteractionInfo info);
        void OnPointerUp(InteractionInfo info);
    }

}


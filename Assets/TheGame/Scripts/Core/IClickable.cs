using UnityEngine;

namespace Services.InputEvents
{
    public interface IInputClickHandler
    {
        void OnClick();
    }

    public interface IInputPressHandler
    {
        public void OnInputPress();
        public void OnInputRelease();
    }

    public interface IInputTargetHandler
    {
        void OnInputTarget(InteractionInfo inputInfo);
        void OnInputTargetLost(InteractionInfo inputInfo);
    }

    public struct InteractionInfo
    {
        public Vector2 ScreenPosition;
        public bool IsPressed;
    }
}



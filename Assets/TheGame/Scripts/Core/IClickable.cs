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
        void OnInputTarget(InputInfo inputInfo);
        void OnInputTargetLost(InputInfo inputInfo);
    }

    public struct InputInfo
    {
        public Vector2 ScreenPosition;
        public bool IsPressed;
    }
}



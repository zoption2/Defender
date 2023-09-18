using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public interface IView
{
    void Show(Action onShow);
    void Hide(Action onHide);
    void Release();
}

public interface IController<TView, TModel> : IView
{
    UniTask Init(TView view, TModel model);
}

public interface IPopupInitialization : IView
{
    UniTask InitPopup(Camera camera, Transform parent, int orderLayer = 0);
}

public interface IPopupMediator
{
    public event Action ON_CLOSE;
    void CreatePopup(Action onComplete = null);
    void ClosePopup(Action onComplete = null);
}


public interface IModel
{

}




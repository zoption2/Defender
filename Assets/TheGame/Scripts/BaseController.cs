using Cysharp.Threading.Tasks;
using System;

namespace Core
{
    public abstract class BaseController<TView, TModel> 
        : IController<TView, TModel> where TView : IView where TModel : IModel
    {
        protected TView _view;
        protected TModel _model;

        public async UniTask Init(TView view, TModel model)
        {
            _view = (TView)view;
            _model = (TModel)model;
            DoOnInit();
            await UniTask.CompletedTask;
        }

        protected virtual void DoOnInit()
        { }

        public void Show(Action onShow)
        {
            _view.Show(onShow);
        }

        public void Hide(Action onHide)
        {
            _view.Hide(onHide);
        }

        public void Release()
        {
            _view.Release();
        }
    }
}


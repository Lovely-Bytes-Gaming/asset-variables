using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LovelyBytes.AssetVariables.Samples
{
    public class MvcController<TModel, TView> : MonoBehaviour
        where TModel : IMvcModel<TView>
        where TView : IMvcView<TModel>
    {
        [SerializeField] private TModel _model;
        [SerializeField] private TView _view;

        protected virtual void Awake()
        {
            _view.Bind(_model);
            _model.Bind(_view);
        }

        protected virtual void OnDestroy()
        {
            _model.Release(_view);
            _view.Release(_model);
        }
    }

    public interface IMvcView<in TModel>
    {
        void Bind(TModel model);
        void Release(TModel model);
    }

    public interface IMvcModel<in TView>
    {
        void Bind(TView view);
        void Release(TView view);
    }
}



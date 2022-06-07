using UnityEngine;

namespace ScaredBug.Cursor
{
    public class CursorPresenter
    {
        private ICursorModel _model;
        private ICursorView _view;

        public CursorPresenter(ICursorModel model, ICursorView view)
        {
            _model = model;
            _view = view;
            _model.OnPositionChanged.AddListener(OnPositionChanged);
            _model.OnRadiusChanged.AddListener(OnRadiusChanged);
            _view.OnMouseMove.AddListener(OnMouseMove);
            _view.SetRadius(_model.radius);
            OnMouseMove(Input.mousePosition);
        }

        private void OnMouseMove(Vector2 mousePosition)
        {
            if (_model != null)
            {
                Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition);
                position.z = 0f;
                _model.position = position;
            }
        }

        private void OnPositionChanged(Vector3 position)
        {
            if (_view != null)
                _view.SetPosition(position);
        }

        private void OnRadiusChanged(float radius)
        {
            if (_view != null)
                _view.SetRadius(radius);
        }
    }
}

using UnityEngine;
using UnityEngine.Events;

namespace ScaredBug.Cursor
{
    public interface ICursorView
    {
        void SetRadius(float radius);
        void SetPosition(Vector3 position);
        UnityEvent<Vector2> OnMouseMove { get; }
    }
}

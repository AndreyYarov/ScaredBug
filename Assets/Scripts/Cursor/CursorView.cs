using UnityEngine;
using UnityEngine.Events;

namespace ScaredBug.Cursor
{
    public class CursorView : MonoBehaviour, ICursorView
    {
        public void SetRadius(float radius)
        {
            transform.localScale = new Vector3(radius, radius, radius);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public UnityEvent<Vector2> OnMouseMove { get; } = new UnityEvent<Vector2>();

        private void Update()
        {
            if (Input.GetAxis("Mouse X") != 0f || Input.GetAxis("Mouse Y") != 0f)
                OnMouseMove.Invoke(Input.mousePosition);
        }
    }
}

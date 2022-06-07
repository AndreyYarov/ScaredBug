using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScaredBug.Cursor
{
    [Serializable]
    public class CursorModel
    {
        [SerializeField] private float m_Radius;
        public float radius
        {
            get => m_Radius;
            set
            {
                if (value != m_Radius && value > 0f)
                {
                    m_Radius = value;
                    OnRadiusChanged.Invoke(value);
                }
            }
        }

        private Vector3 _position;
        public Vector3 position
        {
            get => _position;
            set
            {
                if (value != _position)
                {
                    _position = value;
                    OnPositionChanged.Invoke(value);
                }
            }
        }

        public UnityEvent<Vector3> OnPositionChanged { get; } = new UnityEvent<Vector3>();
        public UnityEvent<float> OnRadiusChanged { get; } = new UnityEvent<float>();
    }
}

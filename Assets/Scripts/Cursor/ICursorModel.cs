using UnityEngine;
using UnityEngine.Events;

namespace ScaredBug.Cursor
{
    public interface ICursorModel
    {
        float radius { get; set; }
        Vector3 position { get; set; }
        UnityEvent<float> OnRadiusChanged { get; }
        UnityEvent<Vector3> OnPositionChanged { get; }
    }
}

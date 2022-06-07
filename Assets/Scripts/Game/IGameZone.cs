using UnityEngine;

namespace ScaredBug.Game
{
    public interface IGameZone
    {
        Rect rect { get; }
        Vector3 start { get; }
        Vector3 end { get; }
    }
}

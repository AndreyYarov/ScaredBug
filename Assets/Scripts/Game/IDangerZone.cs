using UnityEngine;

namespace ScaredBug.Game
{
    public interface IDangerZone
    {
        float radius { get; }
        Vector3 position { get; }
    }
}

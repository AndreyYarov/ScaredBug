using UnityEngine.Events;

namespace ScaredBug.UI
{
    public interface ILevelMenu
    {
        bool Visible { get; set; }
        void SetOnPlayButtonClickCallback(UnityAction callback);
    }
}

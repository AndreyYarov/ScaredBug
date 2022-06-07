using UnityEngine;
using UnityEngine.Events;

namespace ScaredBug.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private FadeScreen m_FadeScreen;
        [SerializeField] private LevelMenu m_LevelMenu;

        public void SetFade(bool fade, float delay) => m_FadeScreen?.SetFade(fade, delay);

        public void ShowLevelMenu(UnityAction OnPlayButtonClick)
        {
            m_LevelMenu.Visible = true;
            m_LevelMenu.SetOnPlayButtonClickCallback(OnPlayButtonClick);
        }

        public void HideLevelMenu() => m_LevelMenu.Visible = false;
    }
}

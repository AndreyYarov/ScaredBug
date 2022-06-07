using System.Collections;
using UnityEngine;
using ScaredBug.UI;

namespace ScaredBug.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager m_UIManager;
        [SerializeField] private LevelManager m_LevelManager;

        private IEnumerator Start()
        {
            m_UIManager.SetFade(true, 0f);

            while (true)
            {
                m_UIManager.HideLevelMenu();
                m_UIManager.SetFade(false, 1f);
                yield return m_LevelManager.Play();
                m_UIManager.SetFade(true, 1f);
                yield return new WaitForSeconds(1f);
                bool waiting = true;
                m_UIManager.ShowLevelMenu(() => waiting = false);
                while (waiting)
                    yield return null;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ScaredBug.UI
{
    class LevelMenu : MonoBehaviour, ILevelMenu
    {
        [SerializeField] private Button m_PlayButton;

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void SetOnPlayButtonClickCallback(UnityAction callback)
        {
            m_PlayButton?.onClick.AddListener(callback);
        }
    }
}

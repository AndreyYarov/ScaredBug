using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ScaredBug.UI
{
    class FadeScreen : MonoBehaviour, IFadeScreen
    {
        [SerializeField] private Image m_Fade;

        public void SetFade(bool fade, float delay = 1f)
        {
            StartCoroutine(FadeTransition(fade ? 1f : 0f, delay));
        }

        private IEnumerator FadeTransition(float end, float duration)
        {
            Color c = m_Fade.color;
            float start = c.a;

            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                c.a = Mathf.Lerp(start, end, t / duration);
                m_Fade.color = c;
                yield return null;
            }
            c.a = end;
            m_Fade.color = c;
        }
    }
}

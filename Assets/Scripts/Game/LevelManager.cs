using System.Collections;
using UnityEngine;
using ScaredBug.Cursor;
using ScaredBug.Character;
using CharacterController = ScaredBug.Character.CharacterController;

namespace ScaredBug.Game
{
    public class LevelManager : MonoBehaviour, IGameZone
    {
        [SerializeField] private CursorModel m_CursorModel;
        [SerializeField] private CursorView m_CursorView;
        [SerializeField] private Vector2 m_StartOffset, m_EndOffset;
        [SerializeField] private Transform m_StartFlagTransform, m_EndFlagTransform;
        [SerializeField] private CharacterView m_CharacterView;

        private Rect _rect;
        public Rect rect => _rect;
        public Vector3 start => m_StartFlagTransform.position;
        public Vector3 end => m_EndFlagTransform.position;

        public IEnumerator Play()
        {
            new CursorPresenter(m_CursorModel, m_CursorView);
            m_CharacterView.transform.position = m_StartFlagTransform.position;
            CharacterController controller = new CharacterController(m_CharacterView, this, m_CursorModel);
            yield return controller.Play();
        }
        private void Awake()
        {
            Vector2 extents = Camera.main.ViewportToWorldPoint(Vector2.one);
            _rect = new Rect(-extents, extents * 2);
            m_StartFlagTransform.position = _rect.max - m_StartOffset;
            m_EndFlagTransform.position = _rect.min + m_EndOffset;
        }

        #region testing

        private void Start()
        {
            StartCoroutine(Play());
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isPlaying)
                m_CursorModel.OnRadiusChanged.Invoke(m_CursorModel.radius);
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(rect.center, rect.size);
                Gizmos.DrawSphere(m_StartFlagTransform.position, 0.25f);
                Gizmos.DrawSphere(m_EndFlagTransform.position, 0.25f);
            }
        }
#endif
        #endregion
    }
}

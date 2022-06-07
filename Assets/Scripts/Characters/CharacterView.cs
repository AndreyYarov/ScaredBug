using System;
using System.Collections;
using UnityEngine;

namespace ScaredBug.Character
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;
        [SerializeField] private float m_Speed = 1f;
        [SerializeField] private float m_MinAnimateFlipAngle = 3f;
        [SerializeField] private float m_FlipAngleSpeed = 90f;

        private void Awake()
        {
            if (!m_Animator)
                m_Animator = GetComponent<Animator>();
        }

        public void SetDestination(Vector3 destination)
        {
            StopAllCoroutines();
            StartCoroutine(MoveAsync(destination));
        }

        private IEnumerator MoveAsync(Vector3 destination)
        {
            Vector3 dir = destination - transform.position;
            float distance = dir.magnitude;
            if (distance == 0f)
                yield break;

            bool canMove = false;
            m_Animator.SetBool("Walk", true);
            StartCoroutine(RotateAsync(dir / distance, () => canMove = true));
            while (!canMove)
                yield return null;
            while (transform.position != destination)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, m_Speed * Time.deltaTime);
                yield return null;
            }
            m_Animator.SetBool("Walk", false);
        }

        public void RotateTo(Vector3 direction)
        {
            StopAllCoroutines();
            StartCoroutine(RotateAsync(direction, null));
        }

        private IEnumerator FlipReturn(float sign)
        {
            float flip = m_Animator.GetFloat("Flip");
            while (flip != -sign && flip != 0f)
            {
                yield return null;
                flip = m_Animator.GetFloat("Flip");
            }
            if (flip != 0f)
            {
                m_Animator.SetTrigger("Rotation-End");
                while (m_Animator.GetFloat("Flip") != 0f)
                    yield return null;
            }
        }

        private IEnumerator RotateAsync(Vector3 direction, Action OnCanMove)
        {
            if (direction == -transform.up)
            {
                if (m_Animator.GetFloat("Flip") != 0f)
                    m_Animator.SetTrigger("Rotation-End");
                yield break;
            }

            float angle = Vector3.SignedAngle(-transform.up, direction, Vector3.forward);
            float sign = Mathf.Sign(angle);
            Quaternion target = Quaternion.LookRotation(Vector3.forward, -direction);
            if (angle * sign < m_MinAnimateFlipAngle)
            {
                yield return FlipReturn(sign);
                OnCanMove?.Invoke();
                while (transform.rotation != target)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, target, m_FlipAngleSpeed * Time.deltaTime);
                    yield return null;
                }
            }
            else
            {
                if (m_Animator.GetFloat("Flip") * sign < 0f)
                    yield return FlipReturn(sign);
                if (m_Animator.GetFloat("Flip") == 0f)
                    m_Animator.SetTrigger(sign < 0 ? "Rotate-Right" : "Rotate-Left");
                while (m_Animator.GetFloat("Flip") != sign)
                    yield return null;
                while (transform.rotation != target)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, target, m_FlipAngleSpeed * Time.deltaTime);
                    yield return null;
                }
                m_Animator.SetTrigger("Rotation-End");
                while (m_Animator.GetFloat("Flip") != 0f)
                    yield return null;
                OnCanMove?.Invoke();
            }
        }

        public void Stop()
        {
            StopAllCoroutines();
            if (m_Animator.GetFloat("Flip") != 0f)
                m_Animator.SetTrigger("Rotation-End");
            m_Animator.SetBool("Walk", false);
        }

        #region testing
        //private Vector3 destination;

        //private void Start()
        //{
        //    destination = transform.position;
        //}

        //private void Update()
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        destination.z = 0f;
        //        SetDestination(destination);
        //    }
        //}

        //private void OnDrawGizmos()
        //{
        //    if (Application.isPlaying)
        //    {
        //        Gizmos.color = Color.red;
        //        Gizmos.DrawLine(transform.position, destination);
        //    }
        //}
        #endregion
    }
}

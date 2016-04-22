using UnityEngine;
using System.Collections;
namespace sunjiahaoz.AnimEffect
{

    public class EyeLookAnim : MonoBehaviour
    {

        public Transform m_LookTarget;
        public GameObject m_Pupil;
        public float m_EyeRadiusX = 1.0f;
        public float m_EyeRadiusY = 1.0f;

        Vector3 m_LookDirection;
        Vector3 m_PupilCentre;
        Vector3 m_PupilInitPosition;

        void Start()
        {
            m_PupilCentre = m_Pupil != null ? m_Pupil.transform.localPosition : Vector3.zero;
            m_PupilInitPosition = m_Pupil != null ? m_Pupil.transform.position : Vector3.zero;
        }


        float lookFilterFactor = 0.1f;
        Vector3 desiredLookDirection;
        void Update()
        {
            if (m_LookTarget != null)
            {
                desiredLookDirection = m_LookTarget.transform.position - m_PupilInitPosition;
            }
            else
            {
                desiredLookDirection = Vector2.zero;
            }

            desiredLookDirection.Normalize();

            m_LookDirection.x = (m_LookDirection.x * (1.0f - lookFilterFactor)) + (desiredLookDirection.x * lookFilterFactor);
            m_LookDirection.y = (m_LookDirection.y * (1.0f - lookFilterFactor)) + (desiredLookDirection.y * lookFilterFactor);

            Vector3 dir = m_LookDirection;
            dir.x *= m_EyeRadiusX;
            dir.y *= m_EyeRadiusY;
            if (m_Pupil != null)
            {
                m_Pupil.transform.localPosition = m_PupilCentre + (dir);
            }
        }

#if UNITY_EDITOR

    public bool _bDrawGizmo = true;
    void OnDrawGizmos()
    {
        if (!_bDrawGizmo)
        {
            return;
        }
        if (m_Pupil != null)
        {
            Gizmos.color = Color.yellow;
            if (Application.isPlaying)
            {
                Gizmos.DrawCube(m_PupilInitPosition, new Vector3(m_EyeRadiusX * 2, m_EyeRadiusY * 2, 0));
            }
            else
            {
                Gizmos.DrawCube(m_Pupil.transform.position, new Vector3(m_EyeRadiusX * 2, m_EyeRadiusY * 2, 0));
            }
        }
    }
#endif
    }
}

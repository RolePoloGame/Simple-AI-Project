using Assets.Scripts;
using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class FaceCamera : EnhancedBehaviour
    {
        #region Properties & Fields
        private Transform m_Camera;
        [SerializeField]
        private int m_OnceEveryTick = 1; // FaceCamera doesn't have be updated every tick
        private int m_Counter = 0;
        #endregion

        #region Unity methods
        private void OnEnable() => LookAtCamera();

        private void Update()
        {
            if (m_Counter > 0)
            {
                m_Counter--;
                return;
            }
            LookAtCamera();
            m_Counter = m_OnceEveryTick;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Rotates this transform so it looks at <see cref="m_Camera"/>
        /// </summary>
        private void LookAtCamera()
        {
            transform.LookAt(transform.position + GetCamera().transform.rotation * Vector3.forward);
        }
        /// <summary>
        /// Returns stashed camera's transform or gets it from a <see cref="CameraManager"/>
        /// </summary>
        /// <returns></returns>
        private Transform GetCamera()
        {
            if (m_Camera == null)
            {
                m_Camera = CameraManager.Instance.Camera.transform;
            }
            return m_Camera;
        }
        #endregion
    }
}
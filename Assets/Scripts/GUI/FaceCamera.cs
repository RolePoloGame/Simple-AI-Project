using UnityEngine;

public class FaceCamera : EnhancedBehaviour
{
    #region Properties & Fields
    private Transform m_Camera;
    [SerializeField]
    // FaceCamera doesn't have be updated every tick. Reducing calls for less performance impact
    private int m_OnceEveryTick = 30;
    private int m_Counter = 0;
    #endregion

    #region Unity methods
    void OnEnable()
    {
        LookAtCamera();
    }

    void Update()
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

    private void LookAtCamera()
    {
        transform.LookAt(transform.position + GetCamera().transform.rotation * Vector3.forward);
    }
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

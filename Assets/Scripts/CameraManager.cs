using Assets.Scripts.Core;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : Singleton<CameraManager>
{
    #region Properties & Fields
    private const int PRIORITY_CAMERA = 20;
    private const int DEFAULT_CAMERA = 10;
    private Camera m_Camera;
    [SerializeField]
    private CinemachineVirtualCamera[] m_Cameras;
    [SerializeField]
    private CinemachineVirtualCamera m_CurrentCamera;
    #endregion

    #region Public Methods
    public Camera Camera => m_Camera == null ? m_Camera = GetComponent<Camera>() : m_Camera;

    public void SwitchCamera(int index)
    {
        if (m_Cameras == null || m_Cameras.Length == 0)
            return;
        if (index < 0 || index >= m_Cameras.Length)
            return;

        CinemachineVirtualCamera newCurrentCamera = m_Cameras[index];
        newCurrentCamera.m_Priority = PRIORITY_CAMERA;
        if (m_CurrentCamera != null)
            m_CurrentCamera.m_Priority = DEFAULT_CAMERA;
        m_CurrentCamera = newCurrentCamera;
    }
    #endregion
}

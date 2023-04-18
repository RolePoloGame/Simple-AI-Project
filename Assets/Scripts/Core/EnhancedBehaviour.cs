using UnityEngine;

public class EnhancedBehaviour : MonoBehaviour
{
    #region Properties & Fields
    [SerializeField]
    protected bool m_IsDebugging = false;
    protected bool m_IsSuppressingErrors = false
    #endregion
;
    #region Private & Protected Methods

    /// <summary>
    /// Displays a message in Console when <see cref="m_IsDebugging"/> is set to true. Colors the message green
    /// </summary>
    /// <param name="message">A message to be sent to Unity Console</param>
    protected void LogSuccess(string message)
    {
        if (!m_IsDebugging) return;
        Debug.Log($"{name.Bold()}: {message.Color(Color.green)}");
    }

    /// <summary>
    /// Displays a message in Console when <see cref="m_IsDebugging"/> is set to true. Informs which object is null
    /// </summary>
    /// <param name="message">A message to be sent to Unity Console</param>
    protected void LogNullError(object gameobject)
    {
        if (!m_IsDebugging) return;
        Debug.Log($"{name.Bold()}: {gameobject} is NULL!");
    }
    /// <summary>
    /// Displays an info message in Console when <see cref="m_IsDebugging"/> is set to true
    /// </summary>
    /// <param name="message">A message to be sent to Unity Console</param>
    protected void Log(object message)
    {
        if (!m_IsDebugging) return;
        Debug.Log($"{name.Bold()}: {message}");
    }

    /// <summary>
    /// Displays a warning message in Console when <see cref="m_IsDebugging"/> is set to true
    /// </summary>
    /// <param name="message">A message to be sent to Unity Console</param>
    protected void LogWarning(object msg)
    {
        if (!m_IsDebugging) return;
        Debug.LogWarning(msg);
    }

    /// <summary>
    /// Displays a error message in Console <see cref="m_IsSuppressingErrors"/> is set to true 
    /// </summary>
    /// <param name="message">A message to be sent to Unity Console</param>
    protected void LogError(object message)
    {
        if (m_IsSuppressingErrors) return;
        Debug.LogError($"{name.Bold()}: {message}");
    }
    #endregion
}

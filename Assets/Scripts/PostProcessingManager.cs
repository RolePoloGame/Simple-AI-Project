using Assets.Scripts.Core;
using UnityEngine;
using System;

public class PostProcessingManager : Singleton<PostProcessingManager>
{
    #region Properties & Fields
    [SerializeField]
    private GameObject[] m_Volumes = new GameObject[0];
    private int m_CurrentIndex = 0;
    #endregion

    public override void Initialize()
    {
        base.Initialize();
        m_CurrentIndex = 0;
        for (int i = 0; i < m_CurrentIndex; i++)
            m_Volumes[i].SetActive(i == m_CurrentIndex);
    }

    public void SwitchToVolume(int volumeIndex)
    {
        if (!VolumesExist()) return;

        bool isIndexCorrect = volumeIndex.EvaluateIndex(m_Volumes.Length);
        if (!isIndexCorrect) return;

        m_Volumes[m_CurrentIndex].SetActive(false);
        m_Volumes[volumeIndex].SetActive(true);
        m_CurrentIndex = volumeIndex;
    }

    /// <summary>
    /// Checks if there were any gameobject assigned to <see cref="m_Volumes"/> 
    /// </summary>
    /// <returns><true if</returns>
    private bool VolumesExist()
    {
        if (m_Volumes == null) return false;
        if (m_Volumes.Length == 0) return false;
        return true;
    }
}

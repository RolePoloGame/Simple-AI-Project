using Assets.Scripts.Core;
using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

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
    /// <summary>
    /// Fades between Volumes stored in <see cref="m_Volumes"/> in given time
    /// </summary>
    /// <param name="nextIndex">index of Volume to which fade in</param>
    /// <param name="fadeInTime">time in which the fade in happens</param>
    public void SwitchToVolume(int nextIndex, float fadeInTime = 0.0f)
    {
        if (!VolumesExist()) return;

        bool isIndexCorrect = nextIndex.EvaluateIndex(m_Volumes.Length);
        if (!isIndexCorrect) return;

        StopAllCoroutines();
        StartCoroutine(FadeInTime(fadeInIndex: nextIndex, fadeOutIndex: m_CurrentIndex, fadeTime: fadeInTime));
    }

    private IEnumerator FadeInTime(int fadeInIndex, int fadeOutIndex, float fadeTime)
    {
        float time = fadeTime;
        
        m_Volumes[fadeInIndex].SetActive(true); //fadeOut is already active, fadeIn is not

        PostProcessVolume fadeInVolume = m_Volumes[fadeInIndex].GetComponent<PostProcessVolume>();
        PostProcessVolume fadeOutVolume = m_Volumes[fadeOutIndex].GetComponent<PostProcessVolume>();

        //reset weights of volumes to proper values:
        fadeInVolume.weight = 0.0f; //fadeIn gets 0 to fade IN to 1
        fadeOutVolume.weight = 1.0f; //fadeOut gets 1 to fade OUT to 0

        while (true)
        {
            if (time <= float.Epsilon) break;
            time -= Time.deltaTime;

            float timeRatio = time / fadeTime;

            fadeInVolume.weight = timeRatio; 
            fadeOutVolume.weight = 1.0f - timeRatio;

            yield return null;
        }
        
        m_Volumes[fadeOutIndex].SetActive(false); // disabling the faded out volume
        m_CurrentIndex = fadeInIndex;
    }

    /// <summary>
    /// Checks if there were any gameobject assigned to <see cref="m_Volumes"/> 
    /// </summary>
    private bool VolumesExist()
    {
        if (m_Volumes == null) return false;
        if (m_Volumes.Length == 0) return false;
        return true;
    }
}

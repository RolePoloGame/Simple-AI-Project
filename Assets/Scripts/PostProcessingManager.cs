using Assets.Scripts.Core;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Rendering;

public class PostProcessingManager : Singleton<PostProcessingManager>
{
    #region Properties & Fields
    [SerializeField]
    private GameObject[] m_Volumes = new GameObject[0];
    #endregion

    /// <summary>
    /// Fades between Volumes stored in <see cref="m_Volumes"/> in given time
    /// </summary>
    /// <param name="nextIndex">index of Volume to which fade in</param>
    /// <param name="fadeTime">time in which the fade in happens</param>
    public void SwitchToVolume(int nextIndex, bool isFadeOut, float fadeTime = 0.0f)
    {
        if (!VolumesExist()) return;

        bool isIndexCorrect = nextIndex.EvaluateIndex(m_Volumes.Length);
        if (!isIndexCorrect) return;

        StartCoroutine(Fade(index: nextIndex, fadeTime: fadeTime, isFadeOut));
    }

    private IEnumerator Fade(int index, float fadeTime, bool isFadeOut)
    {
        float time = fadeTime;

        m_Volumes[index].SetActive(true); //fadeOut is already active, isFadeOut is not

        Volume fadeInVolume = m_Volumes[index].GetComponent<Volume>();

        fadeInVolume.weight = isFadeOut ? 0.0f : 1.0f;

        while (true)
        {
            if (time <= float.Epsilon) break;
            time -= Time.deltaTime;

            float timeRatio = time / fadeTime;

            fadeInVolume.weight = isFadeOut ? timeRatio : 1 - timeRatio;

            yield return null;
        }

        fadeInVolume.weight = !isFadeOut ? 1.0f : 0.0f;
        m_Volumes[index].SetActive(!isFadeOut); // disabling the faded out volume
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

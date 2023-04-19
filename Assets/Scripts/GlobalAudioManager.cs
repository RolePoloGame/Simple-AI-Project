using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class GlobalAudioManager : Singleton<GlobalAudioManager>
    {
        #region Properties & Fields
        private AudioSource m_AudioSource;
        private AudioSource AudioSource => m_AudioSource == null ? m_AudioSource = GetComponent<AudioSource>() : m_AudioSource;
        #endregion

        #region Public Methods
        /// <summary>
        /// Plays an audioclip
        /// </summary>
        public void PlayAudioClip(AudioClip clip) => AudioSource.PlayOneShot(clip);
        #endregion
    }
}
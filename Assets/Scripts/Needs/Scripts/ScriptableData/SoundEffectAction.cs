using Assets.Scripts.AI;
using UnityEngine;

namespace Assets.Scripts.Needs.Scripts.ScriptableData
{
    [CreateAssetMenu(menuName = "Needs/Need Action/Sound Effect")]
    public class SoundEffectAction : NeedAction
    {
        #region Properties & Fields
        [SerializeField]
        private AudioClip m_SoundEffect;
        #endregion

        #region Public methods
        /// <summary>
        /// Plays a sound effect through <see cref="GlobalAudioManager"/> then sets m_IsPerformed accordingly
        /// </summary>
        /// <param name="aiController"></param>
        public override void Act(AIController aiController)
        {
            GlobalAudioManager.Instance.PlayAudioClip(m_SoundEffect);
            m_IsPerformed = true;
        }
        #endregion
    }
}
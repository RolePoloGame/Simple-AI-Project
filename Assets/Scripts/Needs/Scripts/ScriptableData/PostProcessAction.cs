using Assets.Scripts.AI;
using UnityEngine;

namespace Assets.Scripts.Needs.Scripts.ScriptableData
{
    [CreateAssetMenu(menuName = "Needs/Need Action/Fade Postprocess")]
    public class PostProcessAction : NeedAction
    {
        #region Properties & Fields
        [SerializeField]
        private int m_PostProcessingIndex = 0;

        [SerializeField]
        private bool m_IsFadeOut = false;
        [SerializeField]
        private float m_FadeTime = 0.7f;
        #endregion

        #region Public methods
        /// <summary>
        /// Switches to a given post process volume through a <see cref="PostProcessingManager"/> and sets <see cref="m_IsPerformed"/> accordingly
        /// </summary>
        public override void Act(AIController aiController)
        {
            PostProcessingManager.Instance.SwitchToVolume(m_PostProcessingIndex, m_IsFadeOut, m_FadeTime);
            m_IsPerformed = true;
        }
        #endregion
    }
}
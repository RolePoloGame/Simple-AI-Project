using Assets.Scripts.AI;
using UnityEngine;

namespace Assets.Scripts.Needs.Scripts.ScriptableData
{
    [CreateAssetMenu(menuName = "Needs/Need Action/Talk (auto hide)")]
    public class TalkAction : NeedAction
    {
        #region Properties & Fields
        [SerializeField]
        private float m_DisplayTime = 2.0f;

        [SerializeField]
        private string m_Message = "Hello World!";
        #endregion

        #region Public methods
        public override void Act(AIController aiController) => CallDisplayText(aiController);
        #endregion

        #region Private methods
        /// <summary>
        /// Calls display text through <see cref="AIController"/> then sets <see cref="m_IsPerformed"/> accordingly
        /// </summary>
        /// <param name="aiController"></param>
        private void CallDisplayText(AIController aiController) => m_IsPerformed = aiController.DisplayText(m_Message, m_DisplayTime);
        #endregion
    }
}
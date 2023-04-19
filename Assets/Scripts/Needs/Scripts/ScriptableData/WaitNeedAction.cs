using Assets.Scripts.AI;
using UnityEngine;

namespace Assets.Scripts.Needs.Scripts.ScriptableData
{
    [CreateAssetMenu(menuName = "Needs/Need Action/Wait (X)")]
    public class WaitNeedAction : NeedAction
    {
        #region Properties & Fields
        [SerializeField] private float m_WaitTime;
        private float m_Timer;
        #endregion

        #region Public methods
        public override void OnEnter(AIController aiController) => ResetTimer();

        public override void Act(AIController aiController) => WaitAction();

        #endregion

        #region Private methods
        /// <summary>
        /// Waits for a given <see cref="m_WaitTime"/> and sets m_IsPerformed if time has elapsed
        /// </summary>
        private void WaitAction()
        {
            if (m_Timer <= float.Epsilon)
            {
                m_IsPerformed = true;
                return;
            }
            m_Timer -= Time.deltaTime;
        }

        private void ResetTimer()
        {
            m_Timer = m_WaitTime;
        } 
        #endregion
    }
}
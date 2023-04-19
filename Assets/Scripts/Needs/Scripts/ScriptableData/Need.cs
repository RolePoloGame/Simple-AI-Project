using Assets.Scripts.AI;
using UnityEngine;

namespace Assets.Scripts.Needs.Scripts.ScriptableData
{
    [CreateAssetMenu(menuName = "Needs/Need")]

    public class Need : ScriptableObject
    {
        #region Properties & Fields
        [SerializeField]
        private NeedSatisfyer[] m_NeedSatisfyers;

        private NeedSatisfactionObject m_SelectedSatisfyer;

        public bool HasSatisfyer => m_SelectedSatisfyer != null;
        public bool IsNeedSatisfied => m_IsNeedSatisfied;
        [SerializeField]
        private bool m_IsNeedSatisfied = false;
        #endregion

        #region Public methods
        public void SetSatisfier(NeedSatisfactionObject nso, AIController aiController)
        {
            m_SelectedSatisfyer = nso;
        }

        public NeedSatisfyer[] GetSatysfiers() => m_NeedSatisfyers;
        public void OnEnter(AIController aiController)
        {
            m_SelectedSatisfyer.OnEnter(aiController);
            m_IsNeedSatisfied = false;
        }

        public void OnExit(AIController aiController)
        {
            m_SelectedSatisfyer.OnExit(aiController);
            m_IsNeedSatisfied = false;
        }

        public void Act(AIController aiController) => HandleAct(aiController);
        /// <summary>
        /// Returns selected <see cref="NeedSatisfactionObject"/>'s position
        /// </summary>
        /// <returns></returns>
        public Vector3 GetTarget() => m_SelectedSatisfyer.transform.position;
        #endregion

        #region Private methods
        /// <summary>
        /// Calls Act on <see cref="m_SelectedSatisfyer"/> if:
        /// <list type="bullet">
        /// <item><description><see cref="HasSatisfyer"/> is true</description></item>
        /// <item><description>Arrived at its' Satisfyer</description></item>
        /// <item><description><see cref="IsNeedSatisfied"/> is fals</description></item>
        /// </list>
        /// </summary>
        /// <param name="aiController"></param>
        private void HandleAct(AIController aiController)
        {
            if (!HasSatisfyer) return;
            if (!aiController.GoToTarget()) return;
            if (IsNeedSatisfied) return;
            m_IsNeedSatisfied = m_SelectedSatisfyer.Act(aiController);
        }
        #endregion
    }
}
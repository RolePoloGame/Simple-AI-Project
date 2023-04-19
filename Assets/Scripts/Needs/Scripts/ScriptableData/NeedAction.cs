using Assets.Scripts.AI;
using System;
using UnityEngine;

namespace Assets.Scripts.Needs.Scripts.ScriptableData
{
    public abstract class NeedAction : ScriptableObject
    {
        #region Properties & Fields
        public bool IsPerformed => m_IsPerformed;
        protected bool m_IsPerformed;
        #endregion

        #region Public methods
        public virtual void Act(AIController aiController)
        {
            m_IsPerformed = true;
        }

        public virtual void OnEnter(AIController aiController) { m_IsPerformed = false; }
        public virtual void OnExit(AIController aiController) { m_IsPerformed = false; } 
        #endregion
    }
}
using Assets.Scripts;
using Assets.Scripts.AI.State;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Utilities;
using Assets.Scripts.GUI;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(AIScanner))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIController : EnhancedBehaviour
    {
        #region Properties & Fields
        [SerializeField]
        private AIState[] m_States;
        [SerializeField, ReadOnly]
        private AIState m_CurrentState = null;

        private AIScanner m_AIScanner;
        private NavMeshAgent m_NavMeshAgent;
        private TextDisplayController m_DisplayController;

        [SerializeField]
        private bool m_IsAwake = false;

        [SerializeField]
        private bool m_ChooseStateAtRandom = true;
        #endregion

        #region Unity Methods
        void Awake()
        {
            if (!m_ChooseStateAtRandom) return;
            SetRandomCurrentState();
        }

        private void Update()
        {
            if (!m_IsAwake) return;

            if (!HasState()) return;

            HandleCurrentState();
        }

        #endregion

        #region Public Methods
        public AIScanner GetAIScanner() => m_AIScanner == null ? m_AIScanner = GetComponent<AIScanner>() : m_AIScanner;
        /// <summary>
        /// Sets target for a NavMeshAgent
        /// </summary>
        /// <param name="position">Position to which to go</param>
        public void SetGoToTarget(Vector3 position)
        {
            GetNavMeshAgent().isStopped = true;
            GetNavMeshAgent().ResetPath();
            GetNavMeshAgent().SetDestination(position);
        }
        /// <summary>
        /// Set's wheter the AI is awake or not
        /// </summary>
        public void SetActive(bool value)
        {
            m_IsAwake = value;
        }

        /// <summary>
        /// Enables NavMeshAgent to travel to it's destination set by <see cref="SetGoToTarget(Vector3)"/>
        /// </summary>
        /// <returns>True if target arrived to the destination, false if didn't or failed to arive</returns>
        public bool GoToTarget()
        {
            if (GetNavMeshAgent().pathPending) return false;
            if (GetNavMeshAgent().pathStatus != NavMeshPathStatus.PathComplete) return false;

            Log(GetNavMeshAgent().pathStatus);

            bool isClose = GetNavMeshAgent().remainingDistance <= .5f;
            GetNavMeshAgent().isStopped = isClose;
            Log($"({GetNavMeshAgent().remainingDistance} units) left ({GetNavMeshAgent().destination} <= {transform.position}");
            Log(isClose.BoolColor());
            return isClose;
        }
        /// <summary>
        /// Calls Display text on <see cref="TextDisplayController"/>
        /// </summary>
        public bool DisplayText(string text, float time = 2.0f)
        {
            if (m_DisplayController == null) return false;
            return m_DisplayController.DisplayText(text, time);
        }

        public void AttachTextDisplayController(TextDisplayController displayController) => m_DisplayController = displayController;
        public void DettachTextDisplayController() => m_DisplayController = null;
        #endregion

        #region Private Methods
        /// <summary>
        /// Checks if AI has set or (if has no state set) can have a current State. 
        /// </summary>
        /// <returns>True if has a current state or was disabled</returns>
        private bool HasState()
        {
            if (m_CurrentState != null) return true;
            if (m_States == null || m_States.Length == 0)
            {
                HandleNoStates();
                return true;
            }
            Log("Has no Current State!");
            return false;
        }
        /// <summary>
        /// If it's impossible to set current state, disables the AI and throws an error.
        /// </summary>
        private void HandleNoStates()
        {
            DisableAI();
            LogError($"{name} has no States!");
        }

        /// <summary>
        /// Disables the AI
        /// </summary>
        private void DisableAI()
        {
            Log("Disabling AI!".Color(Color.red));
            m_IsAwake = false;
        }
        /// <summary>
        /// Handles the current state. If it's complete calls change to the next state
        /// </summary>
        private void HandleCurrentState()
        {
            if (!m_CurrentState.IsComplete)
            {
                Log($"Performing current state...");
                m_CurrentState.Act(this);
                return;
            }
            Log($"Changing to next state...");
            SetCurrentState(m_CurrentState.NextState);
        }
        /// <summary>
        /// Displays current need in avaliable GUI managers
        /// </summary>
        private void SetCurrentNeedInfo()
        {
            CurrentNeedGUIManager.Instance.SetText(m_CurrentState.name, m_CurrentState.Color);
        }
        /// <summary>
        /// Sets random <see cref="AIState"/> from <see cref="m_States"/> as a <see cref="m_CurrentState"/>
        /// </summary>
        private void SetRandomCurrentState() => SetCurrentState(m_States[Random.Range(0, m_States.Length)]);

        /// <summary>
        /// Sets given state as a new <see cref="m_CurrentState"/> and calls enter & exit methods
        /// </summary>
        /// <param name="newState"></param>
        private void SetCurrentState(AIState newState)
        {
            if (m_CurrentState != null)
                m_CurrentState.OnExit(this);
            m_CurrentState = newState;
            if (m_CurrentState != null)
                m_CurrentState.OnEnter(this);
            Log($"Current State is: {m_CurrentState}");
            SetCurrentNeedInfo();
        }
        /// <summary>
        /// Grabs stashed <see cref="NavMeshAgent"/>. If it's null Gets the component from gameobject
        /// </summary>
        /// <returns></returns>
        private NavMeshAgent GetNavMeshAgent() => m_NavMeshAgent == null ? m_NavMeshAgent = GetComponent<NavMeshAgent>() : m_NavMeshAgent;


        #endregion
    }
}
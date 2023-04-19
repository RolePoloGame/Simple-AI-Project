using Assets.Scripts.AI;
using UnityEngine;

namespace Assets.Scripts.Needs.Scripts.ScriptableData
{
    [CreateAssetMenu(menuName = "Needs/Need Action/Particle")]
    public class ParticleAction : NeedAction
    {
        #region Properties & Fields
        [SerializeField]
        private ParticleSystem m_ParticleSystem;
        #endregion

        #region Public methods
        public override void Act(AIController aiController)
        {
            SpawnParticleEffect(aiController);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Spawns <see cref="m_ParticleSystem"/> and sets <see cref="m_IsPerformed"/> accordingly
        /// </summary>
        /// <param name="aiController"></param>
        private void SpawnParticleEffect(AIController aiController)
        {
            ParticleSystem ps = Instantiate(m_ParticleSystem);
            ps.transform.position = aiController.transform.position;
            ps.Play();
            m_IsPerformed = true;
        }
        #endregion
    }
}
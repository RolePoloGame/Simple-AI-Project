using UnityEngine;

[CreateAssetMenu(menuName = "Needs/Need Action/Particle")]
public class ParticleAction : NeedAction
{
    [SerializeField]
    private ParticleSystem m_ParticleSystem;
}

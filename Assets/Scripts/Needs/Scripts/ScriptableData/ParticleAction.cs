using UnityEngine;

[CreateAssetMenu(menuName = "Needs/Need Action/Particle")]
public class ParticleAction : NeedAction
{
    [SerializeField]
    private ParticleSystem m_ParticleSystem;

    public override void Act(AIController aiController)
    {
        ParticleSystem ps = Instantiate(m_ParticleSystem);
        ps.transform.position = aiController.transform.position;
        ps.Play();
        m_IsPerformed = true;
    }

    public override void OnEnter(AIController aiController) { }
}

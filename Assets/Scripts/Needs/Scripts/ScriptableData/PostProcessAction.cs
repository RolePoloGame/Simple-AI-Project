using Assets.Scripts;
using UnityEngine;

[CreateAssetMenu(menuName = "Needs/Need Action/Fade Postprocess")]
public class PostProcessAction : NeedAction
{
    [SerializeField]
    private int m_PostProcessingIndex = 0;

    [SerializeField]
    private bool m_IsFadeOut = false;
    [SerializeField]
    private float m_FadeTime = 0.7f;
    public override void Act(AIController aiController)
    {
        PostProcessingManager.Instance.SwitchToVolume(m_PostProcessingIndex, m_IsFadeOut, m_FadeTime);
        m_IsPerformed= true;
    }

    public override void OnEnter(AIController aiController)
    {
        m_IsPerformed = false;
    }
}

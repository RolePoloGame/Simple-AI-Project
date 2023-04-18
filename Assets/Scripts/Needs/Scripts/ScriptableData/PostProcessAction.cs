using UnityEngine;

[CreateAssetMenu(menuName = "Needs/Need Action/Change Postprocessing")]
public class PostProcessAction : NeedAction
{
    [SerializeField]
    private int m_PostProcessingIndex = 0;
    [SerializeField]
    private float m_FadeTime = 0.7f;
    public override void OnEnter(AIController aiController) => PostProcessingManager.Instance.SwitchToVolume(m_PostProcessingIndex, m_FadeTime);
}

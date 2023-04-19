using UnityEngine;
[CreateAssetMenu(menuName = "Needs/Need Action/Talk (auto hide)")]
public class TalkAction : NeedAction
{
    [SerializeField]
    private float m_DisplayTime = 2.0f;

    [SerializeField]
    private string m_Message = "Hello World!";

    public override void Act(AIController aiController)
    {
        aiController.DisplayText(m_Message, m_DisplayTime);
        m_IsPerformed = true;
    }
}

using UnityEngine;
[CreateAssetMenu(menuName = "Needs/Need Action/Talk (auto hide)")]
public class TalkAction : NeedAction
{
    [SerializeField]
    private float m_DisplayTime = 2.0f;
    private float m_Timer = 0.0f;

    [SerializeField]
    private string m_Message = "Hello World!";

    public override void OnEnter(AIController aiController)
    {
        m_Timer = m_DisplayTime;
    }
}

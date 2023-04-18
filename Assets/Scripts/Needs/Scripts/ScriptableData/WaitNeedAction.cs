using UnityEngine;

[CreateAssetMenu(menuName = "Needs/Need Action/Wait (X)")]
public class WaitNeedAction : NeedAction
{
    [SerializeField] private float m_WaitTime;
    private float m_Timer;

    public override void OnEnter(AIController aiController)
    {
        m_Timer = m_WaitTime;
    }

    public override void Act(AIController aiController)
    {
        if (m_Timer <= float.Epsilon)
        {
            m_IsPerformed = true;
            return;
        }
        m_Timer -= Time.deltaTime;
    }
}

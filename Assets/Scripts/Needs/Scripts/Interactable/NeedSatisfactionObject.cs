using UnityEngine;

public class NeedSatisfactionObject : MonoBehaviour
{
    [SerializeField]
    private NeedSatisfyer m_Need;

    public bool IsSameSatysfier(NeedSatisfyer need)
    {
        if (need == null) return false;
        return m_Need.Equals(need);
    }
}

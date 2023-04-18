using UnityEngine;

[CreateAssetMenu(menuName = "Needs/Need Action/Change Postprocessing")]
public class PostProcessAction : NeedAction
{
    [SerializeField]
    private int m_PostProcessingIndex = 0;
}

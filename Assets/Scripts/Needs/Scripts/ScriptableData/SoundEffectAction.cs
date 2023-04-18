using UnityEngine;
[CreateAssetMenu(menuName = "Needs/Need Action/Sound Effect")]
public class SoundEffectAction : NeedAction
{
    [SerializeField]
    private AudioClip m_SoundEffect;

    public override void OnEnter(AIController aiController)
    {
        GlobalAudioManager.Instance.PlayAudioClip(m_SoundEffect);
    }
}

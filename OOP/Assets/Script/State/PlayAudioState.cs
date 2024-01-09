using UnityEngine;

public class PlayAudioState : StateMachineBehaviour
{
    public int sfxToPlay;
    public bool onExit, notOnEnter;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!notOnEnter) AudioManager.Ins.PlaySFX(sfxToPlay);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (onExit) AudioManager.Ins.PlaySFX(sfxToPlay);
    }
}

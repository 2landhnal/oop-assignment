using UnityEngine;

public class DisableControlState : StateMachineBehaviour
{
    Animator anim;
    public bool enableWhenExit;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (anim == null)
        {
            anim = animator.GetComponent<Animator>();
        }
        anim.SetBool("canControl", false);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (anim == null)
        {
            anim = animator.GetComponent<Animator>();
        }
        if (enableWhenExit)
        {
            anim.SetBool("canControl", true);
        }
    }
}

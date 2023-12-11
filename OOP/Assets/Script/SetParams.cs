using System;
using UnityEngine;

public class SetParams : StateMachineBehaviour
{
    Animator anim;
    public Node[] paras;
    [Serializable]
    public struct Node
    {
        public string paraName;
        public bool tfWhenEnter, tfWhenExit;
        public bool whenEnter, whenExit;
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (anim == null)
        {
            anim = animator.GetComponent<Animator>();
        }
        foreach (Node node in paras)
        {
            if (node.whenEnter)
            {
                anim.SetBool(node.paraName, node.tfWhenEnter);
            }
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (Node node in paras)
        {
            if (node.whenExit)
            {
                anim.SetBool(node.paraName, node.tfWhenExit);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : StateMachineBehaviour
{
    Creature creature;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        creature = animator.GetComponent<Creature>();
        creature.OnDeath?.Invoke();
        animator.enabled = false;
    }
}

using UnityEngine;

public class SetVelState : StateMachineBehaviour
{
    private Rigidbody2D rb;
    public Vector2 velWhenEneter, velWhenExit;
    public bool onEnter, onExit, onlyX;    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(rb == null)
        {
            rb = animator.GetComponent<Rigidbody2D>();
        }
        if(onEnter)
        {
            if (onlyX)
            {
                rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: velWhenEneter.x);
            }
            else
            {
                rb.velocity = velWhenEneter;
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (rb == null)
        {
            rb = animator.GetComponent<Rigidbody2D>();
        }
        if (onExit)
        {
            if (onlyX)
            {
                rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: velWhenExit.x);
            }
            else
            {
                rb.velocity = velWhenExit;
            }
        }
    }
}

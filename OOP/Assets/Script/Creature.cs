using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField]protected Transform groundPoint;
    [SerializeField] protected float speed, jumpForce;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Vector2 tmpV2;
    protected bool canDoubleJump, grouding;
    protected float horizontal;
    protected Collider2D col;
    protected CombatProps cbProps;
    protected float hurtDirect, hurtCounter;
    // Start is called before the first frame update
    void Start()
    {
        cbProps = CombatProps.instance;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        LateStart();
    }

    public Vector2 GetCenterPos()
    {
        return (Vector2)transform.position + col.offset;
    }

    private void LateUpdate()
    {
        if (hurtCounter > cbProps.hurtTime - cbProps.hurtPart)
        {
            //rb.velocity = cbProps.CreateVector2(rb.velocity, cbProps.hurtFoce * hurtDirect);
            rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: cbProps.hurtForce * (hurtCounter + cbProps.hurtPart - cbProps.hurtTime) / cbProps.hurtPart * hurtDirect);
        }
        CheckFlip();
        CheckGrounding();

        animator.SetBool("grouding", grouding);
        animator.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVel", rb.velocity.y);
        InsideLateUpdate();
        CounterFunc();
    }

    void CounterFunc()
    {
        if (hurtCounter > 0)
        {
            hurtCounter -= Time.deltaTime;
            if(hurtCounter <= 0)
            {
                //rb.velocity
            }
        }
    }

    public bool CanControl()
    {
        return animator.GetBool("canControl");
    }

    protected virtual void InsideLateUpdate() { }

    void CheckFlip()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if(rb.velocity.x > 0.1f)
        {
            transform.Rot(0);
        }
        else if (rb.velocity.x < -0.1f)
        {
            transform.Rot(180);
        }
    }

    public void SetHurtMove(Vector2 pos)
    {
        Debug.Log("This");
        transform.FlipToObj(pos.x);

        hurtDirect = (transform.CheckFlip() ? 1 : -1) * (-1);
        rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: cbProps.hurtForce * hurtDirect, yToSet: CombatProps.instance.hurtForce / 2 * Mathf.Sin(transform.GetRotZFollowTargetByVector3(pos, true)));

        hurtCounter = cbProps.hurtTime;

        animator.SetBool("hurt", hurtCounter > 0);
    }

    public bool CheckGrounding()
    {
        grouding = Physics2D.OverlapCircle(groundPoint.position, .2f, EnviromentProps.Instance.groundLayers);
        if (grouding)
        {
            canDoubleJump = true;
        }
        return grouding;
    }

    protected virtual void LateStart() { }

    public void Flip(bool flipRight)
    {
        tmpV2.x = flipRight ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x);
        tmpV2.y = transform.localScale.y;
        transform.localScale = tmpV2;
    }
}

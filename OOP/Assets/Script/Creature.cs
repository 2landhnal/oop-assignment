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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        LateStart();
    }
    private void Update()
    {
        
    }

    public Vector2 GetCenterPos()
    {
        return (Vector2)transform.position + col.offset;
    }

    private void LateUpdate()
    {
        CheckFlip();
        CheckGrounding();

        animator.SetBool("grouding", grouding);
        animator.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVel", rb.velocity.y);
        InsideLateUpdate();
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

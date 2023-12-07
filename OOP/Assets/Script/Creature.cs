using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField] protected int maxHP;
    [SerializeField]protected Transform groundPoint;
    protected int currentHP;
    [SerializeField] protected float speed, jumpForce;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Vector2 tmpV2;
    protected bool canDoubleJump, grouding;
    protected float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        LateStart();
    }

    private void LateUpdate()
    {
        CheckFlip();
        CheckGrounding();

        animator.SetBool("grouding", grouding);
        animator.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVel", rb.velocity.y);
    }

    void CheckFlip()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if(horizontal != 0f)
        {
            tmpV2.x = rb.velocity.x > 0 ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x);
            tmpV2.y = transform.localScale.y;
            transform.localScale = tmpV2;
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

    public bool FaceRight()
    {
        return transform.localScale.x > 0;
    }

    public void Flip(bool flipRight)
    {
        tmpV2.x = flipRight ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x);
        tmpV2.y = transform.localScale.y;
        transform.localScale = tmpV2;
    }
}

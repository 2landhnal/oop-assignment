using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField] protected int maxHP;
    protected int currentHP;
    [SerializeField] protected float speed, jumpForce;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Vector2 tmpV2;
    protected bool grouding, canDoubleJump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        LateStart();
    }

    private void LateUpdate()
    {
        if(rb.velocity.x != 0)
        {
            tmpV2.x = rb.velocity.x > 0 ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x);
            tmpV2.y = transform.localScale.y;
            transform.localScale = tmpV2;
        }

        animator.SetBool("grouding", grouding);
        animator.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVel", rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grouding = true;
            canDoubleJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grouding = false;
        }
    }

    protected virtual void LateStart() { }
}

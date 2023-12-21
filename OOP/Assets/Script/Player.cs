using UnityEngine;

public class Player : Creature
{
    public static Player instance;
    protected float horizontalInput;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    protected override void LateStart()
    {
        base.LateStart();
    }

    protected override void InsideLateUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (CanControl())
        {
            //move
            tmpV2.x = horizontalInput * speed;
            tmpV2.y = rb.velocity.y;
            rb.velocity = tmpV2;

            //jump
            if (grouding || canDoubleJump)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (canDoubleJump && !grouding)
                    {
                        canDoubleJump = false;
                    }
                    tmpV2.x = rb.velocity.x;
                    tmpV2.y = canDoubleJump ? jumpForce : jumpForce;
                    rb.velocity = tmpV2;
                }
            }
            if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0f)
            {
                tmpV2.x = rb.velocity.x;
                tmpV2.y = rb.velocity.y * .5f;
                rb.velocity = tmpV2;
            }

            //attack
            if (Input.GetKeyDown(KeyCode.J))
            {
                animator.SetBool("attack", true);
                rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: 0);
            }
        }
    }
}

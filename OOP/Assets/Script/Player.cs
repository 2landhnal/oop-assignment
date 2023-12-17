using UnityEngine;

public class Player : Creature
{
    public static Player instance;
    public Transform sidePoint;
    [SerializeField]private float wallJumpCooldown;
    private float wallJumpCounter;
    [SerializeField] private float wallSlideSpeed;
    private void Awake()
    {
        instance = this;
    }
    protected override void LateStart()
    {
        base.LateStart();
    }

    protected override void InsideLateUpdate()
    {
        if(wallJumpCounter > 0)
        {
            wallJumpCounter -= Time.deltaTime;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (CanControl())
        {
            //move
            tmpV2.x = horizontal * speed;
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
                    tmpV2.y = canDoubleJump ? jumpForce : jumpForce / 1.2f;
                    rb.velocity = tmpV2;
                }
                if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0f)
                {
                    tmpV2.x = rb.velocity.x;
                    tmpV2.y = rb.velocity.y * .5f;
                    rb.velocity = tmpV2;
                }
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

using UnityEngine;

public class Player : Creature
{
    public static Player instance;
    public Transform sidePoint;
    [SerializeField] private float wallSlideSpeed;
    private void Awake()
    {
        instance = this;
    }
    protected override void LateStart()
    {
        base.LateStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (TouchingWall() && !grouding)
        {
            if (horizontal != 0)
            {
                tmpV2.x = horizontal * speed;
                tmpV2.y = -wallSlideSpeed;
                rb.velocity = tmpV2;
                animator.SetBool("wallSlide", true);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    tmpV2.x = jumpForce * horizontal;
                    tmpV2.y = canDoubleJump ? jumpForce : jumpForce / 1.2f;
                    rb.velocity = tmpV2;
                }
                animator.SetBool("wallSlide", false);
            }
        }
        else
        {
            animator.SetBool("wallSlide", false);
        }
        if (!animator.GetBool("wallSlide"))
        {
            //move
            tmpV2.x = horizontal * speed;
            tmpV2.y = rb.velocity.y;
            rb.velocity = tmpV2;

            //jump
            if (grouding || canDoubleJump)
            {
                if(Input.GetKeyDown(KeyCode.W)){
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
                    tmpV2.y = rb.velocity.y*.5f;
                    rb.velocity = tmpV2;
                }
            }
        }

    }

    public bool TouchingWall()
    {
        if (Physics2D.OverlapCircle(sidePoint.position, .2f, EnviromentProps.Instance.slideableLayers))
        {
            canDoubleJump = true;
        }
        return Physics2D.OverlapCircle(sidePoint.position, .2f, EnviromentProps.Instance.slideableLayers);
    }
}

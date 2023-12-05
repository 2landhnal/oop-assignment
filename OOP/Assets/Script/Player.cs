using UnityEngine;

public class Player : Creature
{
    public static Player instance;
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
        //move
        tmpV2.x = Input.GetAxisRaw("Horizontal") * speed;
        tmpV2.y = rb.velocity.y;
        rb.velocity = tmpV2;

        //jump
        if(Input.GetKeyDown(KeyCode.W) && (grouding || canDoubleJump))
        {
            if (canDoubleJump && !grouding)
            {
                canDoubleJump = false;
            }
            tmpV2.x = rb.velocity.x;
            tmpV2.y = canDoubleJump ? jumpForce : jumpForce / 1.2f;
            rb.velocity = tmpV2;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Creature
{
    public float walkRaidus, attackRadius;
    private List<float> points = new List<float>();
    private float currentTarget;
    int index;
    private Transform player;
    bool attacked;
    public float minPeriodDis;
    float walkedDis, lasPos;
    float checkTime = 3, checkCounter;

    protected override void LateStart()
    {
        base.LateStart();
        points.Insert(0, transform.position.x - walkRaidus);
        points.Insert(1, transform.position.x + walkRaidus);
        index = 0;
        currentTarget = points[0];
        checkCounter = checkTime;
        lasPos = transform.position.x;
    }
    private void Update()
    {
        if (Player.instance == null) return;
        if (player == null)
        {
            player = Player.instance.gameObject.transform;
        }
        if (CanControl())
        {
            if (checkCounter > 0)
            {
                checkCounter -= Time.deltaTime;
                if(checkCounter < 0)
                {
                    if(Mathf.Abs(lasPos - transform.position.x) < minPeriodDis)
                    {
                        if(Random.Range(0, 100) < 50)
                        {
                            Jump();
                        }
                        else
                        {
                            NextPoint();
                        }
                    }
                    lasPos = transform.position.x;
                    checkCounter = checkTime;
                }
            }
            else
            {

            }
            tempFloat = Vector2.Distance(player.position, transform.position);
            if (tempFloat <= attackRadius) Attack();
            if(!attacked)
            {
                CheckNextPoint();
            }

            //move
            if (attacked)
            {
                if(Mathf.Abs(transform.position.x - player.position.x) <= 2)
                {

                }
                else if (transform.position.x < player.position.x)
                {
                    rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: speed);
                }
                else
                {
                    rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: -speed);
                }
            }
            else
            {
                if (transform.position.x < currentTarget)
                {
                    rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: speed);
                }
                else
                {
                    rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: -speed);
                }
            }
        }
    }

    void Jump()
    {
        //jump
        if (grouding || canDoubleJump)
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

    void Attack()
    {
        attacked = true;
        transform.FlipToObj(player.position.x);
        animator.SetBool("attack", true);
    }

    private void CheckNextPoint()
    {
        if (Mathf.Abs(transform.position.x - currentTarget) < .1f)
        {
            NextPoint();
        }
    }

    void NextPoint()
    {
        index++;
        if (index == points.Count)
        {
            index = 0;
        }
        currentTarget = points[index];
    }
}

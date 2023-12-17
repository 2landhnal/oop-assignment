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

    protected override void LateStart()
    {
        base.LateStart();
        points.Insert(0, transform.position.x - walkRaidus);
        points.Insert(1, transform.position.x + walkRaidus);
        index = 0;
        currentTarget = points[0];
        player = Player.instance.gameObject.transform;
    }
    private void Update()
    {
        if (CanControl())
        {
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
            index++;
            if (index == points.Count)
            {
                index = 0;
            }
            currentTarget = points[index];
        }
    }
}

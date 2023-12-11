using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Creature
{
    public float walkRaidus;
    private List<float> points = new List<float>();
    private float currentPoint;
    int index;
    private Transform player;

    protected override void LateStart()
    {
        base.LateStart();
        points.Insert(0, transform.position.x - walkRaidus);
        points.Insert(1, transform.position.x + walkRaidus);
        index = 0;
        currentPoint = points[0];
    }
    private void Update()
    {
        player = Player.instance.gameObject.transform;
        float dist = Vector2.Distance(player.position, transform.position);
        if (dist <= 2) animator.SetBool("AttackEnemy", true);
        else
        {
            animator.SetBool("AttackEnemy", false);
        }
        if (animator.GetBool("AttackEnemy") == true)
        {
            if(transform.position.x < player.position.x && transform.rotation.z == -1)
            {
                currentPoint = transform.position.x;
            }
            else if (transform.position.x > player.position.x && transform.rotation.z == 0)
            {
                currentPoint = transform.position.x;
            }
        }
        Move();
    }

    private void Move()
    {
        if (Mathf.Abs(transform.position.x - currentPoint) < .1f)
        {
            index++;
            if (index == points.Count)
            {
                index = 0;
            }
            currentPoint = points[index];
        }
        if (transform.position.x < currentPoint)
        {
            rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: speed);
        }
        else
        {
            rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: -speed);
        }
    }
}

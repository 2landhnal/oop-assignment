using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Creature
{
    public float walkRaidus;
    private List<float> points = new List<float>();
    private float currentPoint;
    int index;

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
        if(CanControl())
        {
            Move();
        }
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
            //transform.position.x = currentPoint;
        }
        else
        {
            rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: -speed);
        }
    }
}

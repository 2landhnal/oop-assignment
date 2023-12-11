using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enemy_Skeleton : Creature
{
    Vector2 initialPosition;
    int direction;
    float maxDist = 0;
    float minDist = 0;

    private void Start()
    {
        initialPosition = transform.position;
        direction = -1;
        maxDist += transform.position.x;
        minDist -= transform.position.x;
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        switch (direction)
        {
            case -1:
                // Moving Left
                if (transform.position.x > minDist)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    direction = 1;
                }
                break;
            case 1:
                //Moving Right
                if (transform.position.x < maxDist)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    direction = -1;
                }
                break;
        }
    }
}

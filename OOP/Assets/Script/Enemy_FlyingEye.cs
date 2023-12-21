using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FlyingEye : Creature
{
    public float walkRaidus, attackRadius;
    [SerializeField]private Transform attackPoint;
    [SerializeField]private List<Transform> points = new List<Transform>();
    private Transform currentPoint;
    int index;
    private Transform player;
    bool chase;
    [SerializeField] protected Bullet bulletPrefab;

    protected override void LateStart()
    {
        base.LateStart();
        chase = false;
        index = 0;
        currentPoint = points[0];
        player = Player.instance.gameObject.transform;
        foreach (Transform t in points)
        {
            t.parent = null;
        }
    }
    private void Update()
    {
        if (CanControl())
        {
            tempFloat = Vector2.Distance(player.position, transform.position);
            if (tempFloat <= attackRadius) Attack();
            if(!chase) CheckNextPoint();
            if(chase)
            {
                currentPoint = player.transform;
            }
            if(chase && tempFloat <= attackRadius)
            {

            }
            else
            {
                rb.velocity = transform.right * speed;
            }
            transform.FollowTransformRotZ(currentPoint);
        }
    }
    protected override void CheckFlip()
    {
        return;
    }
    public void ReleaseBullet()
    {
        Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity).SetTarget(player.gameObject);
    }
    void Attack()
    {
        chase = true;
        rb.velocity = Vector2.zero;
        transform.FlipToObj(player.position.x);
        animator.SetBool("attack", true);
    }

    private void CheckNextPoint()
    {
        tempFloat = Vector2.Distance(transform.position, currentPoint.position);
        if (tempFloat < .1f)
        {
            index++;
            if (index == points.Count)
            {
                index = 0;
            }
            currentPoint = points[index];
        }
    }

    private void OnDestroy()
    {
        foreach(Transform t in points)
        {
            if (t == null) continue;
            Destroy(t.gameObject);
        }
    }
}

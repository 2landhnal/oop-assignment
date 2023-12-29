using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AttackPoint
{
    [SerializeField]private float speed, accurate;

    protected override void ColWithWall()
    {
        base.ColWithWall();
        DestroyFunc();
    }
    protected override void ColWithTarget(IDamageable idm)
    {
        base.ColWithTarget(idm);
        DestroyFunc();
    }
    void DestroyFunc()
    {
        Destroy(gameObject);
    }

    public void SetTarget(GameObject target)
    {
        transform.FollowEnemyRotZV2(target.transform.GetCenterPos());
        GetComponent<Rigidbody2D>().velocity = transform.right*speed;
        SetLayerToEnemy(target);
    }
    public void Attack(float x)
    {
        if (x == 0) GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        else
        {
            GetComponent<Rigidbody2D>().rotation = 180;
            GetComponent<Rigidbody2D>().velocity = transform.right * -speed;
        }
    }
}

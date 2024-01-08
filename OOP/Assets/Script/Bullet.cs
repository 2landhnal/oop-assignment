using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AttackPoint
{
    [SerializeField]private float speed, accurate;
    public GameObject fx;

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
        if(fx != null)Instantiate(fx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetTarget(GameObject target)
    {
        transform.FollowEnemyRotZV2(target.transform.GetCenterPos());
        GetComponent<Rigidbody2D>().velocity = transform.right*speed;
        SetLayerToEnemy(target);
    }

    //Arrow of Player
    public void Attack(float Player_rotation)
    {
        if (Player_rotation == 0) GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        else
        {
            GetComponent<Rigidbody2D>().rotation = 180;
            GetComponent<Rigidbody2D>().velocity = transform.right * -speed;
        }
    }
}

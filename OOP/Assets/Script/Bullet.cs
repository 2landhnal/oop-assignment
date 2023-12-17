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
    void DestroyFunc()
    {
        Destroy(gameObject);
    }

    public void SetTarget(GameObject target)
    {
        transform.FollowEnemyRotZ(target.transform);
        GetComponent<Rigidbody2D>().velocity = transform.right*speed;
        SetLayerToEnemy(target);
    }
}

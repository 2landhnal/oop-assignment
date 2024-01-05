using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    [SerializeField] public float damage;
    private Collider2D col;
    private IDamageable tempIDamageable;
    private CombatProps cbProps;
    void Start()
    {
        cbProps = CombatProps.Ins;
        col = GetComponent<Collider2D>();
        if(transform.parent != null) SetLayer(transform.parent.gameObject);
        LateStart();
    }

    private void OnEnable()
    {

    }

    public void SetLayer(GameObject parentObj)
    {
        if (parentObj == null) return;
        if (parentObj.layer == LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("PlayerAtk");
        }
        else if (parentObj.layer == LayerMask.NameToLayer("Enemy"))
        {
            gameObject.layer = LayerMask.NameToLayer("EnemyAtk");
        }
    }

    public void SetLayerToEnemy(GameObject enemyObj)
    {
        if (enemyObj == null) return;
        if (enemyObj.layer == LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("EnemyAtk");
        }
        else if (enemyObj.layer == LayerMask.NameToLayer("Enemy"))
        {
            gameObject.layer = LayerMask.NameToLayer("PlayerAtk");
        }
    }

    protected virtual void LateStart() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out tempIDamageable))
        {
            ColWithTarget(tempIDamageable);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            ColWithWall();
        }
        else
        {
            Debug.Log("no damageable");
        }
    }

    protected virtual void ColWithWall()
    {

    }
    protected virtual void ColWithTarget(IDamageable idm)
    {
        idm.TakeDamage(damage, transform.position);
    }
}

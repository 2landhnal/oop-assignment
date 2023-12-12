using System;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    [SerializeField] public float damage;
    private Collider2D col;
    private LayerMask enemyLayer;
    private IDamageable tempIDamageable;
    private CombatProps cbProps;
    void Start()
    {
        cbProps = CombatProps.instance;
        col = GetComponent<Collider2D>();
        if (transform.parent.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            enemyLayer = CombatProps.instance.enemyLayer;
            gameObject.layer = LayerMask.NameToLayer("PlayerAtk");
        }
        else if (transform.parent.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyLayer = CombatProps.instance.playerLayer;
            gameObject.layer = LayerMask.NameToLayer("EnemyAtk");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out tempIDamageable))
        {
            tempIDamageable.TakeDamage(damage, transform.parent.position);
        }
        else
        {
            Debug.Log("no damageable");
        }
    }
}

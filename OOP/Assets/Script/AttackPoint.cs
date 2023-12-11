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
        if(((1 << transform.parent.gameObject.layer) & cbProps.playerLayer) != 0)
        {
            enemyLayer = CombatProps.instance.enemyLayer;
        }
        else if (((1 << transform.parent.gameObject.layer) & cbProps.enemyLayer) != 0)
        {
            enemyLayer = CombatProps.instance.playerLayer;
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

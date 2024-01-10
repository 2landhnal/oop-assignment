using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    public GameObject fx;
    public void TakeDamage(float damage, Vector2 pos)
    {
        Instantiate(fx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

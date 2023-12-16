using UnityEngine;

public class CombatProps : MonoBehaviour
{
    public static CombatProps instance;
    public LayerMask playerLayer, enemyLayer, playerAtkLayer, enemyAtkLayer;
    public float hurtForce, hurtTime, hurtPart;
    public SpriteRenderer bodyPrefab;

    private void Start()
    {
        
    }
    private void Awake()
    {
        instance = this;
    }
}

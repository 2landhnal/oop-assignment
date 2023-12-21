using UnityEngine;

public class CombatProps : Singleton<CombatProps>
{
    public LayerMask playerLayer, enemyLayer, playerAtkLayer, enemyAtkLayer;
    public float hurtForce, hurtTime, hurtPart;
    public SpriteRenderer bodyPrefab, afterImagePrefab;

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    private void Start()
    {
        
    }
}

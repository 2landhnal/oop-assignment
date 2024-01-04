using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
public class HealthManager : MonoBehaviour, IDamageable
{

    protected CombatProps cbProps;
    protected float hurtDirect, hurtCounter;
    private LayerMask enemyLayer;
    protected Collider2D col;
    [SerializeField] private float maxHP;
    public float currentHP { get; private set; }
    public float currentHPRate { get => currentHP / maxHP; }
    private Creature creature;
    public bool immortal;
    public GameObject Items;
    public int Quantity;
    // Start is called before the first frame update
    void Start()
    {
        creature = GetComponent<Creature>();
        currentHP = maxHP;
        immortal = false;
    }

    public float GetMaxHP()
    {
        return maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DropItems()
    {
        for (int i = 0; i < Quantity; i++)
        {
            Instantiate (Items, (GetComponent<Rigidbody2D>().position) , quaternion.identity );
        }
    }

    public void TakeDamage(float damage, Vector2 pos)
    {
        if (immortal) return;
        if (currentHP > 0) creature.SetHurtMove(pos);
        currentHP -= damage;
        if (currentHP <= 0)
        {
            DropItems();
            creature.Animator_SetDead();
        }
    }
}

using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

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
    bool loaded = false;
    // Start is called before the first frame update
    void Start()
    {
        creature = GetComponent<Creature>();
        if(!loaded) currentHP = maxHP;
        immortal = false;
    }

    public float GetMaxHP()
    {
        return maxHP;
    }
    public void LoadHP(float maxHP, float rate)
    {
        loaded = true;
        this.maxHP = maxHP;
        currentHP = maxHP * rate;
    }

    public void RecorverHP(float amount)
    {
        currentHP += amount;
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage, Vector2 pos)
    {
        if (immortal) return;
        if (currentHP > 0) creature.SetHurtMove(pos);
        Instantiate(CombatProps.Ins.bloodEffect, transform.GetCenterPos(), Quaternion.identity);
        currentHP -= damage;
        if (currentHP <= 0)
        {
            GetComponent<IDropable>()?.DropItems();
            creature.Animator_SetDeath();
        }
    }
}

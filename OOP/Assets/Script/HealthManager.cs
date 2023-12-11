using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHP;
    private float currentHP;
    private Creature creature;
    // Start is called before the first frame update
    void Start()
    {
        creature = GetComponent<Creature>();
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage, Vector2 pos)
    {
        currentHP -= damage;
        creature.SetHurtMove(pos);
    }
}

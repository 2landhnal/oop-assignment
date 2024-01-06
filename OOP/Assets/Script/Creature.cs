using UnityEngine;
using UnityEngine.Events;

public class Creature : MonoBehaviour, IDropable
{
    [SerializeField]protected Transform groundPoint;
    [SerializeField] protected float speed, jumpForce;
    public Rigidbody2D rb { protected set; get; }
    public SpriteRenderer sr { protected set; get; }
    public Animator animator { private set; get; }
    protected Vector2 tmpV2;
    protected bool canDoubleJump, grouding;
    public Collider2D col { private set; get; }
    protected CombatProps cbProps;
    protected float hurtDirect, hurtCounter;
    [SerializeField]protected bool reverseSprite;
    private LayerMask enemyLayer;
    protected float tempFloat;
    public UnityEvent OnDeath;
    // Start is called before the first frame update
    void Start()
    {
        OnDeath.AddListener(Death);
        cbProps = CombatProps.Ins;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        LateStart();
    }
    private void Update()
    {
        
    }

    public Vector2 GetCenterPos()
    {
        return (Vector2)transform.position + col.offset;
    }

    private void LateUpdate()
    {
        if (Animator_GetDead())
        {
            return;
        }
        if (CanControl())
        {
            CheckFlip();
        }
        if (cbProps != null)
        {
            if (hurtCounter >= cbProps.hurtTime - cbProps.hurtPart)
            {
                //rb.velocity = cbProps.CreateVector2(rb.velocity, cbProps.hurtFoce * hurtDirect);
                rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: cbProps.hurtForce * (hurtCounter + cbProps.hurtPart - cbProps.hurtTime) / cbProps.hurtPart * hurtDirect);
            }
        }
        CheckGrounding();

        animator.SetBool("grouding", grouding);
        animator.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVel", rb.velocity.y);
        animator.SetFloat("hurtCounter", hurtCounter);
        InsideLateUpdate();
        CounterFunc();
    }

    public void Animator_SetDead()
    {
        animator.SetBool("dead", true);
    }
    public bool Animator_GetDead()
    {
        return animator.GetBool("dead");
    }

    void CounterFunc()
    {
        if (hurtCounter > 0)
        {
            hurtCounter -= Time.deltaTime;
            if(hurtCounter <= 0)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    public bool CanControl()
    {
        return animator.GetBool("canControl");
    }

    protected virtual void InsideLateUpdate() { }

    protected virtual void CheckFlip()
    {
        if(rb.velocity.x > 0.1f)
        {
            transform.Rot(180*(reverseSprite?1:0)-0);
        }
        else if (rb.velocity.x < -0.1f)
        {
            transform.Rot(180 * (reverseSprite ? 1 : 0)-180);
        }
    }

    public void SetHurtMove(Vector2 pos)
    {
        transform.FlipToObj(pos.x);

        hurtDirect = (transform.CheckFlip() ? 1 : -1) * (-1);
        rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: cbProps.hurtForce * hurtDirect, yToSet: CombatProps.Ins.hurtForce / 2 * Mathf.Sin(transform.GetRotZFollowTargetByVector3(pos, true)));

        hurtCounter = cbProps.hurtTime;

        animator.SetBool("hurt", hurtCounter > 0);
    }

    public bool CheckGrounding()
    {
        if(EnviromentProps.Ins == null)
        {
            grouding = Physics2D.OverlapCircle(groundPoint.position, .2f, LayerMask.NameToLayer("Ground"));
        }
        else grouding = Physics2D.OverlapCircle(groundPoint.position, .2f, EnviromentProps.Ins.groundLayers);
        if (grouding)
        {
            canDoubleJump = true;
        }
        return grouding;
    }

    protected virtual void LateStart() { }
    public virtual void Death()
    {
        SpriteRenderer v = Instantiate(CombatProps.Ins.bodyPrefab, transform.position, Quaternion.identity);
        v.sprite = sr.sprite;
        Destroy(gameObject);
    }

    public void DropItems()
    {
        foreach(var v in EnviromentProps.Ins.itemRates)
        {
            if(Random.Range(0, 101) < v.rate)
            {
                EnviromentProps.Ins.InstantieItem(v.item, transform.position);
            }
        }
    }
}

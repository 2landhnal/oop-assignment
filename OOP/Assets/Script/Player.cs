using UnityEngine;
using UnityEngine.Events;

public class Player : Creature
{
    public static Player instance;
    protected float horizontalInput;
    public UnityEvent EnterEvent;
    protected SkillManager skillManager;
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] private Transform attackPoint;
    [HideInInspector]public float gemCollected = 0;
    [HideInInspector] public float coinCollected = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    protected override void LateStart()
    {
        base.LateStart();
        skillManager = GetComponent<SkillManager>();
    }

    protected override void InsideLateUpdate()
    {

    }

    public void DisableControlAndIdle()
    {
        DisableControl();
        skillManager.StopAllSkill();
        rb.velocity = Vector3.zero;
    }

    public void DisableControl()
    {
        animator.SetBool("canControl", false);
    }
    public void EnableControl()
    {
        animator.SetBool("canControl", true);
    }

    // Archery
    public void ReleaseBullet()
    {
        Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity).Attack(rb.rotation);
    }
    public override void BeforeDeath()
    {
        GameController.Ins.Lose();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("enter");
            EnterEvent?.Invoke();
        }
        if (CanControl())
        {
            //move
            tmpV2.x = horizontalInput * speed;
            tmpV2.y = rb.velocity.y;
            rb.velocity = tmpV2;

            //jump
            if (grouding || canDoubleJump)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (canDoubleJump && !grouding)
                    {
                        canDoubleJump = false;
                    }
                    tmpV2.x = rb.velocity.x;
                    tmpV2.y = canDoubleJump ? jumpForce : jumpForce;
                    rb.velocity = tmpV2;
                }
            }
            if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0f)
            {
                tmpV2.x = rb.velocity.x;
                tmpV2.y = rb.velocity.y * .5f;
                rb.velocity = tmpV2;
            }

            //attack
            if (Input.GetKeyDown(KeyCode.J))
            {
                animator.SetBool("attack", true);
                rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: 0);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                animator.SetBool("arche", true);
                rb.velocity = Vector2Extension.CreateVector2(rb.velocity, xToSet: 0);
            }
        }
    }
}

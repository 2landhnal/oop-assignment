using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Boss : Creature
{
    public static Boss instance;
    public Transform attackPoint;
    public Transform[] points;
    public float hideTime;
    float hideCounter;
    public Light2D lightBud;
    public string[] paraSkillStateList;
    HealthBar healthBar;
    private void Awake()
    {
        if(instance == null )
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    protected override void LateStart()
    {
        foreach (Transform t in points)
        {
            t.parent = null;
        }
        OnDeath.AddListener(GameController.Ins.AddBossDefeatCounter);
        OnDeath.AddListener(SetHealthBarOff);
    }

    private void OnEnable()
    {
        if (UIController.Ins == null) return;
        UIController.Ins.bossHB.SetObject(gameObject);
        healthBar = UIController.Ins.bossHB;
        healthBar.gameObject.SetActive(true);
    }

    public void SetHealthBarOff()
    {
        if (healthBar == null) return;
        healthBar.gameObject.SetActive(false);
    }

    public void ResetHideCounter()
    {
        hideCounter = hideTime;
    }

    protected override void InsideLateUpdate()
    {
        if (hideCounter > 0)
        {
            hideCounter -= Time.deltaTime;
            if(hideCounter <= 0)
            {
                Show();
            }
        }

        base.InsideLateUpdate();
        transform.FlipToEnemy(Player.instance.transform);
    }

    private void OnDisable()
    {
        SetHealthBarOff();
    }

    private void Hide()
    {
        animator.SetBool("hide", true);
    }

    public void PickRandomSkill()
    {
        animator.SetBool(paraSkillStateList[UnityEngine.Random.Range(0, paraSkillStateList.Length)], true);
    }

    public void Show()
    {
        transform.position = points[UnityEngine.Random.Range(0, points.Length)].position;
        animator.SetBool("show", true);
    }

    public void ReleaseBullet(Bullet obj)
    {
        Bullet bullet = Instantiate(obj, attackPoint.position, Quaternion.identity);
        bullet.SetTarget(Player.instance.gameObject, gameObject);
    }
    public void ReleaseAtp(AttackPoint obj)
    {
        AttackPoint atp = Instantiate(obj, Player.instance.transform.position, Quaternion.identity);
        atp.SetLayer(gameObject);
    }

    public override void SetHurtMove(Vector2 pos)
    {
        //base.SetHurtMove(pos);
    }

}

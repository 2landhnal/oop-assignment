using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSkill : SkillController
{
    public GameObject skillObj;
    [SerializeField] float radius;
    [SerializeField] bool fromSelf;
    Vector2 tempV2;
    SpriteRenderer tempSR;
    Collider2D[] contact;


    public int GetEnemyLayer()
    {
        if (skillManager.creature.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            return LayerMask.GetMask("Player");
        }
        return LayerMask.GetMask("Enemy");
    }

    private void OnEnable()
    {
        OnTriggerEnter.AddListener(TriggerEnter);
        OnSkillUpdate.AddListener(SkillUpdate);
        OnTriggeringStop.AddListener(TriggerStop);
    }

    protected override void TriggerEnter()
    {
        base.TriggerEnter();
        Player player = Player.instance;
        player.bulletPrefab = skillObj.GetComponent<Bullet>();
        player.target = null;
        contact = Physics2D.OverlapCircleAll(skillManager.creature.transform.position, radius, GetEnemyLayer());
        if (contact.Length != 0)
        {
            Debug.Log("here");
            player.target = contact[0].gameObject;
        }
    }
    protected override void TriggerStop()
    {
        Player.instance.bulletPrefab = null;
        base.TriggerStop();
    }

    protected override void SkillUpdate()
    {
        base.SkillUpdate();
    }
}

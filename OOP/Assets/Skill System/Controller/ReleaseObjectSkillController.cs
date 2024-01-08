using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseObjectSkillController : SkillController
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
        if (fromSelf)
        {
            Bullet tmpBullet = Instantiate(skillObj, skillManager.transform.root.transform.GetCenterPos(), Quaternion.identity).GetComponent<Bullet>();
            tmpBullet.SetLayer(skillManager.creature.gameObject);
            tmpBullet.SetTarget(contact[Random.Range(0, contact.Length)].gameObject, skillManager.creature.gameObject);
            return;
            //tmpBullet.transform.position = contact[Random.Range(0, contact.Length)].transform.GetCenterPos();
        }
        AttackPoint tmpAtp = Instantiate(skillObj).GetComponent<AttackPoint>();
        tmpAtp.SetLayer(skillManager.creature.gameObject);
        tmpAtp.transform.position = contact[Random.Range(0, contact.Length)].transform.GetCenterPos();
    }
    protected override void TriggerStop()
    {
        base.TriggerStop();
    }

    protected override void SkillUpdate()
    {
        base.SkillUpdate();
    }

    protected override void CheckForCondition()
    {
        contact = Physics2D.OverlapCircleAll(skillManager.creature.transform.position, radius, GetEnemyLayer());
        if (contact.Length != 0)
        {
            if(!IsCoolDowning)
            {
                Trigger();
            }
        }
    }
}

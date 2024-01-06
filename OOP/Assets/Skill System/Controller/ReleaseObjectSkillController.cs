using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseObjectSkillController : SkillController
{
    public AttackPoint skillObj;
    [SerializeField] float radius;
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

    public void TriggerEnter()
    {
        AttackPoint tmpAtp = Instantiate(skillObj);
        tmpAtp.SetLayer(skillManager.creature.gameObject);
        tmpAtp.transform.position = contact[Random.Range(0, contact.Length)].transform.GetCenterPos();
    }
    public void TriggerStop()
    {

    }

    public void SkillUpdate()
    {

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

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleSkill : SkillController
{
    [SerializeField] float radius;
    Vector2 tempV2;
    Collider2D[] contact;
    public float dis;


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
        contact = new Collider2D[0];
        contact = Physics2D.OverlapCircleAll(skillManager.creature.transform.position, radius, GetEnemyLayer());
        if (contact.Length != 0)
        {
            Debug.Log("here");
            player.target = contact[0].gameObject;
            player.skillEvent.RemoveAllListeners();
            player.skillEvent.AddListener(delegate { Tele(contact[Random.Range(0, contact.Length)].transform); });
        }
    }
    public void Tele(Transform target)
    {
        skillManager.creature.transform.position = (Vector2)target.position + new Vector2(dis, 0) * (transform.position.x < target.position.x ? 1 : -1);
        skillManager.creature.transform.FlipToObj(target.position.x);
    }
    protected override void TriggerStop()
    {
        //Player.instance.bulletPrefab = null;
        base.TriggerStop();
    }

    protected override void SkillUpdate()
    {
        base.SkillUpdate();
    }
}

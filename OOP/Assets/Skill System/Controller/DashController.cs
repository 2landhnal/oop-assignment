using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : SkillController
{
    private DashSkillData m_data;
    public float dashSpeed, afterImageDeltaTime;
    float afterImageCounter;
    Vector2 tempV2;
    SpriteRenderer tempSR;
    private void Awake()
    {
        m_data = (DashSkillData)skillData;
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
        tempV2.x = dashSpeed * transform.CheckFaceRight();
        tempV2.y = 0;
        skillManager.GetComponent<HealthManager>().immortal = true;
    }
    protected override void TriggerStop()
    {
        base.TriggerStop();
        skillManager.creature.rb.velocity = Vector2.zero;
        skillManager.GetComponent<HealthManager>().immortal = false;
    }

    protected override void SkillUpdate()
    {
        base.SkillUpdate();
        if(afterImageCounter > 0)
        {
            afterImageCounter -= Time.deltaTime;
        }
        else
        {
            afterImageCounter = afterImageDeltaTime;
            ReleaseAfterImage();
        }
        skillManager.creature.rb.velocity = tempV2;
        if(skillManager.creature.animator.GetBool("hurt")) ForceStop();
    }

    void ReleaseAfterImage()
    {
        tempSR = Instantiate(CombatProps.Ins.afterImagePrefab, transform.position, Quaternion.identity);
        tempSR.sprite = skillManager.creature.sr.sprite;
    }
}

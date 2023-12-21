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

    public void TriggerEnter()
    {
        tempV2.x = dashSpeed * transform.CheckFaceRight();
        tempV2.y = 0;
        skillManager.creature.animator.SetBool(animatorParam, true);
        skillManager.GetComponent<HealthManager>().immortal = true;
    }
    public void TriggerStop()
    {
        skillManager.creature.animator.SetBool(animatorParam, false);
        skillManager.creature.rb.velocity = Vector2.zero;
        skillManager.GetComponent<HealthManager>().immortal = false;
    }

    public void SkillUpdate()
    {
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

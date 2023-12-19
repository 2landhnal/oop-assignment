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
        obj.animator.SetBool(animatorParam, true);
    }
    public void TriggerStop()
    {
        obj.animator.SetBool(animatorParam, false);
        obj.rb.velocity = Vector2.zero;
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
        obj.rb.velocity = tempV2;
        if(obj.animator.GetBool("hurt")) ForceStop();
    }

    void ReleaseAfterImage()
    {
        tempSR = Instantiate(CombatProps.instance.afterImagePrefab, transform.position, Quaternion.identity);
        tempSR.sprite = obj.sr.sprite;
    }
}

using UnityEngine;
using UnityEngine.Events;

public class SkillController : MonoBehaviour
{
    [HideInInspector]public SkillManager skillManager;
    public SkillType type;
    public SkillData skillData;
    public string animatorParam;
    public KeyCode activeKey;

    private float coolDownCounter;
    private float triggerTimeRemain;
    private float damage;
    private bool isTriggered, isCoolDowning;

    public UnityEvent OnCoolDowning;
    // Call in ReduceTriggeredCounterTime func
    public UnityEvent OnSkillUpdate;
    public UnityEvent OnCoolDownStop;
    public UnityEvent OnTriggering;
    public UnityEvent OnTriggeringStop;
    public UnityEvent OnTriggerEnter;
    public UnityEvent<SkillType> OnStopWithType;

    public bool openAtStart = false;

    public float coolDownProgress
    {
        get => coolDownCounter / skillData.coolDownTime;
    }

    public float triggerTimeRemainRate
    {
        get => triggerTimeRemain / skillData.triggerTime;
    }
    public float CoolDownCounter { get => coolDownCounter;}
    public bool IsCoolDowning { get => isCoolDowning;}
    public bool IsTriggered { get => isTriggered;}

    public virtual void LoadData()
    {
        if (skillData == null) return;
        damage = skillData.damage;
    }

    private void Update()
    {
        CoreHandle();
        if(activeKey != KeyCode.None)
        {
            CheckForKeyCode();
        }
    }

    private void CoreHandle() 
    { 
        ReduceCoolDownCounter();
        ReduceTriggerTimeRemain();
    }

    private void CheckForKeyCode()
    {
        if(Input.GetKeyDown(activeKey))
        {
            Trigger();
        }
    }

    public void ForceStop()
    {
        isTriggered = false;
        LoadData();
        OnTriggeringStop?.Invoke();
    }

    public void Stop()
    {
        isTriggered = false;

        OnStopWithType?.Invoke(type);
        OnTriggeringStop?.Invoke();
        Debug.Log("skill stoped");
    }

    public void Trigger()
    {
        if (IsCoolDowning) return;
        coolDownCounter = skillData.coolDownTime;
        triggerTimeRemain = skillData.triggerTime;
        isCoolDowning = true;
        isTriggered = true;
        OnTriggerEnter?.Invoke();
        Debug.Log("skill triggered");
    }

    public void ReduceCoolDownCounter()
    {
        if (!isCoolDowning) return;
        coolDownCounter -= Time.deltaTime;
        OnCoolDowning?.Invoke();
        if (coolDownCounter > 0) return;
        isCoolDowning = false;
        OnCoolDownStop?.Invoke();
    }
    public void ReduceTriggerTimeRemain()
    {
        if (!isTriggered) return;
        triggerTimeRemain -= Time.deltaTime;
        if (triggerTimeRemain <= 0) Stop();
        OnSkillUpdate?.Invoke();
        OnTriggering?.Invoke();
    }
}

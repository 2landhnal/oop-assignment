using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private Image skillIcon;
    [SerializeField] private Text coolDownTxt, activeKeyTxt;
    [SerializeField] private Slider triggeredProgressSlider, coolDownSlider;
    [SerializeField] private Button btnComponent;

    private SkillController skillController;

    #region EVENTS
    private void RegisterEvent()
    {
        if (skillController == null) return;
        skillController.OnCoolDowning.AddListener(UpdateUI);
        skillController.OnTriggering.AddListener(UpdateTriggeredProgressTxt);
        skillController.OnCoolDownStop.AddListener(UpdateUI);
    }

    private void UnregisterEvent()
    {
        if (skillController == null) return;
        skillController.OnCoolDowning.RemoveListener(UpdateUI);
        skillController.OnTriggering.RemoveListener(UpdateTriggeredProgressTxt);
        skillController.OnCoolDownStop.RemoveListener(UpdateUI);
    }
    #endregion

    public void Initialize(SkillType type)
    {
        skillController = SkillButtonDrawer.Ins.playerSkillManager.GetSkillControllerCloneByType(type);
        UpdateUI();
        btnComponent.onClick.RemoveAllListeners();
        btnComponent.onClick.AddListener(TriggerSkill);
        if(skillController.activeKey != KeyCode.None)activeKeyTxt.text = skillController.activeKey.ToString();
        activeKeyTxt.gameObject.SetActive(skillController.activeKey != KeyCode.None);
        RegisterEvent();
    }

    private void UpdateUI()
    {
        if (skillController == null) return;
        skillIcon.sprite = skillController.skillData.skillIcon;
        UpdateCoolDownTxt();
    }

    private void UpdateTriggeredProgressTxt()
    {
        if (skillController == null || triggeredProgressSlider == null) return;
        triggeredProgressSlider.value = skillController.triggerTimeRemainRate;
    }

    private void UpdateCoolDownTxt()
    {
        if(coolDownTxt != null)
        {
            coolDownTxt.text = skillController.CoolDownCounter.ToString("f1");
        }
        if (!coolDownSlider) return;
        coolDownSlider.value = skillController.CoolDownCounter / skillController.skillData.coolDownTime;
        btnComponent.interactable = !skillController.IsCoolDowning;

        coolDownSlider.gameObject.SetActive(skillController.IsCoolDowning);
        coolDownTxt.gameObject.SetActive(skillController.IsCoolDowning);
    }

    private void TriggerSkill()
    {
        if(skillController == null) return;
        skillController.Trigger();
    }

    private void OnDestroy()
    {
        UnregisterEvent();
    }
}

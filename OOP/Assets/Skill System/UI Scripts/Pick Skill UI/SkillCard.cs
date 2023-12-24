using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour
{
    [SerializeField] protected Image skillIcon, cardBG;
    [SerializeField] protected Text nameTxt, descriptionTxt;
    [SerializeField] protected Button btnComponent;

    protected SkillController skillControllerPrefab;

    public Button BtnComponent { get => btnComponent; }

    public void Initialize(SkillController controller)
    {
        skillControllerPrefab = controller;
        UpdateUI();
        BtnComponent.onClick.RemoveAllListeners();
        BtnComponent.onClick.AddListener(Trigger);
    }

    protected virtual void UpdateUI()
    {
        if (skillControllerPrefab == null) return;
        skillIcon.sprite = skillControllerPrefab.skillData.skillIcon;
        cardBG.sprite = skillControllerPrefab.skillData.backGroundCard;
        nameTxt.text = skillControllerPrefab.skillData.name;
        descriptionTxt.text = skillControllerPrefab.skillData.description;
    }

    protected virtual void Trigger()
    {
        if(skillControllerPrefab == null) return;
        Player.instance.GetComponent<SkillManager>()?.AddSkill(skillControllerPrefab);
    }
}

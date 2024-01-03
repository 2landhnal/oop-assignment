using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfoPanel : Singleton<SkillInfoPanel>
{
    protected override void Awake()
    {
        MakeSingleton(false);
    }
    [HideInInspector]public SkillController skillControllerPrefab;
    public Image avtImg;
    public TextMeshProUGUI nameTxt, desTxt, priceTxt;
    public GameObject buySection;
    public Button buyBtn;
    AccountManager.AccountGameData currentAccountGameData;
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnShowUp(SkillController controller)
    {
        Debug.Log("Show");
        skillControllerPrefab = controller;
        currentAccountGameData = AccountManager.accountGameDataList.Single(s => s.username == AccountManager.currentUsername);
        buySection.SetActive(!currentAccountGameData.skillGainedIdList.Contains(RuntimeData.Ins.skillControllerList.IndexOf(skillControllerPrefab)));
        buyBtn.interactable = currentAccountGameData.gemAmount >= skillControllerPrefab.skillData.price;
        avtImg.sprite = skillControllerPrefab.skillData.skillIcon;
        nameTxt.text = skillControllerPrefab.skillData.name;
        desTxt.text = skillControllerPrefab.skillData.description;
        priceTxt.text = skillControllerPrefab.skillData.price.ToString();

        gameObject.SetActive(true);
    }
    public void Purchase()
    {
        if (AccountManager.PurchaseGem(skillControllerPrefab.skillData.price))
        {
            AccountManager.UnlockSkill(skillControllerPrefab);
        }
    }
}

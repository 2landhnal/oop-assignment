using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemCard : MonoBehaviour
{
    [SerializeField] private Image blackPanel;
    [SerializeField] private Text priceTxt;
    [SerializeField] protected Image skillIcon;
    [SerializeField] protected Button btnComponent;

    public SkillController skillControllerPrefab { private set; get; }

    public Button BtnComponent { get => btnComponent; }

    public void Initialize(SkillController controller)
    {
        skillControllerPrefab = controller;
        UpdateUI();
        btnComponent.onClick.RemoveAllListeners();
        btnComponent.onClick.AddListener(Trigger);
        btnComponent.onClick.AddListener(delegate { SkillInfoPanel.Ins.OnShowUp(this); });
    }

    public virtual void UpdateUI()
    {
        if (skillControllerPrefab == null) return;
        if(skillControllerPrefab.openAtStart || AccountManager.accountGameDataList.Single(s=>s.username == AccountManager.currentUsername).skillGainedIdList.Contains(RuntimeData.Ins.skillControllerList.IndexOf(skillControllerPrefab)))
        {
            blackPanel.gameObject.SetActive(false);
        }
        skillIcon.sprite = skillControllerPrefab.skillData.skillIcon;
        priceTxt.text = skillControllerPrefab.skillData.price.ToString();
    }

    protected virtual void Trigger()
    {
        // show purchase and infor panel
    }

}

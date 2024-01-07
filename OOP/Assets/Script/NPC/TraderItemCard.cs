using UnityEngine;
using UnityEngine.UI;

public class TraderItemCard : MonoBehaviour
{
    [SerializeField] private Text priceTxt;
    [SerializeField] protected Image icon;
    [SerializeField] protected Button btnComponent;
    Trader trader;

    protected Trader.Item_Price itemData;
    SkillController skillController;

    public Button BtnComponent { get => btnComponent; }

    public void Initialize(Trader.Item_Price data, Trader t)
    {
        itemData = data;
        trader = t;
        UpdateUI();
        BtnComponent.onClick.RemoveAllListeners();
        BtnComponent.onClick.AddListener(Trigger);
    }
    private void Update()
    {
        btnComponent.interactable = (Player.instance.GetComponent<ResourceManager>().coinAmount >= itemData.price);
    }

    protected virtual void UpdateUI()
    {
        if (itemData.data == null) return;
        if(itemData.data.type == EnviromentProps.Item.Skill)
        {
            skillController = RuntimeData.Ins.skillControllerList[RuntimeData.Ins.GetRandomSkillControllerIdCanCollectedButNotCollectedYet()];
            icon.sprite = skillController.skillData.skillIcon;
        }
        else icon.sprite = itemData.data.sprite;
        priceTxt.text = itemData.price.ToString();
    }

    protected virtual void Trigger()
    {
        trader.itemDataList.Remove(itemData);
        Destroy(gameObject);
        Player.instance.GetComponent<ResourceManager>().UseCoin(itemData.price);
        if(itemData.data.type == EnviromentProps.Item.Skill)
        {
            Player.instance.GetComponent<SkillManager>()?.AddSkill(skillController.type);
        }
        if (itemData.data.type == EnviromentProps.Item.HP)
        {
            Player.instance.GetComponent<HealthManager>().RecorverHP(itemData.data.hpAmount);
        }
    }

    private void Start()
    {
        
    }
}

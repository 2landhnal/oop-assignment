using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<SkillController> skillControllerList;

    public Transform shopPanel;
    public Transform contentGrid;
    public ShopItemCard itemCardPrefab;
    public void DrawShop()
    {
        shopPanel.gameObject.SetActive(true);
        Helper.ClearChilds(contentGrid);
        if (skillControllerList == null || skillControllerList.Count == 0) return;
        foreach (SkillController skill in skillControllerList)
        {
            var skillCardClone = Instantiate(itemCardPrefab);
            Helper.AssignToRoot(contentGrid, skillCardClone.transform, Vector3.zero, Vector3.one);
            skillCardClone.Initialize(skill);
        }
    }

    public void OnClick()
    {
        Debug.Log("clicked");
        if(shopPanel.gameObject.activeSelf)
        {
            Player.instance.EnableControl();
            shopPanel.gameObject.SetActive(false);
        }
        else
        {
            Player.instance.DisableControlAndIdle();
            shopPanel.gameObject.SetActive(true);
            DrawShop();
        }
    }
}

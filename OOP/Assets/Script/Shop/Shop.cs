using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Transform shopPanel;
    public Transform contentGrid;
    public ShopItemCard itemCardPrefab;
    public void DrawShop()
    {
        shopPanel.gameObject.SetActive(true);
        Helper.ClearChilds(contentGrid);
        if (RuntimeData.Ins.skillControllerList == null || RuntimeData.Ins.skillControllerList.Count == 0) return;
        foreach (SkillController skill in RuntimeData.Ins.skillControllerList)
        {
            if(skill.isPrivateSkill) continue;
            var skillCardClone = Instantiate(itemCardPrefab);
            Helper.AssignToRoot(contentGrid, skillCardClone.transform, Vector3.zero, Vector3.one);
            skillCardClone.Initialize(skill);
        }
    }
}

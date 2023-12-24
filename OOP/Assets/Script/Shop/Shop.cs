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
            shopPanel.gameObject.SetActive(false);
        }
        else
        {
            shopPanel.gameObject.SetActive(true);
            DrawShop();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("add");
            Player.instance.EnterEvent.RemoveAllListeners();
            Player.instance.EnterEvent.AddListener(OnClick);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("remove");
            Player.instance.EnterEvent.RemoveListener(OnClick);
        }
    }
}

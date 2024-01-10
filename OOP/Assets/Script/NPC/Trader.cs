using System;
using System.Collections.Generic;
using UnityEngine;
using static EnviromentProps;

public class Trader : MonoBehaviour
{
    public TraderItemCard itemCardPrefab;
    public GameObject triangle;
    public int itemAmount;
    public List<Item_Price> itemDataList = new List<Item_Price>();

    [Serializable]
    public struct Item_Price
    {
        public TraderItemData data;
        public int price;
    }
    private void Start()
    {
        if (triangle) triangle.SetActive(false);
        for (int i = 0; i < itemAmount; i++)
        {
            TraderItemData tmp = RuntimeData.Ins.traderItemList[UnityEngine.Random.Range(0, RuntimeData.Ins.traderItemList.Count)];
            Item_Price tmp2 = new Item_Price();
            tmp2.data = tmp;
            tmp2.price = (int)UnityEngine.Random.Range(tmp.price.x, tmp.price.y);
            itemDataList.Add(tmp2);
        }
    }
    public void DrawShop()
    {
        UIController.Ins.shopPanel.gameObject.SetActive(true);
        Helper.ClearChilds(UIController.Ins.contentGrid);
        foreach (Trader.Item_Price itemData in itemDataList)
        {
            var skillCardClone = Instantiate(itemCardPrefab);
            Helper.AssignToRoot(UIController.Ins.contentGrid, skillCardClone.transform, Vector3.zero, Vector3.one);
            skillCardClone.Initialize(itemData, this);
        }
    }

    public void OnClick()
    {
        Debug.Log("clicked");
        if (UIController.Ins.shopPanel.gameObject.activeSelf)
        {
            Time.timeScale = 1f;
            UIController.Ins.shopPanel.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            UIController.Ins.shopPanel.gameObject.SetActive(true);
            DrawShop();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("add");
            triangle.SetActive(true);
            Player.instance.SpaceEvent.RemoveAllListeners();
            Player.instance.SpaceEvent.AddListener(OnClick);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("remove");
            triangle.SetActive(false);
            Player.instance.SpaceEvent.RemoveListener(OnClick);
        }
    }
}

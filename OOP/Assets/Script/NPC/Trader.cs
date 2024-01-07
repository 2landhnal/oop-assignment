using System;
using System.Collections.Generic;
using UnityEngine;
using static EnviromentProps;

public class Trader : MonoBehaviour
{
    public Transform shopPanel;
    public Transform contentGrid;
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
        shopPanel.gameObject.SetActive(true);
        Helper.ClearChilds(contentGrid);
        foreach (Trader.Item_Price itemData in itemDataList)
        {
            var skillCardClone = Instantiate(itemCardPrefab);
            Helper.AssignToRoot(contentGrid, skillCardClone.transform, Vector3.zero, Vector3.one);
            skillCardClone.Initialize(itemData, this);
        }
    }

    public void OnClick()
    {
        Debug.Log("clicked");
        if (shopPanel.gameObject.activeSelf)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("add");
            triangle.SetActive(true);
            Player.instance.EnterEvent.RemoveAllListeners();
            Player.instance.EnterEvent.AddListener(OnClick);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("remove");
            triangle.SetActive(false);
            Player.instance.EnterEvent.RemoveListener(OnClick);
        }
    }
}

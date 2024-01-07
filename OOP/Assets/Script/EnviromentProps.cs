using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnviromentProps : Singleton<EnviromentProps>, IObserve
{
    public LayerMask groundLayers, platformLayers;
    public float jumpOutWallSpeed;
    public Transform spawnPoint;
    public List<ItemRate> itemRates;
    [Serializable]
    public enum Item
    {
        Coin, Gem, HP, Skill
    }
    [Serializable]
    public struct ItemRate
    {
        public float rate;
        public Item item;
    }

    public void InstantieItem(Item item, Vector2 pos)
    {
        switch(item)
        {
            case Item.Coin:
                Instantiate(CombatProps.Ins.coinPrefab, pos, Quaternion.identity);
                break;
            case Item.Gem:
                Instantiate(CombatProps.Ins.gemPrefab, pos, Quaternion.identity);
                break;
            case Item.HP:
                break;
            case Item.Skill:
                break;
        }
    }

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    public void SubjectCalled()
    {
        if (Player.instance == null) return;
        if (spawnPoint == null) return;
        Player.instance.transform.position = spawnPoint.position;
    }
}

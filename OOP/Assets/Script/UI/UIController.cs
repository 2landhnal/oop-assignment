using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    protected override void Awake()
    {
        MakeSingleton(false);
    }
    public TextMeshProUGUI gemLabel, coinLabel;
   ResourceManager resourceManager;

    private void Update()
    {
        if(resourceManager == null)
        {
            resourceManager = Player.instance.GetComponent<ResourceManager>();
        }
        gemLabel.text = resourceManager.gemAmount.ToString();
        coinLabel.text = resourceManager.coinAmount.ToString();
    }
}

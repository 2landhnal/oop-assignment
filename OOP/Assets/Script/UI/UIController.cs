using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : Singleton<UIController>
{
    public TextMeshProUGUI gemLabel, coinLabel;
    ResourceManager resourceManager;
    public GameObject resultSection;
    public TextMeshProUGUI gemLabelResult, coinLabelResult;
    protected override void Awake()
    {
        MakeSingleton(false);
    }

    private void Update()
    {
        if (Player.instance == null) return;
        if (resourceManager == null)
        {
            resourceManager = Player.instance.GetComponent<ResourceManager>();
        }
        gemLabel.text = resourceManager.gemAmount.ToString();
        coinLabel.text = resourceManager.coinAmount.ToString();
    }
    public void Menu()
    {
        SceneManager.LoadSceneAsync("Lobby v2");
        Time.timeScale = 1f;
    }

    public void ShowResult()
    {
        gemLabelResult.text = resourceManager.gemAmount.ToString();
        coinLabelResult.text = resourceManager.coinAmount.ToString();
        resultSection.SetActive(true);
        Time.timeScale = 0f;
    }
}

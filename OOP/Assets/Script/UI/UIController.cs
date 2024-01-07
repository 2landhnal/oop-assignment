using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : Singleton<UIController>, IObserve
{
    public TextMeshProUGUI gemLabel, coinLabel;
    ResourceManager resourceManager;
    public GameObject resultSection;
    public TextMeshProUGUI gemLabelResult, coinLabelResult, playerName;
    public Image avt;
    public GameObject pauseSection;
    protected override void Awake()
    {
        MakeSingleton(false);
    }

    public void SubjectCalled()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        if(AccountManager.currentUsername != null)
        { 
            avt.sprite = RuntimeData.Ins.avtSprites[AccountManager.GetCurrentUserInfo().avtSpriteId];
            playerName.text = AccountManager.currentUsername;
        }
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Menu()
    {
        if (Player.instance != null) Destroy(Player.instance.gameObject);
        SceneManager.LoadSceneAsync("Lobby v2");
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseSection.SetActive(true);
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
        pauseSection.SetActive(false);
    }

    public void ShowResult()
    {
        gemLabelResult.text = resourceManager.gemAmount.ToString();
        coinLabelResult.text = resourceManager.coinAmount.ToString();
        resultSection.SetActive(true);
        Time.timeScale = 0f;
    }
}

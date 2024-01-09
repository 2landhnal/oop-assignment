using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignInSection : MonoBehaviour
{
    public TextMeshProUGUI warningTxt;
    public MessagePanel messPanel;
    public InputField username, password;
    List<AccountManager.AccountInfor> accountList = new List<AccountManager.AccountInfor>();
    public GameObject playerInfoSection;
    private void OnEnable()
    {
        warningTxt.gameObject.SetActive(false);
        AudioManager.Ins.MenuMusicButton(true);
    }
    public void SignIn()
    {
        accountList = FileHandler.ReadListFromJSON<AccountManager.AccountInfor>(AccountManager.fileName_accountInfo);
        if (!accountList.Any(s => s.username == username.text))
        {
            Warn("Username doesn't exist");
            return;
        }
        AccountManager.AccountInfor tempAcc = accountList.Single(s => s.username == username.text);
        if(tempAcc.password != password.text)
        {
            Warn("Invalid password");
            return;
        }
        warningTxt.gameObject.SetActive(false);
        SignInSuccess();
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Warn(string txt)
    {
        messPanel.gameObject.SetActive(false);
        warningTxt.gameObject.SetActive(true);
        warningTxt.text = txt;
    }

    public void SignInSuccess()
    {
        AccountManager.SetCurrentUsername(username.text);
        SceneManager.LoadSceneAsync("Lobby v2");
    }
}

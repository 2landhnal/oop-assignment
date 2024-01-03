using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class SignUpSection : MonoBehaviour
{
    public TextMeshProUGUI warningTxt;
    public InputField username, password, passwordConfirm, email;
    List<AccountManager.AccountInfor> accountList = new List<AccountManager.AccountInfor>();
    public SignInSection signInSection;
    private void OnEnable()
    {
        warningTxt.gameObject.SetActive(false);
    }
    public void SignUp()
    {
        accountList = FileHandler.ReadListFromJSON<AccountManager.AccountInfor>(AccountManager.fileName_accountInfo);
        if (password.text != passwordConfirm.text)
        {
            Warn("Password and confirm password doesn't match");
            return;
        }
        if (email.text.IndexOf('@') <= 0)
        {
            Warn("Email invalid");
            return;
        }
        if (accountList.Any(s=>s.username == username.text))
        {
            Warn("Already existed");
            return;
        }
        warningTxt.gameObject.SetActive(false);
        accountList.Add(new AccountManager.AccountInfor(username.text, password.text, email.text));
        FileHandler.SaveToJSON(accountList, AccountManager.fileName_accountInfo);

        SignUpSuccess();
    }

   void Warn(string txt)
    {
        warningTxt.gameObject.SetActive(true);
        warningTxt.text = txt;
    }

    void SignUpSuccess()
    {
        GenerateInitInfo();

        email.text = "";
        password.text = "";
        username.text = "";
        passwordConfirm.text = "";

        signInSection.messPanel.ShowMessage("Sign up success!");

        signInSection.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void GenerateInitInfo()
    {
        // userInfo
        List<AccountManager.UserInfo> userInfoList = FileHandler.ReadListFromJSON<AccountManager.UserInfo>(AccountManager.fileName_userInfo);
        userInfoList.Add(new AccountManager.UserInfo(username.text));
        FileHandler.SaveToJSON(userInfoList, AccountManager.fileName_userInfo);

        // accountGameData
        List<AccountManager.AccountGameData> accountGameDataList = FileHandler.ReadListFromJSON<AccountManager.AccountGameData>(AccountManager.fileName_accountGameData);
        accountGameDataList.Add(new AccountManager.AccountGameData(username.text, new List<int>()));
        FileHandler.SaveToJSON(accountGameDataList, AccountManager.fileName_accountGameData);

        // playingGameData
        List<AccountManager.PlayingGameData> playingGameDataList = FileHandler.ReadListFromJSON<AccountManager.PlayingGameData>(AccountManager.fileName_playingGameData);
        playingGameDataList.Add(new AccountManager.PlayingGameData(username.text, new List<int>()));
        FileHandler.SaveToJSON(playingGameDataList, AccountManager.fileName_playingGameData);
    }
}

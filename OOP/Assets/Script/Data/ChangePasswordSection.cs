using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangePasswordSection : MonoBehaviour
{
    public TextMeshProUGUI warningTxt;
    public MessagePanel messagePanel;
    public InputField currentPassword, password, passwordConfirm;
    List<AccountManager.AccountInfor> accountList = new List<AccountManager.AccountInfor>();
    public GameObject accountDetailSection;
    private void OnEnable()
    {
        warningTxt.gameObject.SetActive(false);
    }
    public void ChangePassword()
    {
        accountList = FileHandler.ReadListFromJSON<AccountManager.AccountInfor>(AccountManager.fileName_accountFile);
        AccountManager.AccountInfor tempAcc = accountList.Single(s => s.username == AccountManager.currentUsername);
        if (tempAcc.password != currentPassword.text)
        {
            Warn("Invalid current password");
            return;
        }
        if (password.text != passwordConfirm.text)
        {
            Warn("New password and confirm new password doesn't match");
            return;
        }
        warningTxt.gameObject.SetActive(false);
        accountList.Single(s => s.username == AccountManager.currentUsername).password = password.text;
        FileHandler.SaveToJSON(accountList, AccountManager.fileName_accountFile);
        ChangePassSucceded();
    }
    void ChangePassSucceded()
    {
        currentPassword.text = "";
        password.text = "";
        passwordConfirm.text = "";

        messagePanel.ShowMessage("Change password success!");
        accountDetailSection.SetActive(true);
        gameObject.SetActive(false);
    }
    void Warn(string txt)
    {
        warningTxt.gameObject.SetActive(true);
        warningTxt.text = txt;
    }
}

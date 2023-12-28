using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static AccountManager;

public class ChangePasswordSection : MonoBehaviour
{
    public TextMeshProUGUI warningTxt;
    public MessagePanel messagePanel;
    public InputField currentPassword, password, passwordConfirm;
    List<AccountInfor> accountList = new List<AccountInfor>();
    public GameObject accountDetailSection;
    private void OnEnable()
    {
        warningTxt.gameObject.SetActive(false);
    }
    public void ChangePassword()
    {
        accountList = AccountManager.accountInfoList;
        AccountInfor tempAcc = accountList.Single(s => s.username == currentUsername);
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
        accountList.Single(s => s.username == currentUsername).password = password.text;
        FileHandler.SaveToJSON(accountList, fileName_accountInfo);
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

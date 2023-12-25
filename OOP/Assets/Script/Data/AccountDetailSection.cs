using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AccountDetailSection : MonoBehaviour
{
    public InputField email, username;
    public GameObject changePasswordSection;
    private void OnEnable()
    {
        LoadData();
    }

    void LoadData()
    {
        AccountManager.AccountInfor accountData = FileHandler.ReadListFromJSON<AccountManager.AccountInfor>(AccountManager.fileName_accountFile).Single(s => s.username == AccountManager.currentUsername);
        email.text = accountData.email;
        username.text = accountData.username;
    }

    public void OnClickChangePass()
    {
        changePasswordSection.SetActive(true);
        gameObject.SetActive(false);
    }
}

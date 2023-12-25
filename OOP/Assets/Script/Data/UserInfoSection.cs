using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoSection : MonoBehaviour
{
    public Image avt;
    public InputField name, age;
    public GameObject detailSection;

    private void OnEnable()
    {
        LoadData();
    }

    void LoadData()
    {
        AccountManager.UserInfo accInfo = FileHandler.ReadListFromJSON<AccountManager.UserInfo>(AccountManager.fileName_userInfo).Single(s=>s.username == AccountManager.currentUsername);
        if(accInfo != null)
        {
            if(accInfo.avtSprite != null) avt.sprite = accInfo.avtSprite;
            name.text = accInfo.name;
            age.text = accInfo.age.ToString();
        }
        else
        {
            Debug.Log("Error! Can't find userInfor match currentUsername");
        }
    }
    public void Save()
    {
        List<AccountManager.UserInfo> userInfoList = FileHandler.ReadListFromJSON<AccountManager.UserInfo>(AccountManager.fileName_userInfo);
        userInfoList.Single(s => s.username == AccountManager.currentUsername).avtSprite = avt.sprite;
        userInfoList.Single(s => s.username == AccountManager.currentUsername).name = name.text;
        userInfoList.Single(s => s.username == AccountManager.currentUsername).age = int.Parse(age.text);
        FileHandler.SaveToJSON(userInfoList, AccountManager.fileName_userInfo);
    }

    public void Detail()
    {
        detailSection.SetActive(true);
        gameObject.SetActive(false);
    }
}

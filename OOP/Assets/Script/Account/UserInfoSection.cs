using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;
using static AccountManager;

public class UserInfoSection : MonoBehaviour
{
    public Image avt, miniAvt;
    public InputField name, age;
    public GameObject detailSection;
    public GameObject selectAvtPanel;
    public Transform grid;
    public AvatarToPick prefabAvt;

    private void Start()
    {
        foreach(Sprite s in RuntimeData.Ins.avtSprites)
        {
            AvatarToPick tempImage = Instantiate(prefabAvt);
            Helper.AssignToRoot(grid, tempImage.transform, Vector3.zero, Vector3.one);
            tempImage.avatar.sprite = s;
            tempImage.GetComponent<Button>().onClick.RemoveAllListeners();
            tempImage.GetComponent<Button>().onClick.AddListener(delegate { ChangeAvt(s); }) ;
        }
    }
    public void ChangeAvt(Sprite sprite)
    {
        avt.sprite = sprite;
        miniAvt.sprite = sprite;   
        selectAvtPanel.SetActive(false);
    }

    private void OnEnable()
    {
        LoadData();
    }

    void LoadData()
    {
        AccountManager.UserInfo accInfo = FileHandler.ReadListFromJSON<AccountManager.UserInfo>(AccountManager.fileName_userInfo).Single(s=>s.username == AccountManager.currentUsername);
        if(accInfo != null)
        {
            avt.sprite = RuntimeData.Ins.avtSprites[accInfo.avtSpriteId];
            miniAvt.sprite = avt.sprite;
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
        userInfoList.Single(s => s.username == AccountManager.currentUsername).avtSpriteId = RuntimeData.Ins.avtSprites.GetIndex(avt.sprite);
        userInfoList.Single(s => s.username == AccountManager.currentUsername).name = name.text;
        userInfoList.Single(s => s.username == AccountManager.currentUsername).age = int.Parse(age.text);
        FileHandler.SaveToJSON(userInfoList, AccountManager.fileName_userInfo);
        Lobby.Ins.UpdateUI();
    }

    public void OpenAvtSelectSection()
    {
        selectAvtPanel.SetActive(true);
    }

    public void Detail()
    {
        detailSection.SetActive(true);
        gameObject.SetActive(false);
    }
}

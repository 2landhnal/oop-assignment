using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static AccountManager;
using CharacterInfo = AccountManager.CharacterInfo;

public class Lobby : MonoBehaviour
{
    public Button continueBtn;
    public string startScene;

    PlayingGameData playingGameData;
    AccountGameData accountGameData;
    UserInfo userInfo;
    List<AccountGameData> accountGameDataList;
    List<PlayingGameData> playingGameDataList;

    public List<CharacterInfo> characterInfoList;
    public Animator selectingCharacterAnimator;
    public Image avatarImg;
    public TextMeshProUGUI userNameTxt, gemAmountTxt;
    public CharacterCard selectCharacterPrefab;
    public Transform grid, selectCharacterSection;


    private void Start()
    {
        Debug.Log(currentUsername);
        if(currentUsername == null)
        {
            SceneManager.LoadScene("Sign In");
            return;
        }
        accountGameDataList = AccountManager.accountGameDataList;
        if (!accountGameDataList.Any(s => s.username == currentUsername))
        {
            accountGameData = new AccountGameData(currentUsername, new List<SkillController>());
            accountGameDataList.Add(accountGameData);
            FileHandler.SaveToJSON(accountGameDataList, fileName_accountGameData);
        }
        accountGameData = accountGameDataList.Single(s => s.username == currentUsername);

        playingGameDataList = AccountManager.playingGameDataList;
        if (!playingGameDataList.Any(s => s.username == currentUsername))
        {
            playingGameData = new PlayingGameData(currentUsername, new List<SkillController>());
            playingGameDataList.Add(playingGameData);
            FileHandler.SaveToJSON(playingGameDataList, fileName_playingGameData);
        }
        playingGameData = playingGameDataList.Single(s => s.username == currentUsername);

        userInfo = AccountManager.userInfoList.Single(s=>s.username == currentUsername);
        if (userInfo == null) return;


        continueBtn.interactable = accountGameData.hasPlayingData;
        if(playingGameData.character == null && accountGameData.selectingCharacterInfo == null)
        {
            selectingCharacterAnimator.runtimeAnimatorController = characterInfoList[0].animatorController;
        }
        if (accountGameData.selectingCharacterInfo == null)
        {
            selectingCharacterAnimator.runtimeAnimatorController = playingGameData.character.animatorController;
        }
        else if (accountGameData.selectingCharacterInfo == null)
        {
            selectingCharacterAnimator.runtimeAnimatorController = accountGameData.selectingCharacterInfo.animatorController;
        }
        Debug.Log(userInfo.avtSprite == null);
        Debug.Log(avatarImg.sprite == null);
        if (userInfo.avtSprite != null && avatarImg.sprite != null) avatarImg.sprite = userInfo.avtSprite;
        userNameTxt.text = userInfo.name;
        gemAmountTxt.text = accountGameData.gemAmount.ToString();
    }
    public void NewGame()
    {
        SceneManager.LoadScene(startScene);
    }

    public void SignOut()
    {
        SetCurrentUsername(null);
        SceneManager.LoadScene("Sign In");
    }
    public void OpenSelectCharacterSection()
    {
        selectCharacterSection.gameObject.SetActive(true);
        Helper.ClearChilds(grid);
        foreach(CharacterInfo character in characterInfoList)
        {
            CharacterCard tempCharacterCard = Instantiate(selectCharacterPrefab);
            Helper.AssignToRoot(grid, tempCharacterCard.transform, Vector3.zero, Vector3.one);
            tempCharacterCard.animator.runtimeAnimatorController = character.animatorController;
            tempCharacterCard.nameLabel.text = character.name;  
            tempCharacterCard.btn.onClick.RemoveAllListeners();
            tempCharacterCard.btn.onClick.AddListener(delegate { SelectCharacter(character); });
        }
    }
    void SelectCharacter(CharacterInfo character)
    {
        accountGameData.selectingCharacterInfo = character;
        accountGameDataList.Single(s=>s.username == currentUsername).selectingCharacterInfo = character;
        FileHandler.SaveToJSON(accountGameDataList, fileName_accountGameData);

        selectCharacterSection.gameObject.SetActive(false);
    }
}

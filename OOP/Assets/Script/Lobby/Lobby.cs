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
    public TextMeshProUGUI userNameTxt;
    public CharacterCard selectCharacterPrefab;
    public Transform grid, selectCharacterSection;


    private void Start()
    {
        Debug.Log(currentUsername);
        accountGameDataList = FileHandler.ReadListFromJSON<AccountGameData>(fileName_accountGameData);
        if (!accountGameDataList.Any(s => s.username == currentUsername))
        {
            accountGameData = new AccountGameData(currentUsername, new List<SkillController>());
            accountGameDataList.Add(accountGameData);
            FileHandler.SaveToJSON(accountGameDataList, fileName_accountGameData);
        }
        accountGameData = accountGameDataList.Single(s => s.username == currentUsername);

        playingGameDataList = FileHandler.ReadListFromJSON<PlayingGameData>(fileName_playingGameData);
        if (!playingGameDataList.Any(s => s.username == currentUsername))
        {
            playingGameData = new PlayingGameData(currentUsername, new List<SkillController>());
            playingGameDataList.Add(playingGameData);
            FileHandler.SaveToJSON(playingGameDataList, fileName_playingGameData);
        }
        playingGameData = playingGameDataList.Single(s => s.username == currentUsername);

        userInfo = FileHandler.ReadListFromJSON<UserInfo>(fileName_userInfo).Single(s=>s.username == currentUsername);
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
        avatarImg.sprite = userInfo.avtSprite;
        userNameTxt.text = userInfo.name;
    }
    public void NewGame()
    {
        SceneManager.LoadScene(startScene);
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

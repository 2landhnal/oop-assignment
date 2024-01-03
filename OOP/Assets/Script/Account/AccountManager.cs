using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AccountManager
{
    public static string currentUsername { get; private set; }
    public static string fileName_accountInfo = "AccountFile.json";
    public static string fileName_userInfo = "UserInfo.json";
    public static string fileName_accountGameData = "AccountGameData.json";
    public static string fileName_playingGameData = "PlayingGameData.json";
    static List<SkillController> tempListSkill = new List<SkillController>();

    public static List<AccountGameData> accountGameDataList { get=>FileHandler.ReadListFromJSON<AccountGameData>(fileName_accountGameData); private set { }}
    public static List<PlayingGameData> playingGameDataList { get => FileHandler.ReadListFromJSON<PlayingGameData>(fileName_playingGameData); private set { } }
    public static List<UserInfo> userInfoList { get => FileHandler.ReadListFromJSON<UserInfo>(fileName_userInfo); private set { } }
    public static List<AccountInfor> accountInfoList { get => FileHandler.ReadListFromJSON<AccountInfor>(fileName_accountInfo); private set { } }
    [Serializable]
    public class AccountInfor
    {
        public string username;
        public string password;
        public string email;
        public int age;

        public AccountInfor(string username, string password, string email)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.age = 0;
        }
    }

    [Serializable]
    public class CharacterInfo
    {
        public RuntimeAnimatorController animatorController;
        public GameObject characterPrefab;
        public string name;
    }
    [Serializable]
    public class AccountGameData
    {
        public string username;
        public int gemAmount, enemyKilledAmount, bossKilledAmount, gameCompletedAmount;
        public int selectingCharacterInfoID;
        public List<int> skillGainedIdList;
        public bool hasPlayingData;
        public AccountGameData(string username, List<int> skillGainedList, int gemAmount = 0, int enemyKilledAmount = 0, int bossKilledAmount = 0, int gameCompletedAmount = 0, int selectingCharacterInfo = 0, bool hasPlayingData = false)
        {
            this.username = username;
            this.gemAmount = gemAmount;
            this.enemyKilledAmount = enemyKilledAmount;
            this.bossKilledAmount = bossKilledAmount;
            this.gameCompletedAmount = gameCompletedAmount;
            this.skillGainedIdList = skillGainedList;
            this.selectingCharacterInfoID = selectingCharacterInfo;
            this.hasPlayingData = hasPlayingData;
        }
    }

    [Serializable]
    public class PlayingGameData
    {
        public string username;
        public int characterID;
        public int coinAmount, gemCollectedAmount, currentHP, sceneIndex;
        public List<int> skillCollectedList;
        public PlayingGameData(string username, List<int> skillCollectedList, int coinAmount = 0, int gemCollectedAmount = 0, int currentHPRate = 1, int sceneIndex = 0, int character = 0)
        {
            this.username = username;
            this.coinAmount = coinAmount;
            this.gemCollectedAmount = gemCollectedAmount;
            this.currentHP = currentHPRate;
            this.sceneIndex = sceneIndex;
            this.skillCollectedList = skillCollectedList;
            this.characterID = character;
        }
    }

    [Serializable]
    public class UserInfo
    {
        public string username;
        public int avtSpriteId;
        public string name;
        public int age;
        public UserInfo(string username, int avt = 0, string name = "", int age = 0)
        {
            this.username = username;
            this.avtSpriteId = avt;
            this.name = name;
            this.age = age;
        }
    }

    public static void SetCurrentUsername(string s)
    {
        currentUsername = s;
    }

    public static bool PurchaseGem(int amount)
    {
        AccountGameData currentAccountGameData = accountGameDataList.Single(s => s.username == currentUsername);
        if (currentAccountGameData.gemAmount >= amount)
        {
            List<AccountGameData> tmpList = accountGameDataList;
            tmpList.Single(s => s.username == currentUsername).gemAmount -= amount;
            FileHandler.SaveToJSON(tmpList, fileName_accountGameData);
            return true;
        }
        return false;
    }

    public static void UnlockSkill(SkillController skillControllerPrefabToAdd)
    {
        AccountGameData currentAccountGameData = accountGameDataList.Single(s => s.username == currentUsername);
        List<AccountGameData> tmpList = accountGameDataList;
        tmpList.Single(s => s.username == currentUsername).skillGainedIdList.Add(RuntimeData.Ins.skillControllerList.IndexOf(skillControllerPrefabToAdd));
        FileHandler.SaveToJSON(tmpList, fileName_accountGameData);
    }
}

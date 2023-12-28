using System;
using System.Collections.Generic;
using UnityEngine;

public static class AccountManager
{
    public static string currentUsername { get; private set; }
    public static string fileName_accountFile = "AccountFile.json";
    public static string fileName_userInfo = "UserInfo.json";
    public static string fileName_accountGameData = "AccountGameData.json";
    public static string fileName_playingGameData = "PlayingGameData.json";
    static List<SkillController> tempListSkill = new List<SkillController>();
    [Serializable]
    public class AccountInfor
    {
        public string username;
        public string password;
        public string email;
        public string avtId;
        public int age;

        public AccountInfor(string username, string password, string email)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.avtId = "";
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

    public static void SetCurrentUsername(string s)
    {
        currentUsername = s;
    }

    [Serializable]
    public class AccountGameData
    {
        public string username;
        public int gemAmount, enemyKilledAmount, bossKilledAmount, gameCompletedAmount;
        public CharacterInfo selectingCharacterInfo;
        public List<SkillController> skillGainedList;
        public bool hasPlayingData;
        public AccountGameData(string username, List<SkillController> skillGainedList, int gemAmount = 0, int enemyKilledAmount= 0, int bossKilledAmount = 0, int gameCompletedAmount = 0, CharacterInfo selectingCharacterAnimator = null, bool hasPlayingData = false)
        {
            this.username = username;
            this.gemAmount = gemAmount;
            this.enemyKilledAmount = enemyKilledAmount;
            this.bossKilledAmount = bossKilledAmount;
            this.gameCompletedAmount = gameCompletedAmount;
            this.skillGainedList = skillGainedList;
            this.selectingCharacterInfo = selectingCharacterAnimator;
            this.hasPlayingData = hasPlayingData;
        }
    }

    [Serializable]
    public class PlayingGameData
    {
        public string username;
        public CharacterInfo character;
        public int coinAmount, gemCollectedAmount, currentHP, sceneIndex;
        public List<SkillController> skillCollectedList;
        public PlayingGameData(string username, List<SkillController> skillCollectedList, int coinAmount = 0, int gemCollectedAmount = 0, int currentHPRate = 1, int sceneIndex = 0, CharacterInfo character = null)
        {
            this.username = username;
            this.coinAmount = coinAmount;
            this.gemCollectedAmount = gemCollectedAmount;
            this.currentHP = currentHPRate;
            this.sceneIndex = sceneIndex;
            this.skillCollectedList = skillCollectedList;
            this.character = character;
        }
    }

    [Serializable]
    public class UserInfo
    {
        public string username;
        public Sprite avtSprite;
        public string name;
        public int age;
        public UserInfo(string username, Sprite avt = null, string name = "", int age = 0)
        {
            this.username = username;
            this.avtSprite = avt;
            this.name = name;
            this.age = age;
        }
    }
}

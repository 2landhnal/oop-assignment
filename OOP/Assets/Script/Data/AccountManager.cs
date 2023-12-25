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

    public static void SetCurrentUsername(string s)
    {
        currentUsername = s;
    }

    [Serializable]
    public class AccountGameData
    {
        public string username;
        public List<SkillController> skillControllerList;

        public AccountGameData(string username, List<SkillController> skillControllerList)
        {
            this.username = username;
            this.skillControllerList = skillControllerList;
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

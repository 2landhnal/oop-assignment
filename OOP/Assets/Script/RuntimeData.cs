using System;
using System.Collections;
using System.Collections.Generic;
using CharacterInfo = AccountManager.CharacterInfo;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class RuntimeData : Singleton<RuntimeData>
{
    protected override void Awake()
    {
        base.Awake();
    }
    public List<Sprite> avtSprites;
    public List<SkillController> skillControllerList;
    public List<CharacterInfo> characterInfoList;
    public List<string> sceneNameList;
}

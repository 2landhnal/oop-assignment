using System.Collections.Generic;
using CharacterInfo = AccountManager.CharacterInfo;
using UnityEngine;

public class RuntimeData : Singleton<RuntimeData>
{
    protected override void Awake()
    {
        MakeSingleton(true);
    }
    public List<Sprite> avtSprites;
    public List<SkillController> skillControllerList;
    public List<CharacterInfo> characterInfoList;
    public List<string> sceneNameList;
}

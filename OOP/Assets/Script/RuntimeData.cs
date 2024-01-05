using System.Collections.Generic;
using CharacterInfo = AccountManager.CharacterInfo;
using UnityEngine;
using System.Linq;

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

    public SkillController GetSkillControllerPrefabByType(SkillType type)
    {
        var findeds = skillControllerList.Where(s => s.type == type).ToArray();
        if (findeds == null || findeds.Length == 0) return null;
        return findeds[0];
    }
}

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
    public List<TraderItemData> traderItemList;

    public SkillController GetSkillControllerPrefabByType(SkillType type)
    {
        var findeds = skillControllerList.Where(s => s.type == type).ToArray();
        if (findeds == null || findeds.Length == 0) return null;
        return findeds[0];
    }
    public List<int> GetSkillControllerIdCanCollected()
    {
        List<int> findeds = new List<int>();
        for(int i=0; i<skillControllerList.Count; i++)
        {
            if (AccountManager.GetCurrentAccountGameData().skillGainedIdList.Contains(i) || skillControllerList[i].openAtStart)
            {
                findeds.Add(i);
            }
        }
        if (findeds == null || findeds.Count == 0) return null;
        return findeds;
    }
    public List<int> GetListSkillControllerIdCanCollectedButNotCollectedYet()
    {
        List<int> collectedList = AccountManager.GetCurrentPlayingGameData().skillCollectedList;
        List<int> findeds = new List<int>();
        foreach (int i in GetSkillControllerIdCanCollected())
        {
            if (collectedList.Contains(i)) continue;
            findeds.Add(i);
        }
        if (findeds == null || findeds.Count == 0) return null;
        return findeds;
    }
    public int GetRandomSkillControllerIdCanCollectedButNotCollectedYet()
    {
        List<int> findeds = GetListSkillControllerIdCanCollectedButNotCollectedYet();
        return findeds[Random.Range(0, findeds.Count)];
    }

    public List<SkillController> GetSkillControllerPrefabById(List<int> idList)
    {
        List<SkillController> converteds = new List<SkillController> ();
        foreach (int i in idList)
        {
            if (skillControllerList.Count <= i || i < 0) continue;
            converteds.Add(skillControllerList[i]);
        }
        return converteds;
    }
}

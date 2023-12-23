using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButtonDrawer : Singleton<SkillButtonDrawer>
{
    public Transform contentGrid;
    public SkillButton skillBtnPrefab;
    private List<SkillType> skillCollecteds;
    public SkillManager skillManager;
    protected override void Awake()
    {
        MakeSingleton(false);
    }

    public void DrawButtons()
    {
        Helper.ClearChilds(contentGrid);
        skillCollecteds = skillManager.skillCollected;
        if (skillCollecteds == null || skillCollecteds.Count == 0) return;
        foreach(var skill in skillCollecteds)
        {
            //Debug.Log(skill.Key + " " + skill.Value + " " + SkillManager.Ins.HasSkill(skill.Key) + " " + (SkillManager.Ins.GetSkillController(skill.Key) == null ? "Null" : SkillManager.Ins.GetSkillController(skill.Key)));
            if (!skillManager.HasSkill(skill)) return;
            var skillButtonClone = Instantiate(skillBtnPrefab);
            Helper.AssignToRoot(contentGrid, skillButtonClone.transform, Vector3.zero, Vector3.one);
            skillButtonClone.Initialize(skill);
        }
    }
}

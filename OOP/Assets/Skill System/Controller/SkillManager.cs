using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<SkillController> skillControllersCollected;
    public List<SkillType> skillCollected;
    SkillController tempSkillController;
    public Creature creature { private set; get; }

    private void Start()
    {
        skillControllersCollected = new List<SkillController>();
        foreach (SkillType type in skillCollected)
        {
            tempSkillController = GetSkillControllerPrefabByType(type);
            if (tempSkillController != null)
            {
                ClonePrefabToAssignRoot(tempSkillController);
            }
            else
            {
                Debug.Log($"Skill with type: {type.ToString()} doesn't exist!");
                continue;
            }
        }
        creature = GetComponent<Creature>();
        Initialize();
    }

    SkillController ClonePrefabToAssignRoot(SkillController skillControllerPrefab)
    {
        tempSkillController = Instantiate(skillControllerPrefab);
        tempSkillController.skillManager = this;
        Helper.AssignToRoot(transform, tempSkillController.transform, Vector3.zero, Vector3.one);
        skillControllersCollected.Add(tempSkillController);
        return tempSkillController;
    }

    private void Initialize()
    {
        for (int i = 0; i < skillControllersCollected.Count; i++)
        {
            var skillController = skillControllersCollected[i];
            if (skillController == null) continue;
            skillController.LoadData();
        }
    }

    public SkillController GetSkillControllerPrefabByType(SkillType type)
    {
        var findeds = CombatProps.Ins.skillControllerList.Where(s=>s.type == type).ToArray();
        if(findeds == null || findeds.Length == 0 ) return null;
        return findeds[0];
    }

    public SkillController GetSkillControllerCloneByType(SkillType type)
    {
        var findeds = skillControllersCollected.Where(s => s.type == type).ToArray();
        if (findeds == null || findeds.Length == 0) return null;
        return findeds[0];
    }

    public bool IsSkillExist(SkillType type)
    {
        var findeds = skillControllersCollected.Where(s => s.type == type).ToArray();
        return !(findeds == null || findeds.Length == 0);
    }

    public bool IsSkillCollected(SkillType type)
    {
        return skillCollected.Contains(type);
    }

    public void AddSkill(SkillType type)
    {
        if (skillCollected.Contains(type)) return;
        tempSkillController = GetSkillControllerPrefabByType(type);
        if (tempSkillController == null) return;
        tempSkillController = ClonePrefabToAssignRoot(tempSkillController);
        tempSkillController.LoadData();
        skillCollected.Add(type);
        SkillButtonDrawer.Ins?.DrawButtons();
    }

    public void RemoveSkill(SkillType type)
    {
        if (!skillCollected.Contains(type)) return;
        skillCollected.Remove(type);
    }

    public void StopSkill(SkillType type)
    {
        var skillController = GetSkillControllerPrefabByType(type);
        if(skillController == null) return;
        skillController.Stop();
    }

    public void StopAllSkill()
    {
        if(skillControllersCollected.Count == 0 || skillControllersCollected == null) return;
        for (int i = 0; i < skillControllersCollected.Count; i++)
        {
            var skillController = skillControllersCollected[i];
            if (skillController == null) continue;
            skillController.ForceStop();
        }
    }
}

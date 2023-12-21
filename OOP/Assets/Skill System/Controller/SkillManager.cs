using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]private List<SkillController> skillControllersCanCollectPrefab;
    public List<SkillController> skillControllersCanCollect;
    public List<SkillType> skillCollected;
    SkillController tempSkillController;
    public Creature creature { private set; get; }

    private void Awake()
    {
        foreach(SkillController skillController in skillControllersCanCollectPrefab)
        {
            tempSkillController = Instantiate(skillController);
            tempSkillController.skillManager = this;
            Helper.AssignToRoot(transform, tempSkillController.transform, Vector3.zero, Vector3.one);
            skillControllersCanCollect.Add(tempSkillController);
        }
        creature = GetComponent<Creature>();
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < skillControllersCanCollect.Count; i++)
        {
            var skillController = skillControllersCanCollect[i];
            if (skillController == null) continue;
            skillController.LoadData();
            //skillController.OnStopWithType.AddListener(RemoveSkill);
        }
    }

    public SkillController GetSkillControllerCanCollect(SkillType type)
    {
        var findeds = skillControllersCanCollect.Where(s=>s.type == type).ToArray();
        if(findeds == null || findeds.Length == 0 ) return null;
        return findeds[0];
    }

    public bool IsSkillCollected(SkillType type)
    {
        return skillCollected.Contains(type);
    }

    public bool HasSkill(SkillType type)
    {
        return skillCollected.Contains(type) && GetSkillControllerCanCollect(type) != null;
    }

    public void AddSkill(SkillType type)
    {
        if(GetSkillControllerCanCollect(type) == null || GetSkillControllerCanCollect(type) == null) return;
        if (!skillCollected.Contains(type)) skillCollected.Add(type);
    }

    public void RemoveSkill(SkillType type)
    {
        if (!skillCollected.Contains(type)) return;
        skillCollected.Remove(type);
    }

    public void StopSkill(SkillType type)
    {
        var skillController = GetSkillControllerCanCollect(type);
        if(skillController == null) return;
        skillController.Stop();
    }

    public void StopAllSkill()
    {
        if(skillControllersCanCollect.Count == 0 || skillControllersCanCollect == null) return;
        for (int i = 0; i < skillControllersCanCollect.Count; i++)
        {
            var skillController = skillControllersCanCollect[i];
            if (skillController == null) continue;
            skillController.ForceStop();
        }
    }
}

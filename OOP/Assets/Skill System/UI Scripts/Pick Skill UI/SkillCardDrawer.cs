using UnityEngine;

public class SkillCardDrawer : Singleton<SkillCardDrawer>
{
    public Transform treasureCardPickPanel;
    public Transform contentGrid;
    public SkillCard skillCardPrefab;
    private Treasure treasure;
    protected override void Awake()
    {
        MakeSingleton(false);
    }

    public void DrawCard(Treasure t)
    {
        treasureCardPickPanel.gameObject.SetActive(true);
        Helper.ClearChilds(contentGrid);
        treasure = t;
        if (treasure.ControllerList == null || treasure.ControllerList.Count == 0) return;
        foreach (SkillController skill in treasure.ControllerList)
        {
            var skillCardClone = Instantiate(skillCardPrefab);
            Helper.AssignToRoot(contentGrid, skillCardClone.transform, Vector3.zero, Vector3.one);
            skillCardClone.Initialize(skill);
            skillCardClone.BtnComponent.onClick.AddListener(CardPicked);
        }
    }

    void CardPicked()
    {
        treasure.Picked();
        treasureCardPickPanel.gameObject.SetActive(false);
    }
}

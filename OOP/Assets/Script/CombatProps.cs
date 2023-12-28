using System.Collections.Generic;
using UnityEngine;

public class CombatProps : Singleton<CombatProps>
{
    public LayerMask playerLayer, enemyLayer, playerAtkLayer, enemyAtkLayer;
    public float hurtForce, hurtTime, hurtPart;
    public SpriteRenderer bodyPrefab, afterImagePrefab;
    public List<SkillController> skillControllerList;
    private SkillButtonDrawer skillBtnDrawers;

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    private void Start()
    {
        skillBtnDrawers = SkillButtonDrawer.Ins;
        if (skillBtnDrawers == null) return;
        skillBtnDrawers.DrawButtons();
    }
}

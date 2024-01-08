using System.Collections.Generic;
using UnityEngine;

public class CombatProps : Singleton<CombatProps>, IObserve
{
    public LayerMask playerLayer, enemyLayer, playerAtkLayer, enemyAtkLayer;
    public float hurtForce, hurtTime, hurtPart;
    public SpriteRenderer bodyPrefab, afterImagePrefab;
    private SkillButtonDrawer skillBtnDrawers;
    public Currency coinPrefab, gemPrefab;
    public GameObject bloodEffect;

    protected override void Awake()
    {
        MakeSingleton(false);
    }
    public void SubjectCalled()
    {
        Debug.Log("called");
        skillBtnDrawers = SkillButtonDrawer.Ins;
        if (skillBtnDrawers == null) return;
        skillBtnDrawers.DrawButtons();
    }

}

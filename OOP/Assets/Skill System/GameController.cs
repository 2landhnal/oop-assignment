using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private SkillButtonDrawer skillBtnDrawers;
    void Start()
    {
        skillBtnDrawers = SkillButtonDrawer.Ins;
        if (skillBtnDrawers == null) return;
        skillBtnDrawers.DrawButtons();
    }

}

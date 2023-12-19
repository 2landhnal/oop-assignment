using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private SkillButtonDrawer skillBtnDrawers;
    void Start()
    {
        if (skillBtnDrawers == null) return;
        skillBtnDrawers.DrawButtons();
    }

}

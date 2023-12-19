using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SkillData/Standard")]

public class SkillData : ScriptableObject
{
    public Sprite skillIcon;
    public float damage;
    public float coolDownTime;
    public float triggerTime;
}

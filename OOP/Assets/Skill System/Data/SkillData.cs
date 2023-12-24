using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SkillData/Standard")]

public class SkillData : ScriptableObject
{
    public Sprite skillIcon, backGroundCard;
    public float damage;
    public float coolDownTime;
    public float triggerTime;
    public string skillName, description;
    public int price;
}

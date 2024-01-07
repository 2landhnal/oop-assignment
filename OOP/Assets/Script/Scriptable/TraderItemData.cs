using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TraderItem")]
public class TraderItemData : ScriptableObject
{
    public Vector2 price;
    public EnviromentProps.Item type;
    public Sprite sprite;
    public float hpAmount;
}

using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;

public class MessagePanel : MonoBehaviour
{
    public TextMeshProUGUI messageTxt;
    public void ShowMessage(string mess)
    {
        gameObject.SetActive(true);
        messageTxt.text = mess;
    }
}

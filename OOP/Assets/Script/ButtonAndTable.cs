using UnityEngine;

public class ButtonAndTable : MonoBehaviour
{
    [SerializeField] GameObject[] targetOff, targetOn;
    public void OnClick()
    {
        AudioManager.Ins.PlaySFX(1);
        foreach (GameObject go in targetOff)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in targetOn)
        {
            go.SetActive(true);
        }
    }
}

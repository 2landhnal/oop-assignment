using UnityEngine;

public class ButtonAndTable : MonoBehaviour
{
    [SerializeField] GameObject[] targetOff, targetOn;
    public void OnClick()
    {
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

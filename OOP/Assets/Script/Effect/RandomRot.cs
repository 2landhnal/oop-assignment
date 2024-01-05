using UnityEngine;

public class RandomRot : MonoBehaviour
{
    private void OnEnable()
    {
        transform.Rot(Random.Range(0, 360));
    }
}

using UnityEngine;

public class EnviromentProps : MonoBehaviour
{
    public static EnviromentProps Instance { get; private set; }
    public LayerMask groundLayers, slideableLayers;
    public float jumpOutWallSpeed;

    private void Awake()
    {
        Instance = this;
    }
}

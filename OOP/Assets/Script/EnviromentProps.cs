using UnityEngine;

public class EnviromentProps : Singleton<EnviromentProps> 
{
    public LayerMask groundLayers, platformLayers;
    public float jumpOutWallSpeed;
    public Transform spawnPoint;

    protected override void Awake()
    {
        MakeSingleton(false);
    }
    private void Start()
    {
        Player.instance.transform.position = spawnPoint.position;
    }
}

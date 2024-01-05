using UnityEngine;

public class EnviromentProps : Singleton<EnviromentProps>, IObserve
{
    public LayerMask groundLayers, platformLayers;
    public float jumpOutWallSpeed;
    public Transform spawnPoint;

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    public void SubjectCalled()
    {
        if (Player.instance == null) return;
        if (spawnPoint == null) return;
        Player.instance.transform.position = spawnPoint.position;
    }
}

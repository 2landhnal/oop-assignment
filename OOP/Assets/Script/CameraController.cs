using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>, IObserve
{
    protected override void Awake()
    {
        MakeSingleton(false);
    }
    private Transform player;
    [SerializeField]float speed;
    Vector3 tmpv3;
    // Start is called before the first frame update

    public void SubjectCalled()
    {
        player = Player.instance.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        tmpv3 = Vector2.Lerp(transform.position, player.position, speed);
        tmpv3.z = transform.position.z;
        tmpv3.y = tmpv3.y + 2;
        transform.position = tmpv3;
    }
}

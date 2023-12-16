using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    [SerializeField]float speed;
    Vector3 tmpv3;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        tmpv3 = Vector2.Lerp(transform.position, player.position, speed);
        tmpv3.z = transform.position.z;
        tmpv3.y = tmpv3.y + 2;
        transform.position = tmpv3;
    }
}

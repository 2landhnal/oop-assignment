using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startpos;
    public float parallex_effect;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float dist = (CameraController.Ins.gameObject.transform.position.x * parallex_effect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSkill : MonoBehaviour
{
    public LayerMask groundLayer;
    public float maxRayLength = 3;
    Vector2 tmp;
    private void Start()
    {
        var hit = Physics2D.Raycast(transform.position, Vector3.down, maxRayLength, groundLayer.value);
        tmp = hit.point;
        transform.position = tmp;
    }
}

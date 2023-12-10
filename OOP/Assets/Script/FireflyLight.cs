using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireflyLight : MonoBehaviour
{
    [SerializeField]Light2D light;
    float targetOuterRadius;
    [SerializeField] float towardSpeed;
    [SerializeField] Vector2 outerRadiusRange;

    // Update is called once per frame
    void Update()
    {
        light.pointLightOuterRadius = Mathf.MoveTowards(light.pointLightOuterRadius, targetOuterRadius, towardSpeed);
        if(Mathf.Abs(light.pointLightOuterRadius - targetOuterRadius) < 0.01f)
        {
            targetOuterRadius = Random.Range(outerRadiusRange.x, outerRadiusRange.y);
        }
    }
}

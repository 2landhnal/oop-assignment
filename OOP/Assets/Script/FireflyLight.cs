using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireflyLight : MonoBehaviour
{
    [SerializeField]Light2D light;
    float targetOuterRadius;
    [SerializeField] float cycleTime;
    [SerializeField] Vector2 outerRadiusRange;

    private void Start()
    {
        targetOuterRadius = Random.Range(outerRadiusRange.x, outerRadiusRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        light.pointLightOuterRadius = Mathf.Lerp(light.pointLightOuterRadius, targetOuterRadius, Time.deltaTime/cycleTime);
        if(Mathf.Abs(light.pointLightOuterRadius - targetOuterRadius) < 0.01f)
        {
            targetOuterRadius = Random.Range(outerRadiusRange.x, outerRadiusRange.y);
        }
    }
}

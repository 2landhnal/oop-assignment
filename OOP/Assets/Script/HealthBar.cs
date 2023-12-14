using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlide;
    public HealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthSlide.value = Mathf.Lerp(healthSlide.value, healthManager.currentHP / healthManager.GetMaxHP(), .1f); 
    }
}

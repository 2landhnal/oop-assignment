using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IObserve
{
    public Slider healthSlide;
    HealthManager healthManager;
    // Start is called before the first frame update

    public void SubjectCalled()
    {
        healthManager = Player.instance.GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthManager == null) return;
        healthSlide.value = Mathf.Lerp(healthSlide.value, healthManager.currentHP / healthManager.GetMaxHP(), .1f); 
    }
}

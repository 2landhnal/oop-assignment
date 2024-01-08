using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IObserve
{
    public Slider healthSlide;
    HealthManager healthManager;
    bool isBoss;
    // Start is called before the first frame update

    public void SetObject(GameObject obj)
    {
        healthManager = obj.GetComponent<HealthManager>();
    }

    public void SubjectCalled()
    {
        if(isBoss)
        {
            return;
        }
        healthManager = Player.instance.GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthManager == null) return;
        healthSlide.value = Mathf.Lerp(healthSlide.value, healthManager.currentHP / healthManager.GetMaxHP(), .1f); 
    }
}

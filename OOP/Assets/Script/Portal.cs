using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private bool counting;
    [SerializeField]private float delayTime;
    private float delayCounter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            counting = true;
            delayCounter = delayTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (counting)
        {
            delayCounter -= Time.deltaTime;
            if(delayCounter <= 0)
            {
                SceneManager.LoadScene(sceneName);
                counting = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            counting = false;
        }
    }
}

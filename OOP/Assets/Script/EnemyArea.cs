using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArea : MonoBehaviour
{
    List<Transform> childs;
    private void Start()
    {
        childs = new List<Transform>();
        foreach (Transform t in transform)
        {
            if (t == null) continue;
            t.gameObject.SetActive(false);
            childs.Add(t);
            GameController.Ins.AddEnemyCounter();
            t.GetComponent<Creature>().OnDeath.AddListener(GameController.Ins.AddEnemyDefeatCounter);
            t.GetComponent<Creature>().Active();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            foreach(Transform t in childs)
            {
                t.gameObject.SetActive(true);
                t.parent = null;
            }
            
            Destroy(gameObject);
        }
    }
}

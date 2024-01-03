using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody2D itemRb;
    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random();
        itemRb = GetComponent<Rigidbody2D>();
        itemRb.AddForce(new Vector2(-2 + rnd.Next(5), 5), ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            Destroy(gameObject);
        }
    }
}

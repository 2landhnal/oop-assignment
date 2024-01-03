using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody2D itemRb;
    public float ForceDrop;
    // Start is called before the first frame update
    void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();
        itemRb.AddForce(Vector2.up * ForceDrop, ForceMode2D.Impulse);
    }
}

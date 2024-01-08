using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce;
    Rigidbody2D tmpRb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        tmpRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (tmpRb == null) return;
        if (tmpRb.transform.position.y < GetComponent<Collider2D>().offset.y + transform.position.y) return;
        tmpRb.velocity = new Vector2(tmpRb.velocity.x, jumpForce);
    }
}

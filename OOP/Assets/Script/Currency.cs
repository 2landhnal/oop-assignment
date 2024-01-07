using System;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [Serializable]
    public enum ResourceType
    {
        Coin, Gem
    }
    public Vector2 amountRange;
    public ResourceType type;
    private Rigidbody2D itemRb;
    public GameObject fx;
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
            if(type == ResourceType.Coin)
            {
                Player.instance.GetComponent<ResourceManager>().AddCoin((int)UnityEngine.Random.Range(amountRange.x, amountRange.y));
            }
            else if (type == ResourceType.Gem)
            {
                Player.instance.GetComponent<ResourceManager>().AddGem((int)UnityEngine.Random.Range(amountRange.x, amountRange.y));
            }
            Instantiate(fx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

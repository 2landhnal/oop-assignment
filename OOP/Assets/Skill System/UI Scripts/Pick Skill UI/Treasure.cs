using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField]private List<SkillController> controllerList;
    public bool claimed;

    public List<SkillController> ControllerList { get => controllerList; }

    private void Start()
    {
        claimed = false;
    }

    public void OnClick()
    {
        Debug.Log("clicked");
        SkillCardDrawer.Ins.DrawCard(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!claimed)
            {
                Debug.Log("add");
                Player.instance.EnterEvent.RemoveAllListeners();
                Player.instance.EnterEvent.AddListener(OnClick);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!claimed)
            {
                Debug.Log("remove");
                Player.instance.EnterEvent.RemoveListener(OnClick);
            }
        }
    }

    public void Picked()
    {
        Player.instance.EnterEvent.RemoveAllListeners();
        claimed = true;
    }
}

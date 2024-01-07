using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private List<SkillController> controllerList;
    public bool claimed;
    public GameObject triangle;
    public List<SkillController> ControllerList { get => controllerList; }

    private void Start()
    {
        claimed = false;
        List<int> idList = RuntimeData.Ins.GetListSkillControllerIdCanCollectedButNotCollectedYet();
        if(idList.Count <= 3 )
        {
            controllerList = RuntimeData.Ins.GetSkillControllerPrefabById(idList);
        }
        else
        {
            controllerList = RuntimeData.Ins.GetSkillControllerPrefabById(GetRandomNSkillIdInList(3, idList));
        }
    }

    List<int> GetRandomNSkillIdInList(int n, List<int> list)
    {
        List<int> result = new List<int>();
        for(int i=0; i<n; i++)
        {
            int tmp = Random.Range(0, list.Count);
            result.Add(list[tmp]);
            list.RemoveAt(tmp);
        }
        return result;
    }

    public void OnClick()
    {
        Debug.Log("clicked");
        Player.instance.DisableControlAndIdle();
        SkillCardDrawer.Ins.DrawCard(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!claimed)
            {
                Debug.Log("add");
                triangle.SetActive(true);
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
                triangle.SetActive(false);
                Player.instance.EnterEvent.RemoveListener(OnClick);
            }
        }
    }

    public void Picked()
    {
        Player.instance.EnableControl();
        Player.instance.EnterEvent.RemoveAllListeners();
        claimed = true;
        triangle.SetActive(false);
    }
}

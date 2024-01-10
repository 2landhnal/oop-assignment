using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public GameObject triangle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("add");
            Player.instance.SpaceEvent.RemoveAllListeners();
            Player.instance.SpaceEvent.AddListener(OnClick);
            triangle.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("remove");
            triangle.SetActive(false);
            Player.instance.SpaceEvent.RemoveListener(OnClick);
        }
    }

    public void OnClick()
    {
        if (RuntimeData.Ins.sceneNameList[^1] == SceneManager.GetActiveScene().name)
        {
            GameController.Ins.Win();
            return;
        }
        Debug.Log("clicked");
        SceneManager.LoadSceneAsync(RuntimeData.Ins.sceneNameList[AccountManager.GetCurrentPlayingGameData().sceneIndex + 1]);
    }
}

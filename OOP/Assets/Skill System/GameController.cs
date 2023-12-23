using UnityEngine;

public class GameController : Singleton<GameController>
{
    private SkillButtonDrawer skillBtnDrawers;
    private int enemyCounter, enemyDefeatCounter;
    [SerializeField]GameObject portal;
    public void AddEnemyCounter()
    {
        enemyCounter++;
    }
    public void AddEnemyDefeatCounter()
    {
        enemyDefeatCounter++;
        if(enemyDefeatCounter == enemyCounter)
        {
            AllDefeated();
        }
    }
    protected override void Awake()
    {
        MakeSingleton(false);
    }
    void Start()
    {
        skillBtnDrawers = SkillButtonDrawer.Ins;
        if (skillBtnDrawers == null) return;
        skillBtnDrawers.DrawButtons();
        enemyCounter = 0;
        enemyDefeatCounter = 0;
        portal.SetActive(false) ;
    }

    void AllDefeated()
    {
        if (portal == null) return;
        portal.SetActive(true);
    }

}

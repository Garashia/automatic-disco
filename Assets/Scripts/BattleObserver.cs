using UnityEngine;
using static BattleManager;

public class BattleObserver
{
    static private BattleObserver m_battleObserver = null;
    static public BattleObserver Observer
    {
        get
        {
            m_battleObserver ??= new BattleObserver();
            return m_battleObserver;
        }
        set { m_battleObserver = value; }
    }

    private BattleManager battleManager = null;
    public BattleManager Manager
    {
        // get { return battleManager; }
        set { battleManager = value; }
    }

    private bool IsManage()
    {
        return battleManager != null;
    }

    public void SetMessage1(
        string message,
        Vector2 position,
        string word = "")
    {
        if (!IsManage()) return;
        battleManager.SetMessage1(message, position, word);
    }
    public void SetMessage2(
    string message,
    Vector2 position,
    string word = "")
    {
        if (!IsManage()) return;
        battleManager.SetMessage2(message, position, word);
    }
    public void SetMessage3(
        string message,
        Vector2 position,
        string word = "")
    {
        if (!IsManage()) return;
        battleManager.SetMessage3(message, position, word);
    }

    public GameObject SetText(
        string message,
        Vector2 position)
    {
        if (!IsManage()) return null;
        return battleManager.SetText(message, position);
    }

    public GameObject SetText(
    string message,
    Vector2 position,
    TextDelegate textDelegate)
    {
        if (!IsManage()) return null;
        return battleManager.SetText(message, position, textDelegate);
    }

    public void AEOCommander(string message)
    {
        if (!IsManage()) return;
        battleManager.ReceiveMessages(message);
    }

    public Vector2? SichiyouSetting(Vector2 position, string word, MovingSichiyou sichiyou)
    {
        if (!IsManage()) return null;
        return battleManager.SichiyouSetting(position, word, sichiyou);
    }

}

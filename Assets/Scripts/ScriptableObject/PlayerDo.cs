using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerList", menuName = "ScriptableObjects/Data/List", order = 2)]

public class PlayerDo : ScriptableObject
{
    [SerializeField]
    private Players m_player1 = null;
    public Players Player1
    {
        set { m_player1 = value; }
        get { return m_player1; }
    }

    [SerializeField]
    private Players m_player2 = null;
    public Players Player2
    {
        set { m_player2 = value; }
        get { return m_player2; }
    }
    [SerializeField]
    private Players m_player3 = null;
    public Players Player3
    {
        set { m_player3 = value; }
        get { return m_player3; }
    }
    [SerializeField]
    private Players m_player4 = null;
    public Players Player4
    {
        set { m_player4 = value; }
        get { return m_player4; }
    }

    public List<Players> PlayersList()
    {
        List<Players> players = new List<Players>();

        players.Add(m_player1);
        players.Add(m_player2);
        players.Add(m_player3);
        players.Add(m_player4);
        return players;
    }

}

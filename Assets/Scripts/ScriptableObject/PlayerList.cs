using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerList", menuName = "ScriptableObjects/PlayerList", order = 2)]
public class PlayerList : ScriptableObject
{
    [SerializeField]
    private List<PlayerParameter> m_playerList;
}

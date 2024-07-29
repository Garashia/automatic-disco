using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyParameterList
{
    [SerializeField]
    private EnemyParameter enemy;
    public EnemyParameter Enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }

    [SerializeField]
    private float weight;
    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }

}

[CreateAssetMenu(fileName = "EnemyList", menuName = "ScriptableObjects/EnemyList", order = 3)]
public class EnemyList : ScriptableObject
{
    [SerializeField]
    private List<EnemyParameterList> m_enemyList;
    public List<EnemyParameterList> CharaList
    {
        get { return m_enemyList; }
        set { m_enemyList = value; }
    }

    private float? m_totalWeight = null;
    public float TotalWeight
    {
        get
        {
            if (m_totalWeight == null)
            {
                m_totalWeight = new float();
                m_totalWeight = 0.0f;
                foreach (EnemyParameterList hp in CharaList)
                {
                    m_totalWeight += hp.Weight;
                }
            }
            return (float)m_totalWeight;
        }
        set { m_totalWeight = value; }
    }

}

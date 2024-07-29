using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Data/Player", order = 1)]
public class Players : ScriptableObject
{
    [SerializeField]
    private PlayerParameter m_playerParameter;
    public PlayerParameter PlayerP
    {
        get { return m_playerParameter; }
    }

    [SerializeField, ReadOnly(true)]
    private uint m_hp;
    public uint HP
    {
        get { return m_hp; }
        set { m_hp = value; }
    }
    [SerializeField, ReadOnly(true)]
    private uint m_power = 0;
    public uint Power
    {
        get { return m_power; }
        set { m_power = value; }
    }
    [SerializeField, ReadOnly(true)]
    private uint m_magic;
    public uint Magic
    {
        set { m_magic = value; }
        get { return m_magic; }
    }
    [SerializeField, ReadOnly(true)]
    private uint m_guard;
    public uint Guard
    {
        get { return m_guard; }
        set { m_guard = value; }
    }
    [SerializeField, ReadOnly(true)]
    private string m_name;
    public string Name
    {
        get { return m_name; }
    }

    Players(PlayerParameter playerParameter, string names)
    {
        m_playerParameter = playerParameter;
        m_hp = m_playerParameter.HP;
        m_power = m_playerParameter.Power;
        m_magic = m_playerParameter.Magic;
        m_guard = m_playerParameter.Guard;
        m_name = names;
    }

    private uint m_level = 0;
    private const uint m_maxLevel = 10;

    public uint Level
    {
        get { return m_level; }
    }

    private uint m_ex = 0;
    private uint m_maxEx = 100;
    public void ExperienceAcquisition(uint ex)
    {
        m_ex += ex;
        if (m_ex > m_maxEx)
        {
            m_maxEx += 100;
            ++m_level;
        }
    }

    public void Initialize()
    {
        if (m_playerParameter == null) return;
        m_hp = m_playerParameter.HP;
        m_power = m_playerParameter.Power;
        m_magic = m_playerParameter.Magic;
        m_guard = m_playerParameter.Guard;

    }
}

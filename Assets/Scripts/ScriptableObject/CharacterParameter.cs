using UnityEngine;

public class CharacterParameter : ScriptableObject
{
    [SerializeField]
    private uint m_hp;
    public uint HP
    {
        get { return m_hp; }
        set { m_hp = value; }
    }

    [SerializeField]
    private string m_name;
    public string Name
    {
        get { return m_name; }
    }

    [SerializeField]
    private uint m_power;
    public uint Power
    {
        get { return m_power; }
        set { m_power = value; }
    }

    [SerializeField]
    private uint m_magic;
    public uint Magic
    {
        get { return m_magic; }
        set { m_magic = value; }
    }

    [SerializeField]
    private uint m_guard;
    public uint Guard
    {
        get { return m_guard; }
        set { m_guard = value; }
    }

    [SerializeField]
    private Sprite m_sprite;
    public Sprite GetSprite
    {
        get { return m_sprite; }
    }

    [SerializeField]
    private string m_description;
    public string Description
    {
        get { return m_description; }
    }

}

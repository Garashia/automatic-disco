using UnityEngine;

[CreateAssetMenu(fileName = "SpriteManager", menuName = "ScriptableObjects/Sprite", order = 3)]
public class SpriteManager : ScriptableObject
{
    [SerializeField]
    private Sprite m_battleSprite;
    public Sprite BattleSprite
    {
        get { return m_battleSprite; }
    }
    [SerializeField]
    private Sprite m_escapeSprite;
    public Sprite EscapeSprite
    {
        get { return m_escapeSprite; }
    }

    [SerializeField]
    private Sprite m_sphereSprite;
    public Sprite SphereSprite
    {
        get { return m_sphereSprite; }
    }

    [SerializeField]
    private Sprite m_attackSprite;
    public Sprite AttackSprite
    {
        get { return m_attackSprite; }
    }

}

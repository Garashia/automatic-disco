using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player", order = 1)]
public class PlayerParameter : CharacterParameter
{
    // static private readonly float _value = 1.0f / 100;

    [Header("ê¨í∑ó¶(0Å`100%)")]
    [SerializeField, Range(10, 100)]
    private float m_ratePower;
    [SerializeField, Range(10, 100)]
    private float m_rateHp;
    [SerializeField, Range(10, 100)]
    private float m_rateMagic;
    [SerializeField, Range(10, 100)]
    private float m_rateGuard;

    [Header("ç≈ëÂê¨í∑íl(0Å`10)")]
    [SerializeField, Range(1, 10)]
    private int m_maxGrowHp;
    [SerializeField, Range(1, 10)]
    private int m_maxGrowPower;
    [SerializeField, Range(1, 10)]
    private int m_maxGrowMagic;
    [SerializeField, Range(1, 10)]
    private int m_maxGrowGuard;

    [Header("çUåÇéûÇÃÇ¶ÇÒÇµÇ„Ç¬")]
    [SerializeField]
    private GameObject m_attackEffect;
    [SerializeField]
    private GameObject m_magicEffect;


}

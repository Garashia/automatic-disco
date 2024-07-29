using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyParameter : CharacterParameter
{
    [SerializeField]
    private uint m_ex;
    public uint EX
    {
        get { return m_ex; }
    }

}

using UnityEngine;

[System.Serializable]
public struct Effects
{
    [SerializeField]
    private GameObject effect;
    public GameObject EffectPrefab
    {
        set { effect = value; }
        get { return effect; }
    }
    [SerializeField]
    private string effectName;
    public string EffectName
    {
        set { effectName = value; }
        get { return effectName; }
    }

}

[CreateAssetMenu(fileName = "Effect", menuName = "ScriptableObjects/Effect", order = 11)]
public class EffectList : ScriptableObject
{
    [SerializeField]
    private Effects[] m_effect;
    public Effects[] EffectTable
    {
        set { m_effect = value; }
        get { return m_effect; }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

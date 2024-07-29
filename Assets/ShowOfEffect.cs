using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowOfEffect : MonoBehaviour
{
    [SerializeField]
    private EffectList effectList;
    [SerializeField]
    private Text m_text;
    private List<Effects> m_effect;
    private int m_index = 0;
    private int m_effectIndex = 0;



    // Start is called before the first frame update
    void Start()
    {
        m_effect = new List<Effects>();
        var lists = effectList.EffectTable;
        m_effectIndex = lists.Length;
        for (int i = 0; i < m_effectIndex; ++i)
        {
            Effects effects = lists[i];
            effects.EffectPrefab = Instantiate(effects.EffectPrefab);
            effects.EffectPrefab.SetActive(false);
            m_effect.Add(effects);
            // m_effect.Add(lists[i]);
            // var ef = m_effect[i];
            //m_effect[i].EffectPrefab = Instantiate(m_effect[i].EffectPrefab);
            //m_effect[i].EffectPrefab.SetActive(false);
        }
        m_index = 0;
        m_effect[m_index].EffectPrefab.SetActive(true);
        m_text.text = m_effect[m_index].EffectName;
    }

    // Update is called once per frame
    void Update()
    {
        int m_preIndex = m_index;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ++m_index;
            m_index %= m_effectIndex;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_index = (m_effectIndex + m_index) - 1;
            m_index %= m_effectIndex;
        }

        if (m_index != m_preIndex)
        {
            m_effect[m_preIndex].EffectPrefab.SetActive(false);
            m_effect[m_index].EffectPrefab.SetActive(true);
            m_text.text = m_effect[m_index].EffectName;

        }

    }
}

using UnityEngine;

public class SetWord : MonoBehaviour
{
    private string m_word = null;
    public string Word
    {
        get
        {
            return m_word;
        }
        set { m_word = value; }
    }
    private MovingSichiyou m_textObject = null;
    public MovingSichiyou TextObject
    {
        get { return m_textObject; }
        set { m_textObject = value; }
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

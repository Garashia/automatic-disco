using TMPro;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TextMesh;
    public TextMeshProUGUI Text
    {
        get { return m_TextMesh; }
        set { m_TextMesh = value; }
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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
struct Attribute
{
    [SerializeField]
    private string name;
    public string AttributeName
    {
        get { return name; }
    }
    [SerializeField]
    private Sprite sprite;
    public Sprite AttributeSprite
    {
        get { return sprite; }
        set { sprite = value; }
    }
}

[CreateAssetMenu(fileName = "Sichiyou", menuName = "ScriptableObjects/Sichiyou", order = 2)]
public class Sichiyou : ScriptableObject
{
    [SerializeField]
    private GameObject m_gameObject;
    [SerializeField]
    private List<Attribute> m_sprite;

    public List<GameObject> GetAttributeObject()
    {
        List<GameObject> list = new List<GameObject>();
        foreach (Attribute a in m_sprite)
        {
            GameObject gameObject = Instantiate(m_gameObject);
            GameObject Child = new GameObject(a.AttributeName);
            Child.transform.parent = gameObject.transform;
            Image image = Child.AddComponent<Image>();
            image.sprite = a.AttributeSprite;
            list.Add(gameObject);
        }

        return list;
    }

    public List<string> GetAttributeSprite()
    {
        List<string> list = new List<string>();
        foreach (Attribute a in m_sprite)
        {
            list.Add(a.AttributeName);
        }
        return list;

    }

}

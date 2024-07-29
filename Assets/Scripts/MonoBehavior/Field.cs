using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    struct GameCulture
    {
        public GameObject gameObject;
        public Vector2Int positionNumber;
        // public bool flag = false;
    }

    [SerializeField]
    private GameObject m_field;
    [SerializeField]
    private GameObject m_player;

    private List<GameCulture> m_map = new List<GameCulture>();
    // Start is called before the first frame update
    void Start()
    {
        // m_map = new List<GameCulture>();
        m_player.layer = 5;
    }

    public void Initialize(Dictionary<Vector2Int, Vector3> PositionNumber)
    {
        // m_map.Add
        foreach (var (id, pos) in PositionNumber)
        {
            GameCulture gameCulture = new GameCulture();
            GameObject gamerObject = Instantiate(m_field, transform);
            var i = gamerObject.GetComponent<RectTransform>();
            if (i != null)
            {
                i.position = new Vector3(id.x * i.sizeDelta.x, id.y * i.sizeDelta.y, 0.0f);
            }
            gameCulture.gameObject = gamerObject;
            gameCulture.positionNumber = id;
            gameCulture.gameObject.SetActive(false);
            m_map.Add(gameCulture);
        }
        m_player = GameObject.Instantiate(m_player, transform);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNumberPosition(Vector2Int number)
    {
        foreach (var i in m_map)
        {
            var Num = i.positionNumber;
            Debug.Log(Num.x + ", " + Num.y);

            if (Num.x == number.x && Num.y == number.y)
            {
                i.gameObject.SetActive(true);
                var Re = m_player.gameObject.GetComponent<RectTransform>();
                if (Re != null)
                {
                    m_player.transform.position =
                        new Vector3(Num.x * Re.sizeDelta.x, Num.y * Re.sizeDelta.y, 0.0f);

                }
                return;
            }
        }
    }

}

using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Field m_field;

    private Dictionary<Vector2Int, Vector3> m_positionNumber
        = new Dictionary<Vector2Int, Vector3>();
    public Dictionary<Vector2Int, Vector3> PositionNumber
    {
        set { m_positionNumber = value; }
    }
    private Vector2Int m_direction;
    private Vector2Int m_positionNum;
    private Vector2Int[] m_positionArray = new[]
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0),

    };
    private int index;
    private bool m_flag;

    [SerializeField]
    private PlayerDo m_playerDo;
    private List<Players> m_playerList = new List<Players>();

    public PlayerDo PlayerDoes
    {
        set { m_playerDo = value; }
        get { return m_playerDo; }

    }
    public bool Flag
    {
        set { m_flag = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_playerList.Clear();
        m_playerList = m_playerDo.PlayersList();

        foreach (var player in m_playerList)
        {
            player.Initialize();
        }


        m_direction = new(0, 1);
        index = 0;
        m_flag = true;
    }

    public void Initialize()
    {
        foreach (var (id, pos) in m_positionNumber)
        {
            transform.position = pos;
            m_positionNum = id;
            break;
        }
        if (m_field != null)
        {
            m_field.Initialize(m_positionNumber);
            m_field.SetNumberPosition(m_positionNum);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_positionNumber == null)
        {
            return;
        }
        if (!m_flag) return;
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (m_positionNumber.ContainsKey(m_direction + m_positionNum))
            {
                transform.position = m_positionNumber[m_direction + m_positionNum];
                m_positionNum = m_direction + m_positionNum;
                m_field.SetNumberPosition(m_positionNum);
                FindEnemy.Walking(this);

            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ++index;
            index %= 4;
            m_direction = m_positionArray[index];
            transform.rotation *= Quaternion.AngleAxis(90.0f, Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            index = (index + 4) - 1;
            index %= 4;
            m_direction = m_positionArray[index];
            transform.rotation *= Quaternion.AngleAxis(-90.0f, Vector3.up);

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            index += 2;
            index %= 4;
            m_direction = m_positionArray[index];
            transform.rotation *= Quaternion.AngleAxis(180.0f, Vector3.up);
        }

    }
}

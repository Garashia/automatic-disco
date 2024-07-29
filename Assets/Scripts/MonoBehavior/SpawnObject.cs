using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    private GameObject m_floor;
    [SerializeField]
    private GameObject m_wall;
    [SerializeField]
    private GameObject m_corner;
    [SerializeField]
    private PlayerManager m_player;

    private readonly Vector2Int[] ORIENTATION = new[]
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0)
    };
    private Vector2Int[,] ORIENTATION2 = new Vector2Int[4, 3]
    {
        {
           new Vector2Int(0, 1),
           new Vector2Int(1, 1),
           new Vector2Int(1, 0)

        },
        {
           new Vector2Int(0, 1),
           new Vector2Int(-1, 1),
           new Vector2Int(-1, 0)

        },
        {
           new Vector2Int(0, -1),
           new Vector2Int(1, -1),
           new Vector2Int(1, 0)

        },
        {
           new Vector2Int(0, -1),
           new Vector2Int(-1, -1),
           new Vector2Int(-1, 0)

        },

    };

    static private readonly uint Corner_Up_Right = (1 << 0);
    static private readonly uint Corner_Up_Left = (1 << 1);
    static private readonly uint Corner_Down_Right = (1 << 2);
    static private readonly uint Corner_Down_Left = (1 << 3);
    static private readonly uint Wall_Up = (1 << 0);
    static private readonly uint Wall_Down = (1 << 1);
    static private readonly uint Wall_Right = (1 << 2);
    static private readonly uint Wall_Left = (1 << 3);
    private readonly uint[] WALL_ORIENTATION = new[]
    {
        Wall_Up,
        Wall_Down,
        Wall_Right,
        Wall_Left
    };
    private readonly uint[] CORNER_ORIENTATION = new[]
{
        Corner_Up_Right,
        Corner_Up_Left,
        Corner_Down_Right,
        Corner_Down_Left
    };

    struct Distraction
    {
        private uint corner;
        public uint Corner
        {
            get { return corner; }
            set { corner = value; }
        }



        private uint wall;
        public uint Wall
        {
            get { return wall; }
            set { wall = value; }
        }


    }

    private MazeFactory m_mazeFactory;
    private int[,] m_area;
    private readonly float m_scale = 0.9f;
    private readonly float m_high = 0.275f;
    private readonly float m_wallInvert = 0.39f;
    private readonly float m_cornerInvert = 0.375f;

    private List<GameObject> m_spawnObjects;

    private Dictionary<Vector2Int, Vector3> m_positionNumber;
    public Dictionary<Vector2Int, Vector3> PositionNumber
    {
        get { return m_positionNumber; }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_positionNumber = new Dictionary<Vector2Int, Vector3>();
        m_spawnObjects = new List<GameObject>();
        m_mazeFactory = new(50, 50);
        m_area = m_mazeFactory.CreateMaze();
        int length_x = m_area.GetLength(0);
        for (int x = 0; x < length_x; ++x)
        {
            int length_y = m_area.GetLength(1);
            for (int z = 0; z < length_y; z++)
            {
                if (m_area[x, z] == 1) continue;
                Vector3 position = new Vector3((float)(x) * m_scale, 0.0f, (float)(z) * m_scale);

                GameObject child = GameObject.Instantiate(m_floor, gameObject.transform);
                child.transform.localPosition = position;
                var distraction = DistractionArea(m_area, x, z);
                m_spawnObjects.Add(child);
                SpawnWall(
                    distraction.Wall,
                    m_spawnObjects,
                    position,
                    child.transform);
                SpawnCorner(
                    distraction.Corner,
                    m_spawnObjects,
                    position,
                    child.transform);
                m_positionNumber.Add(new(x, z), child.transform.position);
            }
        }
        if (m_player)
        {
            m_player.PositionNumber = m_positionNumber;
            m_player.Initialize();
        }
        // this.gameObject.transform.localScale = new Vector3(5.0f, 9.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Distraction DistractionArea(in int[,] area, int x, int y)
    {
        int Max_x = area.GetLength(0);
        int Max_y = area.GetLength(1);

        Distraction distraction = new Distraction();
        for (int i = 0; i < ORIENTATION.Length; ++i)
        {
            int posX = ORIENTATION[i].x + x;
            if (posX <= 0 || posX >= Max_x)
            {
                distraction.Wall |= WALL_ORIENTATION[i];
                continue;
            }
            int posY = ORIENTATION[i].y + y;
            if (posY <= 0 || posY >= Max_y)
            {
                distraction.Wall |= WALL_ORIENTATION[i];
                continue;
            }

            if (area[posX, posY] == 0) continue;
            distraction.Wall |= WALL_ORIENTATION[i];

        }
        Vector2Int vector2Int = new Vector2Int(x, y);
        for (int i = 0; i < ORIENTATION2.GetLength(0); ++i)
        {
            // uint m_m = 0;
            var zz = ORIENTATION2[i, 0];
            var zx = ORIENTATION2[i, 1];
            var xx = ORIENTATION2[i, 2];

            var PXX = vector2Int + xx;
            var PXY = vector2Int + zx;
            var PYY = vector2Int + zz;


            int posY = PYY.y;
            if (posY <= 0 || posY >= Max_y)
            {
                continue;
            }
            int posX = PXX.x;
            if (posX <= 0 || posX >= Max_x)
            {
                continue;
            }
            if (area[PYY.x, PYY.y] == 0 && area[PXX.x, PXX.y] == 0
                && area[PXY.x, PXY.y] == 1)
            {
                distraction.Corner |= CORNER_ORIENTATION[i];
            }

        }

        return distraction;
    }

    private void SpawnWall
        (
        uint walls,
        List<GameObject> gameObjects,
        Vector3 position,
        Transform parent
        )
    {
        if (walls == 0) return;
        if ((walls & Wall_Up) != 0)
        {
            Vector3 pos = new Vector3(0.0f, m_high, m_wallInvert);
            var game = Spawn(pos, parent, m_wall);
            gameObjects.Add(game);
            // ga.transform.localRotation
        }
        if ((walls & Wall_Down) != 0)
        {
            Vector3 pos = new Vector3(0.0f, m_high, -m_wallInvert);
            var game = Spawn(pos, parent, m_wall);
            gameObjects.Add(game);
        }
        if ((walls & Wall_Right) != 0)
        {
            Vector3 pos = new Vector3(m_wallInvert, m_high, 0.0f);
            var game = Spawn(pos, parent, m_wall);
            game.transform.localRotation = Quaternion.AngleAxis(90.0f, Vector3.up);
            gameObjects.Add(game);
        }
        if ((walls & Wall_Left) != 0)
        {
            Vector3 pos = new Vector3(-m_wallInvert, m_high, 0.0f);
            var game = Spawn(pos, parent, m_wall);
            game.transform.localRotation = Quaternion.AngleAxis(90.0f, Vector3.up);
            gameObjects.Add(game);
        }
    }

    private void SpawnCorner(
        uint corners,
        List<GameObject> gameObjects,
        Vector3 position,
        Transform parent
        )
    {
        if (corners == 0) return;
        if ((corners & Corner_Up_Right) != 0)
        {
            Vector3 pos = new Vector3(m_cornerInvert, m_high, m_cornerInvert);

            gameObjects.Add(Spawn(pos, parent, m_corner));
        }
        if ((corners & Corner_Up_Left) != 0)
        {
            Vector3 pos = new Vector3(-m_cornerInvert, m_high, m_cornerInvert);
            gameObjects.Add(Spawn(pos, parent, m_corner));
        }
        if ((corners & Corner_Down_Right) != 0)
        {
            Vector3 pos = new Vector3(m_cornerInvert, m_high, -m_cornerInvert);
            gameObjects.Add(Spawn(pos, parent, m_corner));
        }
        if ((corners & Corner_Down_Left) != 0)
        {
            Vector3 pos = new Vector3(-m_cornerInvert, m_high, -m_cornerInvert);
            gameObjects.Add(Spawn(pos, parent, m_corner));
        }
    }
    private GameObject Spawn(Vector3 pos, Transform parent, GameObject spawn)
    {
        GameObject gamerObject = GameObject.Instantiate(
                spawn, parent);
        gamerObject.transform.localPosition = pos;
        return gamerObject;
    }
}

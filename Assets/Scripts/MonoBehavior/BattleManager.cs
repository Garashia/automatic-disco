using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


//using UnityEngine.UIElements;
public class BattleManager : MonoBehaviour
{
    [System.Serializable]
    struct CommandParameterList
    {
        public ICommand command;
        public Parameter parameter;
    }

    [SerializeField]
    private Canvas m_canvas;
    private GameObject m_canvasObject;
    [SerializeField]
    private TextBox m_textBox;
    private GameObject m_textBoxObject;
    [SerializeField]
    private Sichiyou sichiyou;
    private List<GameObject> m_sichiyouObjects;

    [SerializeField]
    private EnemyList enemyList;
    private EnemyParameter m_enemy;
    private GameObject m_enemyObject;
    [SerializeField]
    private SpriteManager m_spriteManager;
    private List<Commander> m_commanderList = new List<Commander>();
    private GameObject m_button;
    private GameObject m_rendererObject;
    private List<GameObject> selectObjects;
    private List<GameObject> mastObjects;
    private GameObject m_checkButton;

    private Correct m_correct = null;

    [SerializeField]
    private Text m_text;

    public delegate void TextDelegate(RectTransform rectTransform, TextBox textBox);
    private int m_enemyHP = 0;
    public int EnemyHP
    {
        get { return m_enemyHP; }
        set { m_enemyHP = value; }
    }

    [SerializeField]
    private PlayerDo m_players;
    private List<Players> m_playerList = new List<Players>();
    // Start is called before the first frame update
    void Start()
    {
        var players = FindEnemy.GetPlayerLists();
        if (players == null)
        {
            players = m_players;
            m_playerList = players.PlayersList();

            foreach (var player in m_playerList)
            {
                player.Initialize();
            }
        }
        else
        {
            m_playerList = players.PlayersList();
        }
        m_sichiyouObjects = new List<GameObject>();
        selectObjects = new List<GameObject>();
        mastObjects = new List<GameObject>();
        m_button = new GameObject("button");
        BattleObserver.Observer.Manager = this;

        m_canvasObject = m_canvas.gameObject;
        m_textBoxObject = m_textBox.gameObject;
        m_button.AddComponent<Image>();

        Button button = m_button.AddComponent<Button>();
        button.interactable = false;
        m_button.transform.localScale = Vector3.zero;

        Enemy();
    }

    ~BattleManager()
    {
        BattleObserver.Observer = null;
    }

    private void Sichiyou()
    {
        m_sichiyouObjects.Clear();
        m_sichiyouObjects = sichiyou.GetAttributeObject();
        int size = m_sichiyouObjects.Count;
        float point = 80 * (float)size * 0.5f;
        var strings = sichiyou.GetAttributeSprite();
        for (int i = 0; i < size; ++i)
        {
            m_sichiyouObjects[i].transform.parent = m_canvasObject.transform;
            var rect = m_sichiyouObjects[i].GetComponent<RectTransform>();
            if (rect == null)
            {
                continue;
            }
            rect.anchoredPosition = Vector3.zero;
            Vector3 pos = rect.anchoredPosition;
            rect.sizeDelta = Vector3.one * 80;
            float width = rect.sizeDelta.x;
            pos.x = -point + (width * (float)i);
            pos.y = -300.0f;
            rect.anchoredPosition = pos;
            m_sichiyouObjects[i].SetActive(false);
            MovingSichiyou vale = m_sichiyouObjects[i].GetComponent<MovingSichiyou>();
            vale.Name = strings[i];
            GameObject child = m_sichiyouObjects[i].transform.GetChild(0).gameObject;
            var childRect = child.GetComponent<RectTransform>();
            childRect.sizeDelta = Vector3.one * 60;

            vale.SetCanvas(m_canvas);
        }
        if (m_correct == null)
        {
            m_correct = new Correct();
            m_correct.SetCorrect(strings);

        }
    }

    private void MastBox()
    {
        mastObjects.Clear();
        if (m_rendererObject == null)
        {
            m_rendererObject = new GameObject("mastBox");
            Image image = m_rendererObject.AddComponent<Image>();
            image.sprite = m_spriteManager.SphereSprite;
            image.raycastTarget = false;
            m_rendererObject.SetActive(false);

        }
        float point = 100.0f * 4.0f * 0.5f;
        for (int i = 0; i < 4; ++i)
        {
            GameObject game = Instantiate(m_rendererObject, m_canvasObject.transform);
            var rect = game.GetComponent<RectTransform>();
            if (rect == null)
            {
                continue;
            }
            rect.anchoredPosition = Vector3.zero;
            Vector2 pos = rect.anchoredPosition;
            rect.sizeDelta = Vector3.one * 100.0f;

            float width = rect.sizeDelta.x;
            pos.x = -point + (width * (float)i);
            pos.y = -200.0f;
            rect.anchoredPosition = pos;
            game.AddComponent<SetWord>();
            mastObjects.Add(game);
        }
    }

    private void SetSichiyouActive(bool active)
    {
        foreach (var obj in m_sichiyouObjects)
        {
            obj.SetActive(active);
        }
        foreach (var obj in mastObjects)
        {
            obj.SetActive(active);
        }
    }

    private void Enemy()
    {
        RandomEnemy();
        m_enemyObject = new GameObject(m_enemy.Name);
        m_enemyObject.transform.parent = m_canvasObject.transform;
        Image image = m_enemyObject.AddComponent<Image>();
        image.sprite = m_enemy.GetSprite;
        image.SetNativeSize();
        image.raycastTarget = false;
        var rect = m_enemyObject.GetComponent<RectTransform>();

        rect.anchoredPosition = Vector2.zero;
        m_enemyObject.transform.localScale = Vector3.zero;
        List<ICommand> commands = new List<ICommand>()
            {
                new ScaleCommand(),
                new WaitCommand(),
                new AnchoredCommand()
           };
        List<Parameter> parameter = new List<Parameter>()
        {
             new Parameter(
            go: m_enemyObject, aScale: new Vector3(1.0f, 1.0f, 1.0f), fs: Vector3.zero),
             new Parameter(go:m_enemyObject, t: 0.8f),
                          new Parameter(
            go: m_enemyObject, frp: rect.anchoredPosition, rd: Vector2.up * 200.0f),

        };
        MicroCommander microCommander = new MicroCommander();
        microCommander.Message = "Player";
        for (int i = 0; i < 3; ++i)
        {
            microCommander.AddCommand(commands[i], parameter[i]);
        }
        m_commanderList.Add(microCommander);
        m_enemyHP = (int)m_enemy.HP;
    }

    private void RandomEnemy()
    {
        float totalWeight = enemyList.TotalWeight;
        // 0Å`èdÇ›ÇÃëçòaÇÃîÕàÕÇÃóêêîíléÊìæ
        float randomPoint = UnityEngine.Random.Range(0.0f, totalWeight);
        float currentWeight = 0f;
        foreach (var enemy in enemyList.CharaList)
        {
            currentWeight += enemy.Weight;
            if (randomPoint < currentWeight)
            {
                m_enemy = enemy.Enemy;
                break;
            }
        }
    }

    public void SetMessage1(string message, Vector2 position, string word)
    {
        GameObject text = Instantiate(m_textBoxObject, m_canvasObject.transform);
        List<ICommand> commands = new List<ICommand>()
            {
                new TextCommand(),
                new WaitCommand(),
                new TextDeleteCommand(),
                new DestroyCommand()
           };
        Parameter parameter = new Parameter(go: text, w: message, t: 0.8f);
        SetMessage(text, position, word, commands, parameter);


    }

    public void SetMessage2(string message, Vector2 position, string word)
    {
        GameObject text = Instantiate(m_textBoxObject, m_canvasObject.transform);
        List<ICommand> commands = new List<ICommand>()
            {
                new TextCommand(),
                new WaitCommand(),
                new ScaleCommand()
           };
        Parameter parameter = new Parameter(
            go: text,
            t: 0.8f,
            fs: new Vector3(1.0f, 1.0f, 1.0f),
            aScale: new Vector3(0.0f, 0.0f, 0.0f),
            w: message);
        SetMessage(text, position, word, commands, parameter);
    }

    public void SetMessage3(string message, Vector2 position, string word)
    {
        GameObject text = Instantiate(m_textBoxObject, m_canvasObject.transform);
        List<ICommand> commands = new List<ICommand>()
            {
                new TextCommand(),
                new WaitCommand(),
                new DestroyCommand()
           };
        Parameter parameter = new Parameter(go: text, w: message, t: 0.8f);
        SetMessage(text, position, word, commands, parameter);
    }

    private void SetMessage(GameObject text,
        Vector2 position, string word, List<ICommand> commands, Parameter parameter)
    {
        text.GetComponent<RectTransform>().anchoredPosition = position;
        MicroCommander microCommander = new MicroCommander();
        microCommander.Message = word;
        foreach (ICommand command in commands)
        {
            microCommander.AddCommand(command, parameter);
        }
        m_commanderList.Add(microCommander);
    }

    public GameObject SetText(string message, Vector2 position)
    {
        GameObject text = Instantiate(m_textBoxObject, m_canvasObject.transform);
        text.GetComponent<RectTransform>().anchoredPosition = position;
        text.GetComponent<TextBox>().Text.text = message;
        return text;

    }

    public GameObject SetText(string message, Vector2 position, TextDelegate textDelegate)
    {
        GameObject text = Instantiate(m_textBoxObject, m_canvasObject.transform);
        var rect = text.GetComponent<RectTransform>();
        var box = text.GetComponent<TextBox>();
        rect.anchoredPosition = position;
        box.Text.text = message;
        textDelegate(rect, box);
        return text;
    }

    // Update is called once per frame
    void Update()
    {
        int index = m_commanderList.Count;
        if (index == 0)
            return;
        for (int i = index - 1; i >= 0; --i)
        {
            if (!m_commanderList[i].Execute())
            {
                m_commanderList.RemoveAt(i);
            }
        }
    }

    public void ReceiveMessages(string message)
    {
        if (message == "Player")
        {
            var rect = m_enemyObject.GetComponent<RectTransform>();
            BattleObserver.Observer.SetMessage1(m_enemy.Name + "\nÇ™åªÇÍÇΩ", rect.anchoredPosition, "Battle");
        }
        else if (message == "Battle")
        {
            Selector();
        }
        else if (message == "Escape")
        {
            EscapeBattle();
        }
        else if (message == "Done")
        {
            FindEnemy.EndBattle();
        }
    }

    private void Selector()
    {
        selectObjects.Add
        (
            CommandSelector
            (
                new Vector3(-500.0f, -200.0f, 0.0f),
                m_spriteManager.BattleSprite,
                () => BattleSelector()
            )
        );
        selectObjects.Add
        (
            CommandSelector
            (
                new Vector3(500.0f, -200.0f, 0.0f),
                m_spriteManager.EscapeSprite,
                () => EscapeSelector()
            )
        );
    }

    private void BattleSelector()
    {
        DestroySelector();
        Sichiyou();
        MastBox();

        SetSichiyouActive(true);


    }

    private void EscapeBattle()
    {
        Parameter scaleP = new Parameter(
            go: m_enemyObject, fs: new Vector3(1.0f, 1.0f, 1.0f), aScale: -new Vector3(1.0f, 1.0f, 1.0f));
        ICommand
             command = new ScaleCommand();
        command.SetParameter(scaleP);
        Commander commander = new Commander();
        commander.SetCommand(command);
        commander.SetParameter(scaleP);
        commander.Message = "Done";
        m_commanderList.Add(commander);
    }

    private void EscapeSelector()
    {
        DestroySelector();
        var rect = m_enemyObject.GetComponent<RectTransform>();
        BattleObserver.Observer.SetMessage3("ì¶Ç∞êÿÇÍÇΩ", rect.anchoredPosition, "Escape");


    }

    private void DestroySelector()
    {
        int count = selectObjects.Count;
        for (int i = count - 1; i >= 0; --i)
        {
            Destroy(selectObjects[i]);
        }

    }

    private void DestroyList()
    {
        int count1 = mastObjects.Count;
        int count2 = m_sichiyouObjects.Count;
        for (int i = count1 - 1; i >= 0; --i)
        {
            Destroy(mastObjects[i]);
        }
        for (int i = count2 - 1; i >= 0; --i)
        {
            Destroy(m_sichiyouObjects[i]);
        }

    }

    private GameObject CommandSelector(Vector3 spawnPos, Sprite sprite, UnityAction call = null)
    {
        GameObject select = Instantiate(m_button, m_canvasObject.transform);
        select.GetComponent<Image>().sprite = sprite;
        select.GetComponent<Image>().SetNativeSize();
        select.GetComponent<Button>().onClick.AddListener(call);
        List<ICommand> commands = new List<ICommand>()
            {
                new MovingWideningCommand(),
                new ButtonClickerCommand(),
           };
        Parameter parameter = new Parameter(
            go: select, fs: new Vector3(0.0f, 0.0f, 0.0f), aScale: new Vector3(3.0f, 3.0f, 1.0f),
            frp: new Vector3(0.0f, 0.0f, 0.0f), rd: spawnPos);
        MicroCommander microCommander = new MicroCommander();
        foreach (ICommand command in commands)
        {
            microCommander.AddCommand(command, parameter);
        }
        m_commanderList.Add(microCommander);
        return select;
    }

    public Vector2? SichiyouSetting(Vector2 position, string word, MovingSichiyou sichiyou)
    {
        float dist = float.MaxValue;
        Vector2? vector2 = null;
        GameObject game = null;
        SetWord second = null;
        foreach (GameObject mast in mastObjects)
        {
            SetWord setWord = mast.GetComponent<SetWord>();
            if (setWord.TextObject == sichiyou)
            {
                second = setWord;
            }


            RectTransform rect = mast.GetComponent<RectTransform>();
            float distance = (rect.anchoredPosition - position).sqrMagnitude;
            if (4900 < distance) continue;
            if (dist < distance) continue;

            dist = distance;
            vector2 = rect.anchoredPosition;
            game = mast;

        }
        if (game != null)
        {
            if (second != null)
            {
                second.TextObject = null;
                second.Word = null;
            }

            SetWord worder = game.GetComponent<SetWord>();
            var text = worder.TextObject;
            if (text != null)
            {
                text.ResetPosition();
            }
            worder.Word = word;
            worder.TextObject = sichiyou;
            IsSichiyou();
        }

        return vector2;
    }

    private void IsSichiyou()
    {
        bool el_dorado = true;
        foreach (GameObject mast in mastObjects)
        {
            SetWord set = mast.GetComponent<SetWord>();
            if (set.Word == null)
            {
                el_dorado = false;
                break;
            }
        }
        if (el_dorado)
        {
            CheckButton();
        }
        else
        {
            Destroy(m_checkButton);
            m_checkButton = null;
        }

    }

    void CheckButton()
    {
        m_checkButton ??= Instantiate(m_button, m_canvas.transform);
        RectTransform rect = m_checkButton.GetComponent<RectTransform>();
        Vector2 pos = rect.anchoredPosition;
        pos.y = -400.0f;
        pos.x = 0.0f;
        rect.anchoredPosition = pos;
        m_checkButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Button button = m_checkButton.GetComponent<Button>();
        if (button != null)
        {
            button.interactable = true;
            button.onClick.AddListener(() => AttackEnemy());
        }
        Image image = m_checkButton.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = m_spriteManager.AttackSprite;
            image.SetNativeSize();
        }
        m_checkButton.SetActive(true);
        ;
    }

    struct Evidence
    {
        public Players players;
        public IsCorrect isCorrect;
    }

    private void AttackEnemy()
    {
        List<string> key = new List<string>();
        foreach (GameObject mast in mastObjects)
        {
            SetWord set = mast.GetComponent<SetWord>();
            key.Add(set.Word);
        }
        var ika = m_correct.IsCorrected(key);
        bool flag = true;
        int all = 0;
        int some = 0;
        foreach (var o in ika)
        {
            if (o != IsCorrect.All)
                flag = false;
            if (o == IsCorrect.All)
            {
                ++all;
            }
            else if (o == IsCorrect.Some)
            {
                ++some;
            }
        }

        foreach (string k in key)
        {
            m_text.text += k + ", ";
        }
        m_text.text += "\n";
        m_text.text += "ê≥:" + all.ToString() + "ïÅ:" + some.ToString();
        m_text.text += "\n";
        Destroy(m_checkButton);
        m_checkButton = null;
        DestroyList();
        var words = m_correct.correctWord;
        string m = "";
        foreach (var o in words)
            m += o;
        Debug.Log(m);
        if (flag)
        {
            m_text.text = "";
            m_correct = null;
        }
        float pre = m_enemyHP;
        for (int i = 0; i < 4; ++i)
        {
            float attack = (float)m_playerList[i].Power;
            float guard = (float)m_enemy.Guard;

            m_enemyHP -= DamageCalculation.Damage(attack, guard, ika[i]);
        }
        BattleObserver.Observer.SetMessage3(
            (pre - m_enemyHP).ToString() + "É_ÉÅÅ[ÉW!!",
            Vector2.down * 300.0f);
        if (m_enemyHP < 0)
        {
            BattleObserver.Observer.SetMessage3("ìGÇì|ÇµÇΩ!!", Vector2.zero, "Escape");
            m_text.text = "";
            for (int i = 0; i < 4; ++i)
            {
                m_playerList[i].ExperienceAcquisition(m_enemy.EX);
            }
        }
        else
        {
            BattleObserver.Observer.SetMessage1(
                "à íuÇä‹ÇﬂÇƒê≥ÇµÇ¢:" + all.ToString() + ", à íuÇèúÇƒê≥â:" + some.ToString(),
                Vector2.zero,
                "Battle"
                );

        }
    }

}

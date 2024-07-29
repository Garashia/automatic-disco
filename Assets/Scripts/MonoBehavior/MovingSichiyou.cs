using UnityEngine;

public class MovingSichiyou : MonoBehaviour
{
    private Vector2 m_position;
    [SerializeField]
    private Canvas _canvas = null;
    [SerializeField]
    private RectTransform _canvasTransform = null;
    private bool _isDragging = false;
    private RectTransform m_MousePosition;
    private Vector2 _direct;
    private string _name;
    private Vector2 m_firstPosition;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_MousePosition ??= GetComponent<RectTransform>();
        // m_firstPosition = Vector2.zero;
        _direct = Vector2.zero;
        m_position = m_MousePosition.anchoredPosition;
        _isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_canvas == null || _canvasTransform == null) return;
        Vector2 mousePos = Input.mousePosition;
        var magnification = _canvasTransform.sizeDelta.x / Screen.width;
        mousePos.x = mousePos.x * magnification - _canvasTransform.sizeDelta.x / 2;
        mousePos.y = mousePos.y * magnification - _canvasTransform.sizeDelta.y / 2;
        // mousePos.z = transform.localPosition.z;
        if (_isDragging)
        {
            m_MousePosition.anchoredPosition = mousePos + _direct;
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
    }

    public void PointerDown()
    {
        Vector2 mousePos = Input.mousePosition;
        var magnification = _canvasTransform.sizeDelta.x / Screen.width;
        mousePos.x = mousePos.x * magnification - _canvasTransform.sizeDelta.x / 2;
        mousePos.y = mousePos.y * magnification - _canvasTransform.sizeDelta.y / 2;
        // mousePos.z = transform.localPosition.z;
        Vector2 vector2 = m_MousePosition.anchoredPosition;
        m_position = m_MousePosition.anchoredPosition;
        _direct = vector2 - mousePos;
        _isDragging = true;
    }

    public void PointerUp()
    {
        Vector2? vector2 = BattleObserver.Observer.SichiyouSetting(
            m_MousePosition.anchoredPosition, _name, this);
        if (vector2 != null)
        {
            m_position = (Vector2)vector2;
        }
        m_MousePosition.anchoredPosition = m_position;
        _isDragging = false;

    }

    public void SetCanvas(Canvas canvas)
    {
        _direct = Vector2.zero;
        _canvas = canvas;
        _canvasTransform = _canvas.GetComponent<RectTransform>();
        m_MousePosition ??= GetComponent<RectTransform>();
        m_firstPosition = m_MousePosition.anchoredPosition;
    }

    public void ResetPosition()
    {
        m_MousePosition.anchoredPosition = m_firstPosition;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    GameObject collObject = collision.gameObject;
    //    if (collObject.CompareTag("Mouse"))
    //    {
    //        Debug.Log(22);
    //        m_position = transform.position;
    //        gameObject1 = collObject;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    GameObject collObject = collision.gameObject;
    //    if (collObject.CompareTag("Mouse"))
    //    {
    //        transform.position = m_position;
    //        gameObject1 = null;
    //    }

    //}

}

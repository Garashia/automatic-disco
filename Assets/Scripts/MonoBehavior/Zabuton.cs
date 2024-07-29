using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UIElements;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Zabuton : MonoBehaviour
{
    private const float Tolerance = 0.00001f;

    [SerializeField]
    private Image m_image;
    [SerializeField]
    private float m_paddingWidth;
    public float PaddingWidth
    {
        get { return m_paddingWidth; }
    }

    [SerializeField]
    private float m_paddingHeight;
    public float PaddingHeight
    {
        get { return m_paddingHeight; }
    }

    private TextMeshProUGUI m_tmp;
    private float m_preWidth;
    private float m_preHeight;

    // public Zabuton()

    private void Start()
    {
        m_tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Math.Abs(m_preWidth - m_tmp.preferredWidth) < Tolerance && Math.Abs(m_preHeight - m_tmp.preferredHeight) < Tolerance) return;

        UpdateTMProUGUISizeDelta();
        UpdateImageSizeDelta();
    }

    /// <summary>
    /// RectTransform.sizeDeltaをテキストにぴっちりさせる
    /// </summary>
    private void UpdateTMProUGUISizeDelta()
    {
        m_preWidth = m_tmp.preferredWidth;
        m_preHeight = m_tmp.preferredHeight;
        m_tmp.rectTransform.sizeDelta = new Vector2(m_preWidth, m_preHeight);
    }

    /// <summary>
    /// 背景のImageのRectTransform.sizeDeltaを指定したパディングで更新
    /// </summary>
    private void UpdateImageSizeDelta()
    {
        if (m_preHeight == 0 || m_preWidth == 0)
        {
            m_image.rectTransform.sizeDelta = Vector2.zero;
            return;
        }

        m_image.rectTransform.sizeDelta = new Vector2(m_preWidth + m_paddingWidth, m_preHeight + m_paddingHeight);
    }
}

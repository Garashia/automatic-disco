using System.Collections;
using UnityEngine;

public class AnchoredCommand : ICommand
{
    private Parameter m_parameter;
    private bool m_equals = false;
    private bool m_once = false;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.01f);

    // パラメータを取得する
    virtual public Parameter GetParameter()
    {
        return m_parameter;
    }
    // パラメータを設定する
    virtual public void SetParameter(Parameter parameter)
    {
        m_parameter = parameter;
    }
    // 実行する
    virtual public void Execute()
    {
        if (!m_once)
        {
            m_parameter.lastRectPosition = m_parameter.firstRectPosition +
                m_parameter.RectDirection;

            CoroutineHandler.StartStaticCoroutine(Enumerator());
            m_once = true;
        }
    }

    virtual public IEnumerator Enumerator()
    {
        float m_lerpT = 0.0f;
        Vector2 firstPosition = m_parameter.firstRectPosition;
        Vector2 lastPosition = m_parameter.lastRectPosition;
        GameObject gameObject = m_parameter.gameObject;
        var rect = gameObject.GetComponent<RectTransform>();
        if (rect == null) { yield break; }
        Transform transform = gameObject.transform;
        while (m_lerpT < 1.0f)
        {
            m_lerpT += Time.deltaTime;
            rect.anchoredPosition = Vector2.Lerp(
                firstPosition, lastPosition, m_lerpT);
            yield return waitForSeconds;
        }

        // BattleObserver.Observer.SetMessage1(m_parameter.word, rect.anchoredPosition);

        m_equals = true;
    }

    virtual public bool Enable()
    {
        return m_equals;
    }
}

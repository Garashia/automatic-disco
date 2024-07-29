using System.Collections;
using UnityEngine;

public class MovingCommand : ICommand
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
            m_parameter.firstPosition = m_parameter.gameObject.transform.localPosition;
            m_parameter.lastPosition = m_parameter.firstPosition +
                m_parameter.direction;

            CoroutineHandler.StartStaticCoroutine(Enumerator());
            m_once = true;
        }
    }

    virtual public IEnumerator Enumerator()
    {
        float m_lerpT = 0.0f;
        Vector3 firstPosition = m_parameter.firstPosition;
        Vector3 lastPosition = m_parameter.lastPosition;
        GameObject gameObject = m_parameter.gameObject;
        Transform transform = gameObject.transform;
        while (m_lerpT < 1.0f)
        {
            m_lerpT += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(
                firstPosition, lastPosition, m_lerpT);
            yield return waitForSeconds;
        }


        m_equals = true;
    }

    virtual public bool Enable()
    {
        return m_equals;
    }

}

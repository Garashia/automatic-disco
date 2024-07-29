using System.Collections;
using TMPro;
using UnityEngine;

public class TextCommand : ICommand
{
    private Parameter m_parameter;
    private bool m_equals = false;
    private bool m_once = false;

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
        if (m_once == false)
        {
            Debug.Log(m_parameter);
            CoroutineHandler.StartStaticCoroutine(Enumerator());
            m_once = true;
        }

    }

    virtual public IEnumerator Enumerator()
    {
        GameObject gameObject = m_parameter.gameObject;
        TextBox textBox = gameObject.GetComponent<TextBox>();
        if (textBox == null) { yield break; }

        TextMeshProUGUI textMeshProUGUI = textBox.Text;
        textMeshProUGUI.text = "";
        string word = m_parameter.word;
        foreach (var i in word)
        {
            textMeshProUGUI.text += i;
            WaitForSeconds waitForSeconds = new WaitForSeconds(Time.deltaTime);
            // Debug.Log(100);
            yield return waitForSeconds;
            waitForSeconds = null;

        }
        m_equals = true;
    }

    virtual public bool Enable()
    {
        return m_equals;
    }
}

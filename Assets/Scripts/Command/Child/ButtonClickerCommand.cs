using UnityEngine;
using UnityEngine.UI;

public class ButtonClickerCommand : ICommand
{
    private Parameter m_parameter;
    private bool m_equals = false;

    // Start is called before the first frame update
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
        GameObject gameObject = m_parameter.gameObject;
        gameObject.GetComponent<Button>().interactable = true;
        m_equals = true;
    }

    virtual public bool Enable()
    {
        return m_equals;
    }
}

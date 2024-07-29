using UnityEngine;

public class DestroyCommand : ICommand
{
    private Parameter m_parameter;
    private bool m_equals = false;

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
        Object.Destroy(m_parameter.gameObject);
        m_equals = true;
    }
    virtual public bool Enable()
    {
        return m_equals;

    }
}

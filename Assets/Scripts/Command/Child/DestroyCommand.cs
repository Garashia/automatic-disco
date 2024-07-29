using UnityEngine;

public class DestroyCommand : ICommand
{
    private Parameter m_parameter;
    private bool m_equals = false;

    // �p�����[�^���擾����
    virtual public Parameter GetParameter()
    {
        return m_parameter;
    }
    // �p�����[�^��ݒ肷��
    virtual public void SetParameter(Parameter parameter)
    {
        m_parameter = parameter;
    }
    // ���s����
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

using System.Collections;
using UnityEngine;

public class WaitCommand : ICommand
{
    private Parameter m_parameter;
    private bool m_equals = false;
    private bool m_once = false;
    private WaitForSeconds waitForSeconds;

    // Start is called before the first frame update
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
        if (!m_once)
        {
            waitForSeconds = new WaitForSeconds(m_parameter.time);

            CoroutineHandler.StartStaticCoroutine(Enumerator());
            m_once = true;
        }

    }

    private IEnumerator Enumerator()
    {
        yield return waitForSeconds;
        waitForSeconds = null;
        m_equals = true;
    }

    virtual public bool Enable()
    {
        return m_equals;
    }
}

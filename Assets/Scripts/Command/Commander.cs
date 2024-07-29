public class Commander
{
    private ICommand m_command = null;
    private Parameter m_parameter;
    private bool m_enabled = false;
    private string m_message = "";

    //// デリゲート
    //public delegate void Actor(string message);
    //private Actor m_actor;
    //public Actor Action
    //{
    //    set { m_actor = value; }
    //}
    public string Message
    {
        set { m_message = value; }
    }

    // コマンドを取得する
    public ICommand GetCommand()
    {
        return m_command;
    }
    // コマンドを設定する
    public void SetCommand(ICommand command)
    {
        m_command = command;
    }

    // パラメータを取得する
    public Parameter GetParameter() { return m_parameter; }
    // パラメータを設定する
    public void SetParameter(Parameter parameter) { m_parameter = parameter; }

    virtual public bool Execute()
    {

        bool isExecute = Execute(ref m_enabled);
        if (isExecute == false)
        {
            BattleObserver.Observer.AEOCommander(m_message);
            // m_actor?.Invoke(m_message);
        }

        return isExecute;
    }

    private bool Execute(ref bool flag)
    {
        if (GetCommand() == null) return false;
        if (!flag)
        {
            m_command.Execute();
            flag = true;
        }
        if (m_command.Enable())
        {
            m_command = null;
            flag = false;
        }
        return true;
    }

}

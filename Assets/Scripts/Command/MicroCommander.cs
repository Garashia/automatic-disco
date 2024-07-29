using System.Collections.Generic;

public class MicroCommander : Commander
{
    private Queue<ICommand> m_microCommand = new Queue<ICommand>();

    // マクロコマンド数を取得する
    int GetMacroCommandNumber() { return m_microCommand.Count; }

    public void AddCommand(ICommand command, Parameter parameter)
    {
        command.SetParameter(parameter);
        m_microCommand.Enqueue(command);
    }

    public override bool Execute()
    {
        int index = GetMacroCommandNumber();
        if (GetCommand() == null && index != 0)
        {
            SetCommand(m_microCommand.Dequeue());
        }
        return base.Execute();
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
public enum IsCorrect
{
    None = 2,
    Some = 1,
    All = 0
}

public class Correct
{

    private string[] m_correctWord = new string[4];
    public string[] correctWord
    {
        get { return m_correctWord; }
    }

    private IsCorrect[] isCorrects = new IsCorrect[4];
    public void SetCorrect(List<string> word)
    {
        word = word.OrderBy(a => Guid.NewGuid()).ToList();
        for (int i = 0; i < 4; ++i)
        {
            m_correctWord[i] = word[i];
        }
    }

    public IsCorrect[] IsCorrected(List<string> word)
    {
        for (int i = 0; i < 4; ++i)
        {
            isCorrects[i] = IsCorrect.None;
            for (int j = 0; j < 4; ++j)
            {
                if (word[i] == m_correctWord[j])
                {
                    if (i == j)
                    {
                        isCorrects[i] = IsCorrect.All;
                    }
                    else
                    {
                        isCorrects[i] = IsCorrect.Some;
                    }
                    break;
                }
            }
        }
        return isCorrects;
    }

}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindEnemy
{

    static private PlayerManager m_playerManager;

    static public void Walking(PlayerManager playerManager)
    {
        if (Bernoulli(0.12f))
        {
            m_playerManager = playerManager;
            m_playerManager.Flag = false;
            // Debug.Log(999);
            SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
        }
    }

    static public void EndBattle()
    {
        if (m_playerManager == null) return;
        m_playerManager.Flag = true;
        CoroutineHandler.StartStaticCoroutine(CoUnload());
        m_playerManager = null;
    }
    static public PlayerDo GetPlayerLists()
    {
        if (m_playerManager == null) return null;
        return m_playerManager.PlayerDoes;
    }
    static IEnumerator CoUnload()
    {
        //SceneAをアンロード
        var op = SceneManager.UnloadSceneAsync("Battle");
        yield return op;

        //アンロード後の処理を書く


        //必要に応じて不使用アセットをアンロードしてメモリを解放する
        //けっこう重い処理なので、別に管理するのも手
        yield return Resources.UnloadUnusedAssets();
    }
    private static bool Bernoulli(float p)
    {
        var random = new System.Random();
        float bernoulli = random.Next() % 100;
        // Debug.Log(bernoulli);
        return (bernoulli * 0.01f) < p;
    }
}

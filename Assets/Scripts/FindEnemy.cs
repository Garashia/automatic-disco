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
        //SceneA���A�����[�h
        var op = SceneManager.UnloadSceneAsync("Battle");
        yield return op;

        //�A�����[�h��̏���������


        //�K�v�ɉ����ĕs�g�p�A�Z�b�g���A�����[�h���ă��������������
        //���������d�������Ȃ̂ŁA�ʂɊǗ�����̂���
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

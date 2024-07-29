using PseudoRandom;
using System;

public class DamageCalculation
{
    public static int Damage(float playerAttack, float enemyDefense, IsCorrect isCorrect)
    {
        MersenneTwister mersenneTwister = new MersenneTwister();
        ulong[] init = new ulong[] { 0x1E3, 0xA34, 0x34D, 0xFFF };  // 必要なら種を設定する。コンストラクタで与えることも可能。ulong 一個の種も可能。
                                                                    //      mersenneTwister.init_by_array(init);                        // 〃
        double dRandomNumber = mersenneTwister.genrand_real2();     // [0,1) 区間で擬似乱数を得る。
        dRandomNumber -= 0.5f;
        dRandomNumber *= 0.2f;
        dRandomNumber += 1.0f;
        float magnification = (float)(3 - (int)isCorrect);
        magnification -= 2.0f;
        magnification *= 1.3f;
        magnification += 3.0f;
        float damage = (playerAttack * magnification) / (enemyDefense + 0.0001f);
        damage *= magnification;
        return Math.Max((int)damage, 1);
    }
}

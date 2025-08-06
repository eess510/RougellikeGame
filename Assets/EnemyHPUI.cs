using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPUI : MonoBehaviour
{
    [SerializeField]
    Guage m_hpBar;

    public void OnSpawnEnemy(MoveToTarget enemy)
    {
        enemy.ChangedHPEvent.AddListener(OnChangedEnemyHP);

    }
    void OnChangedEnemyHP(int hp, int maxhp)
    {
        print($"체력변화 {hp} {maxhp}");
        m_hpBar.SetGuage((float)hp/maxhp);
        m_hpBar.SetLable($"{hp} / {maxhp}");
    }
}

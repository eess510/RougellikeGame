using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPUI : MonoBehaviour
{
    [SerializeField]
    Guage m_hpBar;


    public void OnPlayer(Player player)
    {
        Debug.Log(" PlayerHPUI.OnPlayer »£√‚µ ");
        player.PlayerChangedHpEvent.AddListener(OnChangedPlayerHP);

        //player.ChangedHPEvent.AddListener(OnChangedEnemyHP);
        OnChangedPlayerHP(player.m_hp, player.m_maxHP);

    }

    void OnChangedPlayerHP(int hp, int maxHP)
    {
        m_hpBar.SetGuage((float)hp/maxHP);
        m_hpBar.SetLable($"{hp} / {maxHP }");
    }
  
}

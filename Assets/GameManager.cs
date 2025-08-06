using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameOverController m_gameOverController;

    [SerializeField]
    PlayTimeController m_playTimeController;

    [SerializeField]
    MoveToTarget m_spawnedenemy;

    [SerializeField]
    Spawner m_spawner;

    [SerializeField]
    Player m_player;

    [SerializeField]
    PlayerHPUI m_playerHPUI;


    private void Start()
    {
        m_playTimeController.StarTime();

        if (m_player == null)
        {
            m_player = FindFirstObjectByType<Player>();
        }

        if (m_playerHPUI != null && m_player != null)
        {
            Debug.Log("GameManager에서 PlayerHPUI 연결");
            m_playerHPUI.OnPlayer(m_player); // 내부에서 이벤트 연결됨
        }
    }


    public void OnDead()
    {
        m_gameOverController.GameOver();
    }
    public void ReStartGame() { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void OnSpwanEnemy(MoveToTarget enemy)
    {
        print("몹 스폰");
       // m_spawnedenemy = enemy;
        m_spawnedenemy.KilledEvent.AddListener(OnKillEnemy);

    }
    void OnKillEnemy(MoveToTarget enemy){
        m_player.AddExp(enemy.GetExpReward());

    }
    
    

}


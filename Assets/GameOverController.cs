using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverController : MonoBehaviour
{
    public struct GameOverInfo
    {
        public float playTime;
    }
    [SerializeField]
    PlayTimeController m_playTimeController;

    [SerializeField]
    UnityEvent<GameOverInfo> m_gameOverEvent;


    [ContextMenu(nameof(GameOver))]
    public void GameOver()
    {
        StartCoroutine(nameof(CoGameOver));
    }
    IEnumerator CoGameOver()
    {
        yield return new WaitForSeconds(0.5f);

        GameOverInfo gameOverInfo;
        gameOverInfo.playTime = m_playTimeController.PlayTime;
        Debug.Log($"Invoking gameOverEvent with playTime: {gameOverInfo.playTime}");

        m_gameOverEvent.Invoke(gameOverInfo);

    }
   

}

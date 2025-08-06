using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI m_txtPlayTime;

    public void OnGameOver(GameOverController.GameOverInfo v)
    {
        m_txtPlayTime.text = $"{(int)(v.playTime / 60):00}:{(int)(v.playTime % 60):00}";
        gameObject.SetActive(true);
    }
}

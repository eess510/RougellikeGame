using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTimeUI : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI m_txtPlayTime;

    public void OnChangedPlayTime(float t)
    {
        m_txtPlayTime.text = $"{(int)(t/60) : 00} : {(int)(t%60):00}";
    }
}

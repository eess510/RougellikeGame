using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExpUI : MonoBehaviour
{
    [SerializeField]
    Image m_imgbgLevel;
   
    [SerializeField]
    Image m_imgLevel;

    [SerializeField] 
    TextMeshProUGUI m_txtLevel;

    public void SetExp(int currentExp, int expToNextLevel, int level)
    {
        if (m_imgLevel != null)
        {
            m_imgLevel.fillAmount = (float)currentExp / expToNextLevel;
        }

        if (m_txtLevel != null)
        {
            m_txtLevel.text = $"Lv. {level}";
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guage : MonoBehaviour
{

    [SerializeField]
    Image m_redBar;

    [SerializeField]
    Image m_greenbar;

    [SerializeField]
    TMPro.TextMeshProUGUI m_txt;
    
  

    private void Update()
    {
        if(m_greenbar != null)
        {
            float v = Mathf.Lerp(m_greenbar.fillAmount, m_redBar.fillAmount, Time.deltaTime);
            m_greenbar.fillAmount = v;
        }
    }
    public void SetGuage(float v)
    {
        v = Mathf.Clamp01(v);

        if(m_redBar != null) 
            m_redBar.fillAmount = v;


    }
    public void SetLable(string v)
    {
        if(m_txt != null)
            m_txt.text = v;
    }
   
}

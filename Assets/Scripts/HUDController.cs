using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    [SerializeField]
    private Text m_titleText;
    [SerializeField]
    private Text m_scoreText;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////

    [SerializeField]
    GameObject m_button;


    private void Awake()
    {
        EventBroker.HUDController += UpdateUI;
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        EventBroker.WinController += WinRoutine;

    }



    private void UpdateUI(string title, string score)
    {
        m_titleText.text = title;
        m_scoreText.text = "ATTEMPTS: " + score;

    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void WinRoutine()
    {

        m_button.gameObject.SetActive(true);
        m_button.transform.localScale = Vector3.zero;
        LeanTween.scale(m_button, Vector3.one, 0.3f);

    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnDestroy()
    {
        EventBroker.HUDController -= UpdateUI;
        EventBroker.WinController -= WinRoutine;
    }


}

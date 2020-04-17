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


    private void Awake()
    {
        EventBroker.HUDController += UpdateUI;
    }



    private void UpdateUI(string title, string score)
    {
        m_titleText.text = title;
        m_scoreText.text = "ATTEMPTS: " + score;

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Solution
{

    public class HUDController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_titleText;

        [SerializeField]
        private TextMeshProUGUI m_scoreText;
        [SerializeField]
        private string m_title = "HI LETS PLAY MEMO";
        [SerializeField]
        private string m_score = "NUMBER OF MOVES";


        private void Awake()
        {
            EventBroker.HUDHandler += UpdateUI;
            m_titleText.text = m_title;
            m_scoreText.text = m_score;

        }


        private void UpdateUI(string title, string score)
        {
            m_titleText.text = title;
            m_scoreText.text = m_score + " " + score;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool m_isGameFinished = false;

    private int m_numberOfAttempts = 0;
    public int puzzleSize;

    [SerializeField]
    private Transform m_field;

    [SerializeField]
    private GameObject m_cardPrefab;

    public Sprite[] cardImages;

    private List<Card> m_cardsInGame = new List<Card>();

    private List<Card> m_stackToCompare = new List<Card>();

    private bool m_isStackFull = false;

    private string m_courseName = string.Empty;

    private void Awake()
    {
        IntializeGame();
    }

    private void Start()
    {
        //Rnd our positions
    }



    private void IntializeGame()
    {
        cardImages = Resources.LoadAll<Sprite>("Sprites/photos");
        puzzleSize = cardImages.Length;

        int identifier = 0;
        for (int i = 0; i < puzzleSize; i++)
        {
            if (i % 2 == 0) identifier++;
            GameObject card = Instantiate(m_cardPrefab);
            Card c = card.GetComponent<Card>();
            c.Initialize(cardImages[i].name, cardImages[i], i, identifier);
            card.transform.SetParent(m_field, false);
            m_cardsInGame.Add(c);

        }
        foreach (Card card in m_cardsInGame)
        {
            var rnd = Random.Range(0, 7);
            card.gameObject.transform.SetSiblingIndex(rnd);
        }
    }


    public void AddCardToStack(Card card)
    {
        if (!m_isStackFull)
        {
            m_stackToCompare.Add(card);
        }
        m_isStackFull = m_stackToCompare.Count == 2;
        Compare();
    }

    public void RemoveFromStack(Card card)
    {
        m_isStackFull = m_stackToCompare.Count < 1;
        if (m_stackToCompare.Contains(card))
        {
            m_stackToCompare.Remove(card);
        }

    }

    private void Compare()
    {
        if (m_isStackFull)
        {
            m_numberOfAttempts++;
            bool guessed = m_stackToCompare[0].m_pairKey == m_stackToCompare[1].m_pairKey ? true : false;

            m_courseName = guessed ? "THE COURSE WAS " + m_stackToCompare[0].m_name : "TRY AGAIN :(";

            MessageToDisplay(guessed, m_courseName);
            ClearStack(guessed);
            m_courseName = string.Empty;
        }
    }

    private void ClearStack(bool guessed)
    {
        foreach (Card card in m_stackToCompare)
        {
            if (guessed)
            {
                card.GuessedCard();
                m_cardsInGame.Remove(card);

            }
            else
            {
                card.RestartCard();
            }

        }
        m_stackToCompare.Clear();
        m_isStackFull = false;
        m_isGameFinished = m_cardsInGame.Count == 0;

        FinishGame();
    }

    private void FinishGame()
    {

        if (m_isGameFinished)
        {
            EventBroker.CallWinController();
        }

    }

    public void RestartGame()
    {
        LevelLoader.ReloadLevel();
    }



    private void MessageToDisplay(bool guessed, string message)
    {

        EventBroker.CallHUDController(message, m_numberOfAttempts.ToString());
    }

}

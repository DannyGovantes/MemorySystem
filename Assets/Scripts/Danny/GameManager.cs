using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solution
{

    public class GameManager : MonoBehaviour
    {

        [SerializeField]
        private int m_numberOfAttempts = 0;

        [SerializeField]
        private Transform m_board;

        [SerializeField]
        private GameObject m_cardPrefab;

        [SerializeField]
        private int m_puzzleSize;

        public Sprite[] cardImages;

        private List<Card> m_cardsInGame = new List<Card>();

        private List<Card> m_stackToCompare = new List<Card>();

        private bool m_isStackFull = false;




        private void Awake()
        {
            InitializeGame();

        }

        private void InitializeGame()
        {

            cardImages = Resources.LoadAll<Sprite>("Sprites/photos");
            m_puzzleSize = cardImages.Length;
            int identifier = 0;
            for (int i = 0; i < m_puzzleSize; i++)
            {
                if (i % 2 == 0) identifier++;
                GameObject card = Instantiate(m_cardPrefab);
                Card c = card.GetComponent<Card>();
                c.Initialize(cardImages[i].name, cardImages[i], i, identifier);
                card.transform.SetParent(m_board, false);
                m_cardsInGame.Add(c);
            }
        }

        public void AddToCompare(Card card)
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

        public bool IsStackFull()
        {
            return m_isStackFull;
        }
        private void Compare()
        {

            if (m_isStackFull)
            {
                m_numberOfAttempts++;
                bool guessed = m_stackToCompare[0].m_pairKey == m_stackToCompare[1].m_pairKey ? true : false;
                MessageToDisplay(guessed);
                ClearStack(guessed);


            }

        }

        private void ClearStack(bool guessed)
        {
            foreach (Card card in m_stackToCompare)
            {
                if (guessed)
                {
                    card.GuessedCard();
                }
                else
                {
                    card.RestartCard();
                }

            }
            m_stackToCompare.Clear();
            m_isStackFull = false;
        }

        private void MessageToDisplay(bool guessed)
        {
            string message = guessed ? "CONGRATS" : "TRY AGAIN :(";
            EventBroker.CallHUDController(message, m_numberOfAttempts.ToString());
        }

    }
}

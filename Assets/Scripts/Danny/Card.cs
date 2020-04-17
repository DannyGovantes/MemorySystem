using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
namespace Solution
{

    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {


        [Header("Animation Parameters")]
        public Vector3 selectedAnimationSize = new Vector3(1.1f, 1.1f, 1.1f);
        public Vector3 normalAnimationSize = new Vector3(1f, 1f, 1f);

        public Vector3 guessedAnimationSize = new Vector3(0.9f, 0.9f, 0.9f);

        [SerializeField]
        protected Image m_image;

        [SerializeField]
        protected Sprite m_backImage;
        protected string m_name { get; set; }
        [SerializeField]
        protected Sprite m_sprite;
        [SerializeField]
        protected int m_id = 0;
        [SerializeField]
        protected bool m_isSelected = false;
        [SerializeField]
        public int m_pairKey = 0;

        [SerializeField]
        protected bool m_isCardInGame = true;

        [SerializeField]
        Color32 m_idleColor = new Color32();
        [SerializeField]
        Color32 m_enterColor = new Color32();
        [SerializeField]
        Color32 m_selectedColor = new Color32();

        private GameManager gameManager;
        private Image m_selectedImage;

        public void Initialize(string name, Sprite sprite, int id, int pairKey)
        {
            m_name = name;
            m_sprite = sprite;
            m_id = id;
            m_pairKey = pairKey;
            m_image.sprite = m_backImage;
            gameManager = FindObjectOfType<GameManager>();
            m_selectedImage = GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {

            if (m_isCardInGame)
            {
                m_isSelected = !m_isSelected;
                m_selectedImage.color = m_selectedColor;
                if (m_isSelected)
                {
                    m_image.sprite = m_sprite;
                    gameManager.AddToCompare(this);
                }
                else
                {
                    m_image.sprite = m_backImage;
                    gameManager.RemoveFromStack(this);
                }

            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_isCardInGame)
            {
                //
                LeanTween.scale(gameObject, selectedAnimationSize, 0.1f);

                m_selectedImage.color = m_isSelected ? m_selectedColor : m_enterColor;
            }


        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_isCardInGame)
            {
                //
                LeanTween.scale(gameObject, normalAnimationSize, 0.1f);

                if (!m_isSelected) m_selectedImage.color = m_idleColor;

            }

        }

        public void RestartCard()
        {

            StartCoroutine(RestarCardRoutine());

        }


        private IEnumerator RestarCardRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            //
            LeanTween.scale(gameObject, normalAnimationSize, 0.3f);
            m_image.sprite = m_backImage;
            m_selectedImage.color = m_idleColor;
            m_isSelected = false;
        }


        public void GuessedCard()
        {
            //
            LeanTween.scale(gameObject, guessedAnimationSize, 0.1f);
            m_backImage = null;
            m_image = null;
            m_isSelected = false;
            m_isCardInGame = false;


        }

    }
}

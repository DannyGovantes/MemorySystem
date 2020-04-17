using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    protected Image m_image;

    [SerializeField]
    protected Sprite m_backgroundImage;
    public string m_name = string.Empty;

    [SerializeField]
    protected Sprite m_sprite;

    [SerializeField]
    protected int m_id = 0;

    [SerializeField]
    protected bool m_isSelected = false;

    // [SerializeField]
    public int m_pairKey;

    [SerializeField]
    protected bool m_isCardInGame = true;

    private GameManager m_gameManager;

    [SerializeField]
    private Color32 m_idleColor = new Color32();
    [SerializeField]
    private Color32 m_enterColor = new Color32();
    [SerializeField]
    private Color32 m_selectedColor = new Color32();

    [SerializeField]
    private Image m_selectedImage;

    public void Initialize(string name, Sprite sprite, int id, int pairKey)
    {
        m_name = name;
        m_sprite = sprite;
        m_id = id;
        m_pairKey = pairKey;
        m_image.sprite = m_backgroundImage;
        m_gameManager = FindObjectOfType<GameManager>();
        m_selectedImage = GetComponent<Image>();
    }



    public void OnPointerClick(PointerEventData data)
    {
        if (m_isCardInGame)
        {
            m_isSelected = !m_isSelected;
            m_selectedImage.color = m_selectedColor;
            if (m_isSelected)
            {
                m_image.sprite = m_sprite;
                m_gameManager.AddCardToStack(this);
            }
            else
            {
                //flip the image to its original state
                m_image.sprite = m_backgroundImage;
                m_gameManager.RemoveFromStack(this);
            }
        }

    }

    public void OnPointerEnter(PointerEventData data)
    {

        if (m_isCardInGame)
        {
            //Animation
            m_selectedImage.color = m_isSelected ? m_selectedColor : m_enterColor;
        }

    }

    public void OnPointerExit(PointerEventData data)
    {
        if (m_isCardInGame)
        {
            if (!m_isSelected) m_selectedImage.color = m_idleColor;
        }

    }


    public void RestartCard()
    {
        StartCoroutine(RestartCardRoutine());
    }


    private IEnumerator RestartCardRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        //animation
        m_image.sprite = m_backgroundImage;
        m_selectedImage.color = m_idleColor;
        m_isSelected = false;
    }


    public void GuessedCard()
    {
        //Animation
        m_backgroundImage = null;
        m_image = null;
        m_isSelected = false;
        m_isCardInGame = false;
    }

}

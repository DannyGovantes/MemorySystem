using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private Sprite bgImage;

    public Sprite[] puzzles;
   
    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> btns = new List<Button>() ;

    private bool firstGuess,secondGuess;

    private int CountGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;



    void Awake() {
        //for (int i = 0; i < 8; i++) {
           // if (i < 2) { puzzles[i] = Resources.LoadAll<Sprite>("Sprites/DB"); }
          //  if (i < 4) { puzzles[i] = Resources.LoadAll<Sprite>("Sprites/Gr"); }
         //  if (i < 6) { puzzles[i] = Resources.LoadAll<Sprite>("Sprites/OS"); }
        //    if (i < 8) { puzzles[i] = Resources.LoadAll<Sprite>("Sprites/Pr"); }
       //}
            puzzles = Resources.LoadAll<Sprite>("Sprites/photos");

    }

    void Start() {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++) {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;

        }
    }

    void AddListeners() {
        foreach (Button btn in btns) {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    void AddGamePuzzles() {
        int looper = btns.Count;
        int index = 0;
        for (int i = 0; i < looper; i++) {

            gamePuzzles.Add(puzzles[index]);

            index++;

        }
    }

    public void PickAPuzzle() {

        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        
        Debug.Log("You Are Clicking A Button name "+name);

        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
           

            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            

            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if (!secondGuess) 
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            if (firstGuessPuzzle == secondGuessPuzzle)
            {
                Debug.Log("The Puzzles Match");
            }
            else {
                Debug.Log("The Puzzles dont Match");
            }
        }
    }

}

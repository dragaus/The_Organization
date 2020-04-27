using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    TutorialUI tutorialUI;

    [SerializeField]
    AIPlayer cleaner = null;
    [SerializeField]
    AIPlayer agent = null;
    [SerializeField]
    AIReporter reporter = null;

    [SerializeField]
    InteractableObject ufo = null;
    [SerializeField]


    const string tutorialTextAssetDir = "Unique/Tutorial";
    const string staticStringDir = "Scenes/Tutorial";

    List<string> tutorialStrings;

    int tutorialIndex = 0;

    UnityAction tutorialAction;
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        tutorialStrings = TextManager.TextsOfAsset(tutorialTextAssetDir);
        tutorialUI.SetStaticTexts(TextManager.TextsOfAsset(staticStringDir));
        SetNewText();
        tutorialUI.okBtn.onClick.AddListener(UnderstandTutorial);
        manager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        tutorialAction();
    }

    void SetNewText()
    {
        var stripedStrings = tutorialStrings[tutorialIndex].Split('_');
        tutorialUI.tutorialInstruction.text = stripedStrings[0];
        ChangeTutorialAction(int.Parse(stripedStrings[1]));
    }

    void UnderstandTutorial()
    {
        if (++tutorialIndex >= tutorialStrings.Count)
        {
            tutorialUI.tutorialPanel.SetActive(false);
        }
        else
        {
            SetNewText();
        }
    }

    void ChangeTutorialAction(int numberOfAction)
    {
        tutorialUI.okBtn.onClick.RemoveAllListeners();
        tutorialUI.okBtn.gameObject.SetActive(false);
        switch (numberOfAction)
        {
            case 1:
                tutorialAction = MoveCamera;
                break;
            case 2:
                tutorialAction = MoveScrollWheel;
                break;
            case 3:
                tutorialAction = () => SelectACharacter(cleaner);
                break;
            case 4:
                tutorialAction = () => SelectAnObject(ufo);
                break;
            case 5:
                tutorialAction = RightClick;
                break;
            case 6:
                tutorialAction = () => SelectACharacter(agent);
                break;
            case 7:
                reporter.FreeTheReporter();
                tutorialAction = TrapReporter;
                break;
            case 8:
                tutorialAction = FinishCleaning;
                break;
            case 9:
                tutorialAction = EnterToGoToBase;
                tutorialUI.okBtn.onClick.AddListener(GoBackBase);
                tutorialUI.okBtn.gameObject.SetActive(true);
                break;
            default:
                tutorialAction = EnterNextTutorial;
                tutorialUI.okBtn.onClick.AddListener(NextTutorial);
                tutorialUI.okBtn.gameObject.SetActive(true);
                break;
        }
    }

    void EnterNextTutorial()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextTutorial();
        }
    }

    void NextTutorial()
    {
        tutorialIndex++;
        SetNewText();
    }

    void MoveCamera()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)||
            Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) 
            || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextTutorial();
        }
    }

    void MoveScrollWheel()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            NextTutorial();
        }
    }

    void SelectACharacter(AIPlayer character)
    {
        while (!character.isSelected)
        {
            return;
        }
        NextTutorial();
    }

    void SelectAnObject(InteractableObject obj)
    {
        while (!obj.IsSelected())
        {
            return;
        }
        NextTutorial();
    }

    void RightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            NextTutorial();
        }
    }

    void TrapReporter()
    {
        if (reporter.IsReporterTrap())
        {
            NextTutorial();
        }
    }

    public bool AlienShipCanBeDestroyed()
    {
        return tutorialIndex > 15;
    }

    void FinishCleaning()
    {
        while (!manager.isGameOver) return;
        NextTutorial();
    }

    void EnterToGoToBase()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GoBackBase();
        }
    }

    void GoBackBase()
    {
        LoaderManager.LoadScene(OfficeManager.sceneName);
    }
}

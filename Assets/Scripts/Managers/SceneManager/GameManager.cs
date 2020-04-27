using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    int evidenceToRecolect = 0;
    int evidenceRecolected;

    //const float awarenessTime = 0.01f;
    const float awarenessTime = 0.1f;

    bool isSomethingSelected = false;
    [HideInInspector]public bool isGameOver = false;

    public const string sceneName = "Level_";
    const string gameStaticStrings = "Scenes/Game";

    AIPlayer selectedPlayer;

    [SerializeField]
    GameObject[] boxes = null;

    [SerializeField]
    GameUI gameUI;
    string loseString;
    string winString;

    // Start is called before the first frame update
    void Start()
    {
        var interactables = FindObjectsOfType<InteractableObject>();
        foreach (var interactable in interactables)
        {
            evidenceToRecolect += interactable.AmountOfBoxes();
        }

        gameUI.Initialization();
        gameUI.ShowPanel(!FindObjectOfType<TutorialManager>());
        gameUI.SetStaticTexts(TextManager.TextsOfAsset(gameStaticStrings));
        gameUI.UpdateEvidence(evidenceRecolected, evidenceToRecolect);
        gameUI.UpdateAwarenessBar(GameSettingsManager.awerenessLevel);
        gameUI.winBtn.onClick.AddListener(GoToOffice);
        gameUI.loseBtn.onClick.AddListener(GoToMenu);
    }

    void FixedUpdate()
    {
        isSomethingSelected = selectedPlayer != null;   
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return;
        if (isSomethingSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.gameObject.GetComponent<InteractableObject>())
                    {
                        var interactable = hit.transform.gameObject.GetComponent<InteractableObject>();
                        if (selectedPlayer is AICleaner && selectedPlayer)
                        {
                            var aiCleaner = selectedPlayer.GetComponent<AICleaner>();
                            if (!aiCleaner.isHoldingSomething)
                            { 
                                aiCleaner.SetObjectToInteract(interactable);
                            }
                        }
                        selectedPlayer.MoveAgentToNewPos(interactable.transform.position);
                    }
                    else if (hit.transform.gameObject.GetComponent<GrabableObjects>())
                    {
                        var grabable = hit.transform.gameObject.GetComponent<GrabableObjects>();
                        if (selectedPlayer is AICleaner && selectedPlayer)
                        {
                            var aiCleaner = selectedPlayer.GetComponent<AICleaner>();
                            if (!aiCleaner.isHoldingSomething)
                            {
                                aiCleaner.SetObjectToGrab(grabable);
                            }
                        }
                        selectedPlayer.MoveAgentToNewPos(grabable.transform.position);
                    }
                    else if (hit.transform.gameObject.GetComponent<StorageObject>())
                    {
                        var storage = hit.transform.gameObject.GetComponent<StorageObject>();
                        if (selectedPlayer is AICleaner && selectedPlayer)
                        {
                            var aiCleaner = selectedPlayer.GetComponent<AICleaner>();
                            if (aiCleaner.isHoldingSomething)
                            {
                                aiCleaner.GoToTruck(storage);
                            }
                        }
                        selectedPlayer.MoveAgentToNewPos(storage.transform.position);
                    }
                    else
                    {
                        var hitPos = hit.point;
                        selectedPlayer.MoveAgentToNewPos(hitPos);
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                selectedPlayer.DeselectPlayer();
                selectedPlayer = null;
            }
        }
    }

    public void SelectPlayer(AIPlayer newAISelected)
    {
        if (selectedPlayer != null)
        {
            selectedPlayer.DeselectPlayer();
        }

        selectedPlayer = newAISelected;
        isSomethingSelected = false;
    }

    public void DestroyAnInteractable(Transform[] boxSpawnPoints, GameObject interactable)
    {
        foreach (var point in boxSpawnPoints)
        {
            Instantiate(boxes[Random.Range(0, boxes.Length)], point.position, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
        }
        Destroy(interactable);
    }

    public void GetEvidence()
    {
        evidenceRecolected++;
        gameUI.UpdateEvidence(evidenceRecolected, evidenceToRecolect);
        if (evidenceRecolected >= evidenceToRecolect)
        {
            isGameOver = true;
            if (!FindObjectOfType<TutorialManager>())
            {
                gameUI.winPanel.SetActive(true);
            }
        }
    }

    public void UpdateAwareness()
    {
        if (isGameOver) return;
        GameSettingsManager.awerenessLevel += Time.deltaTime * awarenessTime;
        gameUI.UpdateAwarenessBar(GameSettingsManager.awerenessLevel);
        if (GameSettingsManager.awerenessLevel >= 1f)
        {
            isGameOver = true;
            gameUI.losePanel.SetActive(true);
        }
    }

    void GoToOffice()
    {
        LoaderManager.LoadScene(OfficeManager.sceneName);
    }

    void GoToMenu()
    {
        LoaderManager.LoadScene(MenuManager.sceneName);
    }
}

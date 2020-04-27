using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    int evidenceToRecolect = 0;
    int evidenceRecolected;
    public float time;

    bool isGameStart = false;
    bool isSomethingSelected = false;
    public bool isGameOver = false;

    public const string sceneName = "Level_";

    AIPlayer selectedPlayer;

    [SerializeField]
    GameObject[] boxes = null;

    // Start is called before the first frame update
    void Start()
    {
        var interactables = FindObjectsOfType<InteractableObject>();
        foreach (var interactable in interactables)
        {
            evidenceToRecolect += interactable.AmountOfBoxes();
        }
        Debug.Log($"Evidence to grab is {evidenceToRecolect}");
    }

    void FixedUpdate()
    {
        isSomethingSelected = selectedPlayer != null;   
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStart)
        {
            time -= Time.deltaTime;
        }

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
        if (evidenceRecolected >= evidenceToRecolect)
        {
            isGameOver = true;
            if (!FindObjectOfType<TutorialManager>())
            {
                Debug.Log("End Game");
            }
        }
    }
}

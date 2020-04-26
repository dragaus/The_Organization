using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public int task;
    public float time;

    bool isGameStart = false;
    bool isSomethingSelected = false;

    public const string sceneName = "Level_";

    AIPlayer selectedPlayer;
    NavMeshSurface surface;

    // Start is called before the first frame update
    void Start()
    {
        surface = FindObjectOfType<NavMeshSurface>();
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
                        if (selectedPlayer is AICleaner)
                        {
                            var aiCleaner = selectedPlayer.GetComponent<AICleaner>();
                            aiCleaner.SetObjectToInteract(interactable);
                        }
                        selectedPlayer.MoveAgentToNewPos(interactable.transform.position);
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

    void RebakeNaveMesh()
    {
        surface.BuildNavMesh();
    }
}

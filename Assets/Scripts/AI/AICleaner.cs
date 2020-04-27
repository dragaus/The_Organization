using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICleaner : AIPlayer
{

    InteractableObject objectToInteract;
    void Start()
    {
        Initialization();
    }

    void Update()
    {
        EndMovement();
        Clean();
    }

    private void OnMouseEnter()
    {
        HighlightAPlayer();
    }

    private void OnMouseExit()
    {
        HideOutline();
    }

    private void OnMouseDown()
    {
        SelectAPlayer();
    }

    public void SetObjectToInteract(InteractableObject interactable)
    {
        objectToInteract = interactable;
    }

    public override void MoveAgentToNewPos(Vector3 newPos)
    {
        FreeThePlayer();
        base.MoveAgentToNewPos(newPos);
    }

    internal override void EndMovement()
    {
        base.EndMovement();
        if (objectToInteract != null && !isWorking && !isMoving)
        {
            objectToInteract.GetAPerson(this);
            isWorking = true;
            influenceArea.transform.localScale *= 2f;
            anim.SetBool(animIsWorking, isWorking);
        }
    }

    public void FreeThePlayer()
    {
        if (objectToInteract != null && isWorking)
        {
            objectToInteract = null;
            isWorking = false;
            anim.SetBool(animIsWorking, isWorking);
            influenceArea.transform.localScale *= 0.5f;
        }
    }

    void Clean()
    {
        if (isWorking && objectToInteract != null)
        {
            objectToInteract.GetWork();
        }
    }
}

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

    internal override void EndMovement()
    {
        base.EndMovement();
        if (objectToInteract != null && !isWorking)
        {
            objectToInteract.GetAPerson(this);
            isWorking = true;
            influenceArea.transform.localScale *= 2f;
            anim.SetBool(animIsWorking, isWorking);
        }
    }
}

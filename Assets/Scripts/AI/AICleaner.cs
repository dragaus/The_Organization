using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICleaner : AIPlayer
{

    InteractableObject objectToInteract;
    GrabableObjects objectToGrab;

    StorageObject truck;

    Transform holderPlace;

    bool isGoingToTruck;
    bool isGoingToBox;
    public bool isHoldingSomething;
    void Start()
    {
        Initialization();
    }

    void Update()
    {
        EndMovement();
        Clean();
    }

    internal override void Initialization()
    {
        base.Initialization();
        holderPlace = transform.Find("BoxHolder");
    }

    public void SetObjectToInteract(InteractableObject interactable)
    {
        objectToInteract = interactable;
    }

    public void SetObjectToGrab(GrabableObjects grabable)
    {
        objectToGrab = grabable;
        isGoingToBox = true;
    }

    public void GoToTruck(StorageObject truckToGo)
    {
        truck = truckToGo;
        isGoingToTruck = true;
    }

    public override void MoveAgentToNewPos(Vector3 newPos)
    {
        FreeThePlayer();
        base.MoveAgentToNewPos(newPos);
        isGoingToBox = false;
        isGoingToTruck = false;
    }

    internal override void EndMovement()
    {
        base.EndMovement();
        if (objectToInteract != null && !isWorking && !isMoving)
        {
            objectToInteract.GetAPerson(this);
            influenceArea.transform.localScale *= 2f;
            GetComponent<CapsuleCollider>().radius = influenceArea.transform.localScale.x / 2;
            BeginWork();
        }
        if (objectToGrab != null && !isWorking && !isMoving)
        {
            isHoldingSomething = true;
            objectToGrab.transform.position = holderPlace.position;
            objectToGrab.transform.rotation = holderPlace.rotation;
            objectToGrab.transform.parent = holderPlace;

        }
        if (isHoldingSomething && truck != null && !isMoving)
        {
            StorageObject();
        }
    }

    public void FreeThePlayer()
    {
        if (objectToInteract != null && isWorking)
        {
            objectToInteract = null;
            EndWork();
            influenceArea.transform.localScale *= 0.5f;
            GetComponent<CapsuleCollider>().radius = influenceArea.transform.localScale.x / 2;
        }
        if (objectToGrab != null && !isGoingToBox && !isHoldingSomething)
        {
            objectToGrab = null;
        }
        if (objectToGrab != null && isHoldingSomething && !isGoingToTruck)
        {
            truck = null;
        }
    }

    public void StorageObject()
    {
        if (objectToGrab != null)
        {
            truck.DepositABox(objectToGrab.gameObject);
            objectToGrab = null;
            truck = null;
            isHoldingSomething = false;
            isGoingToTruck = false;
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

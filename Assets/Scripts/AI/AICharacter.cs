using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AICharacter : MonoBehaviour
{
    internal NavMeshAgent agent;
    internal Animator anim;

    internal bool isMoving = false;
    public bool isWorking;

    internal const string animIsMoving = "IsMoving";
    internal const string animIsWorking = "IsWorking";
    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal virtual void EndMovement()
    {
        if (anim != null && isMoving)
        {
            if (agent.pathPending) return;
            bool isMovementLeft = agent.remainingDistance > 0.1f;
            if (!isMovementLeft)
            {
                anim.SetBool(animIsMoving, false);
                isMoving = false;
            }
        }
    }

    internal virtual void Initialization()
    {
        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();
    }

    public virtual void MoveAgentToNewPos(Vector3 newPos)
    {
        agent.SetDestination(newPos);
        isMoving = true;
        anim.SetBool(animIsMoving, true);
    }

    internal virtual void BeginWork()
    {
        isWorking = true;
        anim.SetBool(animIsWorking, isWorking);
    }

    internal virtual void EndWork()
    {
        isWorking = false;
        anim.SetBool(animIsWorking, isWorking);
    }
}

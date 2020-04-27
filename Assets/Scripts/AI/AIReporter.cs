using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AIReporter : AICharacter
{
    bool isSeeking;
    bool isTrapped;

    const float timeToGo = 3f;
    float timeTrapped;

    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
        isTrapped = FindObjectOfType<TutorialManager>();
        FindTheClosestCleaner();
    }

    // Update is called once per frame
    void Update()
    {
        Trapped();
        GoToTransform();
        EndMovement();
    }

    void FindTheClosestCleaner()
    {
        var cleaners = FindObjectsOfType<AICleaner>();

        if (cleaners.Length > 0)
        {
            target = cleaners.OrderBy(cleaner => (Vector3.Distance(transform.position, cleaner.transform.position))).FirstOrDefault().transform;
        }
        else
        {
            target = transform;
        }
    }

    void FindTheClosestAway()
    {
        var awayPoints = GameObject.FindGameObjectsWithTag("AwayPosition");
        if (awayPoints.Length > 0)
        {
            target = awayPoints.OrderBy(point => (Vector3.Distance(transform.position, point.transform.position))).FirstOrDefault().transform;
        }
        else
        {
            target = transform;
        }
    }


    public void FreeTheReporter()
    {
        isTrapped = false;
    }

    public bool IsReporterTrap()
    {
        return isTrapped;
    }

    public void Seeking()
    {
        isSeeking = true;
        if (isSeeking)
        {
            FindTheClosestCleaner();
            MoveAgentToNewPos(target.position);
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("Agent"))
        {
            agent.SetDestination(transform.position);
            isTrapped = true;
            isSeeking = false;
            timeTrapped = 0;
            EndWork();
        }
        else if (target.CompareTag("Cleaner"))
        {
            agent.SetDestination(transform.position);
            isSeeking = false;
            BeginWork();
        }
    }

    private void OnTriggerExit(Collider target)
    {
        if (target.CompareTag("Agent"))
        {
            isTrapped = false;
        }
        else if (target.CompareTag("Cleaner"))
        {
            EndWork();
        }
    }

    void GoToTransform()
    {
        if (isSeeking)
        {
            MoveAgentToNewPos(target.position);
        }
    }

    void Trapped()
    {
        if (isTrapped)
        {
            timeTrapped += Time.deltaTime;
            if (timeTrapped >= timeToGo)
            {
                FindTheClosestAway();
                MoveAgentToNewPos(target.position);
            }
        }
    }

    internal override void EndMovement()
    {
        base.EndMovement();
        if (!isMoving && !isTrapped && !isWorking)
        {
            Seeking();
        }
    }
}

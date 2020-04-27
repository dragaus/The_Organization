using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : AIPlayer
{
    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        EndMovement();
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("Reporter"))
        {
            BeginWork();
        }
    }

    private void OnTriggerExit(Collider target)
    {
        if (target.CompareTag("Reporter"))
        {
            EndWork();
        }
    }
}

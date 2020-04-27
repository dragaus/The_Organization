using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageObject : MonoBehaviour
{
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DepositABox(GameObject box)
    {
        Destroy(box);
        Debug.Log("Storage a box");
        manager.GetEvidence();
    }
}

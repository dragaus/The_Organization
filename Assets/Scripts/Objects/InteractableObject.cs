using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public float workingTime;
    public Transform[] boxPositions;

    const float widthOfOutline = 0.05f;
    Renderer[] renders;
    List<AIPlayer> cleanersInteracting = new List<AIPlayer>();
    void Start()
    {
        renders = GetComponentsInChildren<Renderer>();
        ChangeOutline(0);
    }

    private void OnMouseEnter()
    {
        ChangeOutline(widthOfOutline);
    }

    private void OnMouseExit()
    {
        ChangeOutline(0f);  
    }

    void ChangeOutline(float outlineWidth)
    {
        foreach (var renderer in renders)
        {
            renderer.material.SetFloat("_Outline", outlineWidth);
        }
    }

    public bool IsSelected()
    {
        Debug.Log($"Number of cleaners {cleanersInteracting.Count}");
        return cleanersInteracting.Count > 0;
    }

    public void GetAPerson(AICleaner person)
    {
        cleanersInteracting.Add(person);
    }
}

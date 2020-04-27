using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : AICharacter
{
    public bool isSelected;
    internal GameManager manager;
    internal Renderer[] meshRenderers;
    internal GameObject influenceArea;

    const float widthOfOutline = 1f;
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

    internal override void Initialization()
    {
        base.Initialization();
        manager = FindObjectOfType<GameManager>();
        meshRenderers = GetComponentsInChildren<Renderer>();
        influenceArea = GetComponentInChildren<SpriteRenderer>().gameObject;
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

    internal void HighlightAPlayer()
    {
        if (!isSelected)
        {
            ChangeOutline(widthOfOutline, Color.yellow);
        }
    }

    internal void HideOutline()
    {
        if (!isSelected)
        {
            ChangeOutline(0f);
        }
    }

    internal void SelectAPlayer()
    {
        manager.SelectPlayer(this);
        ChangeOutline(widthOfOutline, Color.red);
        isSelected = true;
    }

    public void DeselectPlayer()
    {
        ChangeOutline(0f);
        isSelected = false;
    }

    internal void ChangeOutline(float outlineWidth, Color color)
    {
        ChangeOutline(outlineWidth);
        foreach (var renderer in meshRenderers)
        { 
            renderer.material.SetColor("_OutlineColor", color);
        }
    }

    internal void ChangeOutline(float outlineWidth)
    {
        foreach (var renderer in meshRenderers)
        {
            renderer.material.SetFloat("_Outline", outlineWidth);
        }
    }

}

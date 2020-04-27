using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    float workedTime;
    public float workingTime;
    Transform[] boxPositions;
    const string boxSpawnerName = "Box Spawner";

    const float widthOfOutline = 0.05f;
    const float heightOverObject = 1.5f;
    Renderer[] renders;
    List<AICleaner> cleanersInteracting = new List<AICleaner>();

    public GameObject fillBar;
    Image fillBarInstance;

    TutorialManager tutoManager;

    private void Awake()
    {
        var boxSpawner = transform.Find(boxSpawnerName);
        boxPositions = new Transform[boxSpawner.childCount];
        for (int i = 0; i < boxSpawner.childCount; i++)
        {
            boxPositions[i] = boxSpawner.GetChild(i);
        }
    }

    void Start()
    {
        renders = GetComponentsInChildren<Renderer>();
        ChangeOutline(0);
        tutoManager = FindObjectOfType<TutorialManager>();
    }

    void Update()
    {

    }

    public int AmountOfBoxes()
    {
        return boxPositions.Length;
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
        return cleanersInteracting.Count > 0;
    }

    public void GetAPerson(AICleaner person)
    {
        cleanersInteracting.Add(person);
        if (fillBarInstance == null)
        {         
            var barPos = transform.position;
            barPos.y += heightOverObject;
            var barInstance = Instantiate(fillBar, barPos, fillBar.transform.rotation, GameObject.Find("World Canvas").transform);
            fillBarInstance = barInstance.transform.GetChild(0).GetComponent<Image>();
        }
    }

    public void GetWork()
    {
        workedTime += Time.deltaTime;
        if (tutoManager != null && !tutoManager.AlienShipCanBeDestroyed())
        {
            return;
        }
        fillBarInstance.fillAmount = workedTime / workingTime;
        if (workedTime >= workingTime)
        {
            foreach (var cleaner in cleanersInteracting)
            {
                cleaner.FreeThePlayer();
            }
            Destroy(fillBarInstance.transform.parent.gameObject);
            FindObjectOfType<GameManager>().DestroyAnInteractable(boxPositions, gameObject);
        }
    }
}

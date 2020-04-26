using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSettingsManager
{
    public static int currentLevel = 0;
    public static string playerName = "";
    public static float awerenessLevel = 0.0f;

    const string animalListDir = "Codes/AnimalCode";
    public static void SetPlayerCodeName(string name, string color)
    {
        var animalList = TextManager.TextsOfAsset(animalListDir);
        string animal = "";
        animal = animalList.Find((an) => an.ToLower()[0] == name.ToLower()[0]);
        Debug.Log(animal);
        if (animal == "" || animal == null)
        {
            animal = animalList[UnityEngine.Random.Range(0, animalList.Count)];
        }
        string colorUpperFirtCase = char.ToUpper(color[0]) + color.Substring(1); 
        playerName = $"{colorUpperFirtCase} {animal}";
        Debug.Log(playerName);
    }

}

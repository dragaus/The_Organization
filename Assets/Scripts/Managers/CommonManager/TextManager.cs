using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TextManager 
{
    public static string TextLenguageDir()
    {
        return "Texts/en";
    }

    public static List<string> TextsOfAsset(string assetDir)
    {
        var textsToReturn = Resources.Load<TextAsset>($"{TextLenguageDir()}/{assetDir}").text.Split('\n');
        return textsToReturn.ToList();
    }

    public static string FullTextAsset(string assetDir)
    {
        return Resources.Load<TextAsset>($"{TextLenguageDir()}/{assetDir}").text;
    }
}

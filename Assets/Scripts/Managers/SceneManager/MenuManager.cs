using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    MenuUI menuUI;
    [SerializeField]
    CreditsUI creditsUI;

    public const string sceneName = "Menu";
    // Start is called before the first frame update
    void Start()
    {
        WriteTexts();
        ShowMenu();
        SetPanelFunctions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowMenu()
    {
        menuUI.ShowPanel(true);
        creditsUI.ShowPanel(false);
    }

    void ShowCredits()
    {
        menuUI.ShowPanel(false);
        creditsUI.ShowPanel(true);
    }

    public void WriteTexts()
    {
        menuUI.SetStaticTexts(TextManager.TextsOfAsset("Scenes/Menu"));

        var creditsStrings = TextManager.TextsOfAsset("Panels/Credits");
        creditsStrings.Add(TextManager.FullTextAsset("Unique/Credits"));
        creditsUI.SetStaticTexts(creditsStrings);
    }

    void SetPanelFunctions()
    {
        menuUI.playBtn.onClick.AddListener(PlayGame);
        menuUI.creditsBtn.onClick.AddListener(ShowCredits);
#if UNITY_WEBGL
        menuUI.exitBtn.gameObject.SetActive(false);
#else
        menuUI.exitBtn.onClick.AddListener(Application.Quit);
#endif
        creditsUI.closeButton.onClick.AddListener(ShowMenu);
    }

    void PlayGame()
    {
        GameSettingsManager.currentLevel = 0;
        GameSettingsManager.awerenessLevel = 0;
        LoaderManager.LoadScene(OfficeManager.sceneName);
    }
}

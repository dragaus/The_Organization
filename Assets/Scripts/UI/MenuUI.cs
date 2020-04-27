using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct MenuUI : CommonUI
{
    public GameObject menuPanel;
    public TextMeshProUGUI gameTitle;
    public Button playBtn;
    public Button creditsBtn;
    public Button settingsBtn;
    public Button exitBtn;

    public void ShowPanel(bool needToShowPanel)
    {
        menuPanel.SetActive(needToShowPanel);
    }

    public void SetStaticTexts(List<string> staticStrings)
    {
        gameTitle.text = staticStrings[0];
        playBtn.GetComponentInChildren<TextMeshProUGUI>().text = staticStrings[1];
        creditsBtn.GetComponentInChildren<TextMeshProUGUI>().text = staticStrings[2];
        settingsBtn.GetComponentInChildren<TextMeshProUGUI>().text = staticStrings[3];
        exitBtn.GetComponentInChildren<TextMeshProUGUI>().text = staticStrings[4];
    }
}

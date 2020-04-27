using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct GameUI : CommonUI
{
    public GameObject GamePanel;
    [HideInInspector] public TextMeshProUGUI evidenceLabel;
    [HideInInspector] public TextMeshProUGUI evidenceAmounts;
    [HideInInspector] public TextMeshProUGUI awarenessLabel;
    [HideInInspector] public Image awarnessBar;
    [HideInInspector] public GameObject winPanel;
    [HideInInspector] public TextMeshProUGUI winMessage;
    [HideInInspector] public Button winBtn;
    [HideInInspector] public GameObject losePanel;
    [HideInInspector] public TextMeshProUGUI loseMessage;
    [HideInInspector] public Button loseBtn;

    public void Initialization()
    {
        var trans = GamePanel.transform;
        evidenceLabel = trans.Find("Evidence Panel").Find("Evidence Label").GetComponent<TextMeshProUGUI>();
        evidenceAmounts = trans.Find("Evidence Panel").Find("Evidence Amounts").GetComponent<TextMeshProUGUI>();
        awarenessLabel = trans.Find("Awareness Label").GetComponent<TextMeshProUGUI>();
        awarnessBar = trans.Find("Awareness Bar").GetComponent<Image>();
        winPanel = trans.Find("Win Panel").gameObject;
        winMessage = winPanel.transform.Find("Win Label").GetComponent<TextMeshProUGUI>();
        winBtn = winPanel.transform.Find("Win Btn").GetComponent<Button>();
        losePanel = trans.Find("Lose Panel").gameObject;
        loseMessage = losePanel.transform.Find("Lose Label").GetComponent<TextMeshProUGUI>();
        loseBtn = losePanel.transform.Find("Lose Btn").GetComponent<Button>();
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    public void SetStaticTexts(List<string> staticStrings)
    {
        evidenceLabel.text = staticStrings[0];
        awarenessLabel.text = staticStrings[1];
        winMessage.text = staticStrings[2];
        winBtn.GetComponentInChildren<TextMeshProUGUI>().text = staticStrings[3];
        loseMessage.text = staticStrings[4];
        loseBtn.GetComponentInChildren<TextMeshProUGUI>().text = staticStrings[5];
    }

    public void UpdateEvidence(int collected, int toCollect)
    {
        evidenceAmounts.text = $"{collected:00}/{toCollect:00}";
    }

    public void ShowPanel(bool needToShowPanel)
    {
        GamePanel.SetActive(needToShowPanel);
    }

    public void UpdateAwarenessBar(float amountOfAwareness)
    {
        string colorLabel = "8AF000";
        if (amountOfAwareness >= 0.8f)
        {
            colorLabel = "FF3320";
        }
        else if (amountOfAwareness >= 0.4f)
        {
            colorLabel = "FFC200";
        }
        ColorUtility.TryParseHtmlString($"#{colorLabel}", out Color colorOfBar);
        awarnessBar.color = colorOfBar;
        var fillImage = awarnessBar.transform.GetChild(0).GetComponent<Image>();
        fillImage.color = colorOfBar;
        fillImage.fillAmount = amountOfAwareness;
    }
}

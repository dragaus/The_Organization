using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct OfficeUI : CommonUI
{
    public GameObject panel;
    public TextMeshProUGUI programNameTMP;
    public TextMeshProUGUI chatTMP;
    public TMP_InputField chatInput;
    public Button sendBtn;
    public Button okButton;
    public TextMeshProUGUI awarenessLabel;
    public Image fillBar;
    public TextMeshProUGUI latestNewsLabel;
    public TextMeshProUGUI newsTmp;
    public void SetStaticTexts(List<string> staticStrings)
    {
        programNameTMP.text = staticStrings[0];
        sendBtn.GetComponentInChildren<TextMeshProUGUI>().text = staticStrings[1];
        okButton.GetComponentInChildren<TextMeshProUGUI>().text = staticStrings[2];
        chatInput.placeholder.GetComponent<TextMeshProUGUI>().text = staticStrings[3];
        awarenessLabel.text = staticStrings[4];
        latestNewsLabel.text = staticStrings[5];
    }

    public void ShowPanel(bool needToShowPanel)
    {
        throw new System.NotImplementedException();
    }

    public void ShowPublicAwareness(float amountOfAwareness)
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
        fillBar.color = colorOfBar;
        var fillImage = fillBar.transform.GetChild(0).GetComponent<Image>();
        fillImage.color = colorOfBar;
        fillImage.fillAmount = amountOfAwareness;

    }

    public void ShowInput(bool needToShowInput)
    {
        chatInput.transform.parent.gameObject.SetActive(needToShowInput);
        sendBtn.gameObject.SetActive(needToShowInput);
        okButton.gameObject.SetActive(!needToShowInput);
    }
}

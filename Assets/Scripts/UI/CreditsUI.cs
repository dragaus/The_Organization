using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct CreditsUI : CommonUI
{
    public GameObject creditPanel;
    public TextMeshProUGUI creditsTitleTmp;
    public TextMeshProUGUI creditsTMP;
    public Button closeButton;
    public void ShowPanel(bool needToShowPanel)
    {
        creditPanel.SetActive(needToShowPanel);
    }

    public void SetStaticTexts(List<string> staticStrings)
    {
        creditsTitleTmp.text = staticStrings[0];
        closeButton.GetComponentInChildren<TextMeshProUGUI>().text = staticStrings[1];
        creditsTMP.text = staticStrings[2];
    }

}

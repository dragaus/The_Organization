using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct TutorialUI : CommonUI
{
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialInstruction;
    public Button okBtn;

    public void ShowTutorial(string tutorialCopy, UnityAction actionForButtonOk)
    {
        okBtn.onClick.RemoveAllListeners();
        okBtn.onClick.AddListener(actionForButtonOk);
        tutorialInstruction.text = tutorialCopy;
    }

    public void ShowTutorial(string tutorialCopy)
    {
        okBtn.onClick.RemoveAllListeners();
        okBtn.onClick.AddListener(HideTutorial);
        tutorialInstruction.text = tutorialCopy;
    }

    public void HideTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    public void ShowPanel(bool needToShowPanel)
    {
        tutorialPanel.SetActive(needToShowPanel);
    }

    public void SetStaticTexts(List<string> staticStrings)
    {
        okBtn.GetComponentInChildren<TextMeshProUGUI>().text = staticStrings[0];
    }
}

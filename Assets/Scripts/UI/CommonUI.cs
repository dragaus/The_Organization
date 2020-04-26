using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface CommonUI
{
    void ShowPanel(bool needToShowPanel);
    void SetStaticTexts(List<string> staticStrings);
}

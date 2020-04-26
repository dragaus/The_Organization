using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OfficeManager : MonoBehaviour
{
    public const string sceneName = "Office";
    const string staticTextsDir = "Scenes/Office";
    const string textDir = "ChangingTexts/Office_";
    const string newsDir = "News/";
    const string awernessDir = "Awerness_";
    const string funnyDir = "Funny";

    [SerializeField]
    [Header("UI")]
    OfficeUI officeUI;

    [SerializeField]
    [Header("Sounds")]
    AudioClip[] clips;
    const float typingTime = 0.09f;
    float typeTime = 0f;
    int typeIndex = 0;
    int clipIndex = 0;

    List<string> chatStrings;
    string currentChatString;
    int chatIndex;
    string newsPromter;

    SFXManager sfxManager;

    bool isTyping;
    bool canPressOk;
    bool needInput;

    string playerName;
    string favoriteColour;

    delegate void InputDel();
    UnityAction actionToDo;
    // Start is called before the first frame update
    void Start()
    {
        officeUI.SetStaticTexts(TextManager.TextsOfAsset(staticTextsDir));
        officeUI.ShowPublicAwareness(GameSettingsManager.awerenessLevel);
        chatStrings = TextManager.TextsOfAsset($"{textDir}{GameSettingsManager.currentLevel:00}");
        sfxManager = GetComponent<SFXManager>();

        InputDel del = () => { canPressOk = officeUI.chatInput.text != ""; };
        officeUI.chatInput.onValueChanged.AddListener(delegate { del(); });

        StartTyping();
    }

    void ChangeChatButtonActions(UnityAction action)
    {
        officeUI.okButton.onClick.RemoveAllListeners();
        officeUI.sendBtn.onClick.RemoveAllListeners();
        officeUI.okButton.onClick.AddListener(action);
        officeUI.sendBtn.onClick.AddListener(action);
    }

    void StartTyping()
    {
        StartTyping(EndTyping);
        actionToDo = SetNextTyping;
    }

    void StartTyping(UnityAction action)
    {
        var chatInfo = chatStrings[chatIndex].Split('_');
        officeUI.chatInput.text = "";
        typeIndex = 0;
        string finalChatString = "";
        if (chatInfo[0].Contains("*"))
        {
            var sb = new System.Text.StringBuilder();
            var stripString = chatInfo[0].Split('*');
            for (int i = 0; i < stripString.Length; i++)
            {
                if (i != 0)
                {
                    sb.Append(GameSettingsManager.playerName);
                    sb.Append('\n');
                }
                sb.Append(stripString[i]);
                finalChatString = sb.ToString();
            }

        }
        else
        {
            finalChatString = chatInfo[0];
        }
        currentChatString = finalChatString;
        bool needInput = Convert.ToBoolean(int.Parse(chatInfo[1]));
        officeUI.ShowInput(needInput);
        this.needInput = needInput;
        canPressOk = !needInput;
        isTyping = true;
        actionToDo = action;
        ChangeChatButtonActions(action);
    }

    void SetNextTyping()
    {
        chatIndex++;
        StartTyping();
    }

    void SetNextTyping(UnityAction action)
    {
        chatIndex++;
        StartTyping(action);
    }

    void TypeTheMessage()
    {
        if (isTyping)
        {
            typeTime += Time.deltaTime;
            if (typeTime >= typingTime)
            {
                sfxManager.PlayClip(clips[clipIndex]);

                if (++clipIndex >= clips.Length)
                {
                    clipIndex = 0;
                }

                officeUI.chatTMP.text = currentChatString.Substring(0, ++typeIndex);
                int messageLenght = chatStrings[chatIndex].Length;
                if (typeIndex >= messageLenght - 3)
                {
                    EndTyping();
                }
                typeTime = 0;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {

                EndTyping();
            }
        }
    }


    void EndTyping()
    {
        officeUI.chatTMP.text = currentChatString;
        isTyping = false;
        typeTime = 0;
        if (needInput)
        {
            officeUI.chatInput.ActivateInputField();
        }
    }

    void AcceptMessage()
    {
        if (!isTyping)
        {
            Debug.Log($"can press ok is {canPressOk}");
            if (canPressOk && Input.GetKeyDown(KeyCode.Return))
            {
                if (!NeedTutorialLevelInputManager(officeUI.chatInput.text))
                {
                    actionToDo();
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        TypeTheMessage();
        AcceptMessage();
    }

    bool NeedTutorialLevelInputManager(string input)
    {
        if (GameSettingsManager.currentLevel == 0)
        {
            if (needInput)
            {
                switch (chatIndex)
                {
                    case 1:
                        playerName = input;
                        break;
                    case 2:
                        favoriteColour = input;
                        GameSettingsManager.SetPlayerCodeName(playerName, favoriteColour);
                        break;
                    default:
                        YesOrNoAnswer(input, GoToNextLevel, SkipTutorial);
                        return true;
                }
                SetNextTyping();
                return true;
            }
        }
        return false;
    }

    void YesOrNoAnswer(string input, UnityAction actionForYes, UnityAction actionForNo)
    {
        string firstLetter = input.ToLower()[0].ToString();
        if (firstLetter == "y")
        {
            SetNextTyping(actionForYes);
        }
        else
        {
            chatIndex++;
            SetNextTyping(actionForNo);
        }
    }

    void GoToNextLevel()
    {
        GameSettingsManager.currentLevel++;
        LoaderManager.LoadScene($"{GameManager.sceneName}{GameSettingsManager.currentLevel}");
    }

    void SkipTutorial()
    {
        GameSettingsManager.currentLevel++;
        SceneManager.LoadScene(sceneName);
    }
}

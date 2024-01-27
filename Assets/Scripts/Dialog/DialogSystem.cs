using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


public class DialogSystem : StaticReference<DialogSystem>
{
    [System.Serializable]
    public class DialogCharacter
    {
        public string characterKey;
        public string emotionKey;
        public Sprite sprite;
    }

    [SerializeField] private DialogData dialogData;
    [SerializeField] private List<DialogCharacter> dialogCharacters;

    [Header("Process")]
    [SerializeField] private bool ongoingDialog;
    [SerializeField] private Queue<DialogData.DialogLine> dialogLinesQueue;
    [SerializeField] private DialogData.DialogLine currentDialogLine;

    [Header("Caches")]
    [SerializeField] private GameObject dialogHandler;
    [SerializeField] private GameObject dialogOverlay;

    [SerializeField] private TextMeshProUGUI dialogHeader;
    [SerializeField] private TextMeshProUGUI dialogContent;
    [SerializeField] private Image characterImageLeft;
    [SerializeField] private Image characterImageRight;
    [SerializeField] private PlayerControl playerControl;

    private void Awake()
    {
        BaseAwake(this);
    }

    private void Start()
    {
        ////dummy
        //NextDialog(dialogData);
        Hide();
    }

    private void Show()
    {
        dialogHandler.SetActive(true);
        dialogOverlay.SetActive(true);
    }

    private void Hide()
    {
        dialogHandler.SetActive(false);
        dialogOverlay.SetActive(false);
    }

    public void NextDialog(DialogData data)
    {
        dialogData = data;
        Fetch();

        Show();

        ongoingDialog = true;
        playerControl.SetFreeze(true);

        NextLine();
    }

    private void Fetch()
    {
        dialogLinesQueue = new Queue<DialogData.DialogLine>();

        foreach (DialogData.DialogLine dl in dialogData.lines)
        {
            dialogLinesQueue.Enqueue(dl);
        }
    }

    public void NextLine()
    {
        if (!ongoingDialog)
        {
            print("ERROR");
            return;
        }

        if (dialogLinesQueue.Count > 0)
        {
            currentDialogLine = dialogLinesQueue.Dequeue();
            Load(currentDialogLine);
        }
        else
        {
            print("Dialog runs out!");
            ongoingDialog = false;

            Hide();

            playerControl.SetFreeze(false);

            var informationKey = dialogData.informationKey;
            if(informationKey != "none")
            {
                InformationSystem.Instance().AddInformation(dialogData.informationKey);
            }
        }
    }

    private void Load(DialogData.DialogLine dialogLine)
    {
        dialogHeader.text = dialogLine.characterKey;
        dialogContent.text = dialogLine.content;

        var charImage = LoadCharacterSprite(dialogLine);
        if (charImage == null)
        {
            print("ERROR");
            return;
        }

        SetupDialogCharacter(dialogLine, charImage);
    }

    private Sprite LoadCharacterSprite(DialogData.DialogLine dialogLine)
    {
        for (int i = 0; i < dialogCharacters.Count; i++)
        {
            var temp = dialogCharacters[i];
            if (temp.characterKey == dialogLine.characterKey && temp.emotionKey == dialogLine.emotionKey)
            {
                return temp.sprite;
            }
        }

        return null;
    }

    private void SetupDialogCharacter(DialogData.DialogLine dialogLine, Sprite charImage)
    {
        if (dialogLine.onTheRight)
        {
            characterImageLeft.gameObject.SetActive(false);
            characterImageRight.gameObject.SetActive(true);

            characterImageRight.sprite = charImage;
        }
        else
        {
            characterImageLeft.gameObject.SetActive(true);
            characterImageRight.gameObject.SetActive(false);

            characterImageLeft.sprite = charImage;
        }
    }


    private void OnDestroy()
    {
        BaseOnDestroy();
    }
}
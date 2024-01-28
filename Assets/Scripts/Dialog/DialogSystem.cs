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
    [SerializeField] private List<DialogData.DialogLine> dialogLines;
    [SerializeField] private DialogData.DialogLine currentDialogLine;

    [Header("Caches")]
    [SerializeField] private GameObject dialogHandler;
    [SerializeField] private GameObject dialogOverlay;

    [SerializeField] private TextMeshProUGUI dialogHeader;
    [SerializeField] private TextMeshProUGUI dialogContent;
    [SerializeField] private Image characterImageLeft;
    [SerializeField] private Image characterImageRight;
    [SerializeField] private PlayerControl playerControl;

    [SerializeField] private Button dialogNextButton;
    [SerializeField] private Button dialogOptionButton1;
    [SerializeField] private Button dialogOptionButton2;
    [SerializeField] private Button dialogOptionButton3;


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
        if(data.dialogKey == "null")
        {
            print("Currently cant talk to " + gameObject);
            return;
        }

        dialogData = data;
        Fetch();

        Show();

        ongoingDialog = true;
        playerControl.SetFreeze(true);

        StartLine();
    }

    private void Fetch()
    {
        //dialogLines = new List<DialogData.DialogLine>();
        dialogLines = dialogData.lines;
    }

    public void StartLine()
    {
        currentDialogLine = dialogData.lines.Find(e => e.key == dialogData.initialLineKey);
        Load(currentDialogLine);
    }

        public void NextLine(string nextDialogLineKey = "")
    {
        if (!ongoingDialog)
        {
            print("ERROR");
            return;
        }

        if(currentDialogLine.nextKey != "END")
        {
            if (nextDialogLineKey == "")
            {
                currentDialogLine = dialogLines.Find(e => e.key == currentDialogLine.nextKey);
            }
            else
            {
                currentDialogLine = dialogLines.Find(e => e.key == nextDialogLineKey);
            }
            //if(!currentDialogLine)
            //{
            //    print("ERROR");
            //}

            Load(currentDialogLine);
        } else
        {
            print("Dialog runs out!");
            ongoingDialog = false;

            Hide();

            playerControl.SetFreeze(false);

            var informationKey = dialogData.informationKey;
            if (informationKey != "none" && informationKey != "")
            {
                InformationSystem.Instance().AddInformation(dialogData.informationKey);
            }

            dialogData = null;
        }



        //if (dialogLines.Count > 0)
        //{
        //    if(nextDialogLineKey == "")
        //    {
        //        currentDialogLine = dialogLines.Find(e => e.key == currentDialogLine.nextKey);
        //    } else
        //    {
        //        currentDialogLine = dialogLines.Find(e => e.key == nextDialogLineKey);
        //    }
        //    //if(!currentDialogLine)
        //    //{
        //    //    print("ERROR");
        //    //}

        //    Load(currentDialogLine);
        //}
        //else
        //{
        //    print("Dialog runs out!");
        //    ongoingDialog = false;

        //    Hide();

        //    playerControl.SetFreeze(false);

        //    var informationKey = dialogData.informationKey;
        //    if(informationKey != "none" || informationKey != "")
        //    {
        //        InformationSystem.Instance().AddInformation(dialogData.informationKey);
        //    }
        //}
    }

    private void Load(DialogData.DialogLine dialogLine)
    {
        dialogHeader.text = dialogLine.characterKey;
        dialogContent.text = dialogLine.content;
        
        var conditionalDialog = dialogLine.conditionalDialog;
        if (conditionalDialog)
        {
            dialogNextButton.gameObject.SetActive(false);
            
            dialogOptionButton1.onClick.AddListener(
                () => NextLine(dialogLine.dialogOption1.nextKey)
            );
            dialogOptionButton1.GetComponentInChildren<TextMeshProUGUI>().text = "     " + dialogLine.dialogOption1.content;
            dialogOptionButton1.gameObject.SetActive(true);

            dialogOptionButton2.onClick.AddListener(
                () => NextLine(dialogLine.dialogOption2.nextKey)
            );
            dialogOptionButton2.GetComponentInChildren<TextMeshProUGUI>().text = "     " + dialogLine.dialogOption2.content;
            dialogOptionButton2.gameObject.SetActive(true);

            dialogOptionButton3.onClick.AddListener(
                () => NextLine(dialogLine.dialogOption3.nextKey)
            );
            dialogOptionButton3.GetComponentInChildren<TextMeshProUGUI>().text = "     " + dialogLine.dialogOption3.content;
            dialogOptionButton3.gameObject.SetActive(true);
        } else
        {
            dialogNextButton.gameObject.SetActive(true);

            dialogOptionButton1.gameObject.SetActive(false);
            dialogOptionButton2.gameObject.SetActive(false);
            dialogOptionButton3.gameObject.SetActive(false);
        }



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
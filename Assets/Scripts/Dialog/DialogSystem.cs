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
    [SerializeField] private bool isTypingLine;
    [SerializeField] private float typingLineDelay;
    [SerializeField] private bool onConditionalDialogState;
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

    [SerializeField] private Button imageDialogBox;
    [SerializeField] private GameObject dialogInstructionText;

    [SerializeField] private Button dialogOptionButton1;
    [SerializeField] private Button dialogOptionButton2;
    [SerializeField] private Button dialogOptionButton3;


    private Coroutine typingLineCoroutine;


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

        if(isTypingLine == true)
        {
            ForceFinishTypingDialogLine();
            return;
        }


        if (currentDialogLine.nextKey != "END")
        {
            // only NextLine call with argument is allowed to be called if onConditionalDialogState
            if (nextDialogLineKey == "")
            {
                if (onConditionalDialogState)
                {
                    print("PROHIBITED");
                    return;
                }

                currentDialogLine = dialogLines.Find(e => e.key == currentDialogLine.nextKey);
            }
            else
            {

                currentDialogLine = dialogLines.Find(e => e.key == nextDialogLineKey);
            }
            

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
    }

    private void Load(DialogData.DialogLine dialogLine)
    {
        dialogHeader.text = dialogLine.characterKey;

        var charImage = LoadCharacterSprite(dialogLine);
        if (charImage == null)
        {
            print("ERROR");
            return;
        }

        SetupDialogCharacter(dialogLine, charImage);
        if (dialogLine.useLaughSoundEffect)
        {
            SoundEffectManager.Instance().PlayLaughSoundEffect();
        }

        onConditionalDialogState = dialogLine.conditionalDialog;
        if (onConditionalDialogState)
        {
            isTypingLine = false;
            dialogContent.text = dialogLine.content;

            //dialogNextButton.gameObject.SetActive(false);
            imageDialogBox.interactable = false;
            dialogInstructionText.SetActive(false);

            SetupDialogOptions(dialogLine);
        } else
        {
            //dialogNextButton.gameObject.SetActive(true);
            imageDialogBox.interactable = true;
            dialogInstructionText.SetActive(true);

            dialogOptionButton1.gameObject.SetActive(false);
            dialogOptionButton2.gameObject.SetActive(false);
            dialogOptionButton3.gameObject.SetActive(false);

            isTypingLine = true;
            typingLineCoroutine = StartCoroutine(TypeDialogLine());            
            //StartCoroutine(TypeDialogLine());
            //dialogContent.text = dialogLine.content;

        }
    }

    private IEnumerator TypeDialogLine()
    {
        //isTypingLine = true;
        //print("brow");

        dialogContent.text = "";
        string dialogString = currentDialogLine.content;

        for(int i=0; i<dialogString.Length; i++)
        {
            if(isTypingLine == false)
            {
                break;
            }

            dialogContent.text += dialogString[i];
            yield return new WaitForSeconds(typingLineDelay);
        }

        isTypingLine = false;
    }

    private void ForceFinishTypingDialogLine()
    {
        //StopAllCoroutines();
        StopCoroutine(typingLineCoroutine);
        isTypingLine = false;
        dialogContent.text = currentDialogLine.content;
    }

    private void SetupDialogOptions(DialogData.DialogLine dialogLine)
    {
        print("hoi");

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
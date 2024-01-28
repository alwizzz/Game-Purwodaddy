using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "Dialog/DialogData")]
public class DialogData : ScriptableObject
{
    [System.Serializable]
    public struct DialogOption
    {
        public string content;
        public string nextKey;
    }

    [System.Serializable]
    public struct DialogLine
    {
        public string key;

        public bool useLaughSoundEffect;

        public string characterKey;
        public string emotionKey;
        public bool onTheRight;
        [TextArea(minLines: 3, maxLines: 5)] public string content;
        public string nextKey;

        public bool conditionalDialog;
        public DialogOption dialogOption1;
        public DialogOption dialogOption2;
        public DialogOption dialogOption3;

    }

    public string dialogKey;
    public string informationKey;
    public List<DialogLine> lines;
    public string initialLineKey;
}
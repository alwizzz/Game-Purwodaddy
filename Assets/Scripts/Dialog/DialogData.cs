using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "Dialog/DialogData")]
public class DialogData : ScriptableObject
{
    [System.Serializable]
    public struct DialogLine
    {
        public string characterKey;
        public string emotionKey;
        public bool onTheRight;
        [TextArea(minLines: 3, maxLines: 5)] public string content;
    }

    public string dialogKey;
    public string informationKey;
    public List<DialogLine> lines;
}
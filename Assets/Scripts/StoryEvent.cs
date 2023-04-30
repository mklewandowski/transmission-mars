using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent
{
    public string ChoiceLeadIn = "";
    public List<DialogChunk> PreDialogChunks = new List<DialogChunk>();
    public string Choice1 = "";
    public string Choice2 = "";
    public List<DialogChunk> PostDialogChunks = new List<DialogChunk>();
}

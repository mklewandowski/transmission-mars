using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSceneManager : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField]
    GameObject TitlePanel;

    [SerializeField]
    GameObject StoryPanel;

    [SerializeField]
    GameObject PersonContainer;
    [SerializeField]
    Image PersonImage;
    [SerializeField]
    Sprite ChloeSprite;
    [SerializeField]
    Sprite ChloeTalkSprite;
    [SerializeField]
    Sprite FritzSprite;
    [SerializeField]
    Sprite FritzTalkSprite;

    [SerializeField]
    GameObject DialogContainer;
    [SerializeField]
    TypewriterUI DialogContainerText;

    int currTextChunk = 0;
    int currTextChunkIndex = 0;
    bool currTextPre = true;

    [SerializeField]
    GameObject ChoiceContainer;
    [SerializeField]
    TextMeshProUGUI Choice1Text;
    [SerializeField]
    TextMeshProUGUI Choice2Text;

    [SerializeField]
    GameObject StationContainer;
    [SerializeField]
    TextMeshProUGUI StationText;
    [SerializeField]
    TextMeshProUGUI WeekText;

    [SerializeField]
    GameObject InterludeContainer;

    float delayTimer = 0f;
    float delayTimerMax = 1f;

    StoryEvent[] storyEvents = new StoryEvent[10];
    int currStoryEvent = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject am = GameObject.Find("AudioManager");
        audioManager = am.GetComponent<AudioManager>();

        AddIntroStoryEvent();
        AddPunkStoryEvent();
    }

    void AddIntroStoryEvent()
    {
        StoryEvent introStoryEvent = new StoryEvent();
        introStoryEvent.ChoiceLeadIn = "none";
        introStoryEvent.Choice1 = "5 GHZ";
        introStoryEvent.Choice2 = "10 GHZ";
        DialogChunk preChunk = new DialogChunk();
        preChunk.PersonSprite = ChloeSprite;
        preChunk.PersonTalkSprite = ChloeTalkSprite;
        preChunk.PersonName = "CHLOE: ";
        preChunk.PersonDialog.Add("We made it to Mars! And we have all the equipment needed to start our new radio station!");
        preChunk.PersonDialog.Add("Soon, the people of Mars will fall in love with lunar lo-fi!");
        preChunk.PersonDialog.Add("We're in charge of the tech so first we need to pick a frequency to broadcast our music.");
        preChunk.PersonDialog.Add("I think we should try 5 GHZ or 10 GHZ. What do you think?");
        introStoryEvent.PreDialogChunks.Add(preChunk);
        DialogChunk postChunk = new DialogChunk();
        postChunk.PersonSprite = ChloeSprite;
        postChunk.PersonTalkSprite = ChloeTalkSprite;
        postChunk.PersonName = "CHLOE: ";
        postChunk.PersonDialog.Add("Great! Let's start sending lunar lo-fi across the Martian mountains!");
        introStoryEvent.PostDialogChunks.Add(postChunk);
        storyEvents[0] = introStoryEvent;
    }
    void AddPunkStoryEvent()
    {
        StoryEvent storyEvent = new StoryEvent();
        storyEvent.ChoiceLeadIn = "none";
        storyEvent.Choice1 = "1 GHZ";
        storyEvent.Choice2 = "2 GHZ";

        DialogChunk preChunk = new DialogChunk();
        preChunk.PersonSprite = FritzSprite;
        preChunk.PersonTalkSprite = FritzTalkSprite;
        preChunk.PersonName = "FRITZ: ";
        preChunk.PersonDialog.Add("Welcome to Mars... I run KMARZ, a local punk radio station.");
        preChunk.PersonDialog.Add("You're broadcasting over the SAME frequency we use for our punk station!");
        preChunk.PersonDialog.Add("We're getting a lot of interference.");
        preChunk.PersonDialog.Add("If you don't want trouble, you better change frequencies!");
        storyEvent.PreDialogChunks.Add(preChunk);

        DialogChunk preChunk2 = new DialogChunk();
        preChunk2.PersonSprite = ChloeSprite;
        preChunk2.PersonTalkSprite = ChloeTalkSprite;
        preChunk2.PersonName = "CHLOE: ";
        preChunk2.PersonDialog.Add("We better do what he says.");
        preChunk2.PersonDialog.Add("Let's choose a new frequency.");
        storyEvent.PreDialogChunks.Add(preChunk2);

        DialogChunk postChunk = new DialogChunk();
        postChunk.PersonSprite = ChloeSprite;
        postChunk.PersonTalkSprite = ChloeTalkSprite;
        postChunk.PersonName = "CHLOE: ";
        postChunk.PersonDialog.Add("Great! Lunar lo-fi is live!");
        storyEvent.PostDialogChunks.Add(postChunk);
        storyEvents[1] = storyEvent;
    }

    // Update is called once per frame
    void Update()
    {
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer < 0)
            {
                DialogContainer.transform.localScale = new Vector3 (.1f, .1f, .1f);
                DialogContainer.SetActive(true);
                DialogContainer.GetComponent<GrowAndShrink>().StartEffect();
                DialogContainerText.StartEffect(storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonName,
                    storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonDialog[currTextChunkIndex],
                    storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonSprite, storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonTalkSprite
                );
            }
        }
    }

    public void StartGame()
    {
        audioManager.PlayMenuSound();
        TitlePanel.GetComponent<MoveNormal>().MoveUp();
        PersonImage.sprite = storyEvents[currStoryEvent].PreDialogChunks[0].PersonSprite;
        StoryPanel.GetComponent<MoveNormal>().MoveUp();
        delayTimer = delayTimerMax;
    }

    public void AdvanceText()
    {
        audioManager.PlayMenuSound();
        currTextChunkIndex++;
        if (currTextPre)
        {
            if (currTextChunkIndex < storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonDialog.Count)
            {
                DialogContainerText.StartEffect(storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonName,
                    storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonDialog[currTextChunkIndex],
                    storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonSprite, storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonTalkSprite
                );
            }
            else
            {
                currTextChunk++;
                // we either need to read the next dialog chunk or move to post
                if (currTextChunk < storyEvents[currStoryEvent].PreDialogChunks.Count)
                {
                    // someone else needs to speak
                    currTextChunkIndex = 0;
                    DialogContainerText.StartEffect(storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonName,
                        storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonDialog[currTextChunkIndex],
                        storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonSprite, storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonTalkSprite
                    );
                }
                else
                {
                    // show choice
                    currTextPre = false;
                    ShowChoice();
                }
            }
        }
        else
        {
            if (currTextChunkIndex < storyEvents[currStoryEvent].PostDialogChunks[currTextChunk].PersonDialog.Count)
            {
                DialogContainerText.StartEffect(storyEvents[currStoryEvent].PostDialogChunks[currTextChunk].PersonName,
                    storyEvents[currStoryEvent].PostDialogChunks[currTextChunk].PersonDialog[currTextChunkIndex],
                    storyEvents[currStoryEvent].PostDialogChunks[currTextChunk].PersonSprite, storyEvents[currStoryEvent].PostDialogChunks[currTextChunk].PersonTalkSprite
                );
            }
            else
            {
                // hide this section of story
                DialogContainer.SetActive(false);
                audioManager.StartMusic();
                PersonContainer.GetComponent<MoveNormal>().MoveDown();
                InterludeContainer.GetComponent<MoveNormal>().MoveUp();
            }
        }
    }

    public void ShowChoice()
    {
        Choice1Text.text = storyEvents[currStoryEvent].Choice1;
        Choice2Text.text = storyEvents[currStoryEvent].Choice2;
        DialogContainer.SetActive(false);
        ChoiceContainer.SetActive(true);
    }

    public void ChooseOne()
    {
        StationText.text = storyEvents[currStoryEvent].Choice1;
        MakeChoice();
    }

    public void ChooseTwo()
    {
        StationText.text = storyEvents[currStoryEvent].Choice2;
        MakeChoice();
    }

    public void MakeChoice()
    {
        audioManager.PlayMenuSound();
        ChoiceContainer.SetActive(false);
        currTextChunkIndex = 0;
        currTextChunk = 0;
        DialogContainer.transform.localScale = new Vector3 (.1f, .1f, .1f);
        DialogContainer.SetActive(true);
        DialogContainer.GetComponent<GrowAndShrink>().StartEffect();
        DialogContainerText.StartEffect(storyEvents[currStoryEvent].PostDialogChunks[currTextChunk].PersonName,
            storyEvents[currStoryEvent].PostDialogChunks[currTextChunk].PersonDialog[currTextChunkIndex],
            storyEvents[currStoryEvent].PostDialogChunks[currTextChunk].PersonSprite, storyEvents[currStoryEvent].PostDialogChunks[currTextChunk].PersonTalkSprite
        );
        StationContainer.SetActive(true);
    }

    public void NextStoryEvent()
    {
        audioManager.PlayMenuSound();

        audioManager.StopMusic();
        currStoryEvent++;
        WeekText.text = "WEEK " + (currStoryEvent + 1);
        currTextChunkIndex = 0;
        currTextChunk = 0;
        currTextPre = true;
        PersonContainer.GetComponent<MoveNormal>().MoveUp();
        InterludeContainer.GetComponent<MoveNormal>().MoveDown();

        DialogContainer.transform.localScale = new Vector3 (.1f, .1f, .1f);
        DialogContainer.SetActive(true);
        DialogContainer.GetComponent<GrowAndShrink>().StartEffect();
        DialogContainerText.StartEffect(storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonName,
            storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonDialog[currTextChunkIndex],
            storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonSprite, storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonTalkSprite
        );
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSceneManager : MonoBehaviour
{
    AudioManager audioManager;
    FadeManager fadeManager;

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
    Sprite JanSprite;
    [SerializeField]
    Sprite JanTalkSprite;

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

    float speakerDelayTimer = 0f;
    float speakerDelayTimerMax = .4f;

    float changeSpeakerDelayTimer = 0f;
    float changeSpeakerDelayTimerMax = .4f;

    bool fadeIn = false;
    bool fadeOut = false;

    StoryEvent[] storyEvents = new StoryEvent[10];
    int currStoryEvent = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject am = GameObject.Find("AudioManager");
        audioManager = am.GetComponent<AudioManager>();
        fadeManager = this.GetComponent<FadeManager>();
        AddIntroStoryEvent();
        AddPunkStoryEvent();
        AddMiningStoryEvent();
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
        preChunk.PersonDialog.Add("Could you do us a HUGE favor and change frequencies?");
        storyEvent.PreDialogChunks.Add(preChunk);

        DialogChunk preChunk2 = new DialogChunk();
        preChunk2.PersonSprite = ChloeSprite;
        preChunk2.PersonTalkSprite = ChloeTalkSprite;
        preChunk2.PersonName = "CHLOE: ";
        preChunk2.PersonDialog.Add("He's right. They were on this frequency before us.");
        preChunk2.PersonDialog.Add("Let's choose a new frequency.");
        storyEvent.PreDialogChunks.Add(preChunk2);

        DialogChunk postChunk = new DialogChunk();
        postChunk.PersonSprite = ChloeSprite;
        postChunk.PersonTalkSprite = ChloeTalkSprite;
        postChunk.PersonName = "CHLOE: ";
        postChunk.PersonDialog.Add("Hurrah! Lunar lo-fi is live!");
        storyEvent.PostDialogChunks.Add(postChunk);
        storyEvents[1] = storyEvent;
    }
    void AddMiningStoryEvent()
    {
        StoryEvent storyEvent = new StoryEvent();
        storyEvent.ChoiceLeadIn = "none";
        storyEvent.Choice1 = "3 GHZ";
        storyEvent.Choice2 = "4 GHZ";

        DialogChunk preChunk = new DialogChunk();
        preChunk.PersonSprite = JanSprite;
        preChunk.PersonTalkSprite = JanTalkSprite;
        preChunk.PersonName = "Jan: ";
        preChunk.PersonDialog.Add("Are you the folks running the radio station?");
        preChunk.PersonDialog.Add("We run the mining camp over the hill.");
        preChunk.PersonDialog.Add("You're radio station is interfering with our walkie talkies.");
        preChunk.PersonDialog.Add("We NEED to be able to communicate with our miners in case of an explosion of accident.");
        preChunk.PersonDialog.Add("You're going to have to shut down or change your broadcast.");
        storyEvent.PreDialogChunks.Add(preChunk);

        DialogChunk preChunk2 = new DialogChunk();
        preChunk2.PersonSprite = ChloeSprite;
        preChunk2.PersonTalkSprite = ChloeTalkSprite;
        preChunk2.PersonName = "CHLOE: ";
        preChunk2.PersonDialog.Add("I think he means business.");
        preChunk2.PersonDialog.Add("Let's choose a new frequency.");
        storyEvent.PreDialogChunks.Add(preChunk2);

        DialogChunk postChunk = new DialogChunk();
        postChunk.PersonSprite = ChloeSprite;
        postChunk.PersonTalkSprite = ChloeTalkSprite;
        postChunk.PersonName = "CHLOE: ";
        postChunk.PersonDialog.Add("Whew! Lunar lo-fi is live!");
        storyEvent.PostDialogChunks.Add(postChunk);
        storyEvents[2] = storyEvent;
    }

    // Update is called once per frame
    void Update()
    {
        if (speakerDelayTimer > 0)
        {
            speakerDelayTimer -= Time.deltaTime;
            if (speakerDelayTimer < 0)
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
        if (changeSpeakerDelayTimer > 0)
        {
            changeSpeakerDelayTimer -= Time.deltaTime;
            if (changeSpeakerDelayTimer < 0)
            {
                PersonImage.sprite = storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonSprite;
                PersonContainer.GetComponent<MoveNormal>().MoveUp();
                speakerDelayTimer = speakerDelayTimerMax;
            }
        }
        if (fadeOut)
        {
            if (fadeManager.FadeComplete())
            {
                fadeOut = false;
                audioManager.StopMusic();
                WeekText.text = "WEEK " + (currStoryEvent + 1);
                fadeManager.StartFadeIn();
                fadeIn = true;
            }
        }
        if (fadeIn)
        {
            if (fadeManager.FadeComplete())
            {
                fadeIn = false;
                PersonImage.sprite = storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonSprite;
                PersonContainer.GetComponent<MoveNormal>().MoveUp();
                DialogContainer.SetActive(false);
                speakerDelayTimer = speakerDelayTimerMax * 2f;
            }
        }
    }

    public void StartGame()
    {
        audioManager.PlayMenuSound();
        TitlePanel.GetComponent<MoveNormal>().MoveUp();
        PersonImage.sprite = storyEvents[currStoryEvent].PreDialogChunks[currTextChunk].PersonSprite;
        StoryPanel.GetComponent<MoveNormal>().MoveUp();
        speakerDelayTimer = speakerDelayTimerMax * 2f;
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
                    DialogContainer.SetActive(false);
                    changeSpeakerDelayTimer = changeSpeakerDelayTimerMax;
                    PersonContainer.GetComponent<MoveNormal>().MoveDown();
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
                audioManager.StartMusic(currStoryEvent);
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
        currStoryEvent++;
        currTextChunkIndex = 0;
        currTextChunk = 0;
        currTextPre = true;
        InterludeContainer.GetComponent<MoveNormal>().MoveDown();

        fadeManager.StartFadeOut();
        fadeOut = true;
    }


}

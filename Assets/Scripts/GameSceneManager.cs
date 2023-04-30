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
    GameObject InterludeContainer;

    float delayTimer = 0f;
    float delayTimerMax = 1f;

    StoryEvent introStoryEvent;
    [SerializeField]
    Sprite ChloeSprite;
    [SerializeField]
    Sprite ChloeTalkSprite;

    // Start is called before the first frame update
    void Start()
    {
        GameObject am = GameObject.Find("AudioManager");
        audioManager = am.GetComponent<AudioManager>();

        introStoryEvent = new StoryEvent();
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
                DialogContainerText.StartEffect(introStoryEvent.PreDialogChunks[currTextChunk].PersonName,
                    introStoryEvent.PreDialogChunks[currTextChunk].PersonDialog[currTextChunkIndex],
                    introStoryEvent.PreDialogChunks[currTextChunk].PersonSprite, introStoryEvent.PreDialogChunks[currTextChunk].PersonTalkSprite
                );
            }
        }
    }

    public void StartGame()
    {
        audioManager.PlayMenuSound();
        TitlePanel.GetComponent<MoveNormal>().MoveUp();
        PersonImage.sprite = introStoryEvent.PreDialogChunks[0].PersonSprite;
        StoryPanel.GetComponent<MoveNormal>().MoveUp();
        delayTimer = delayTimerMax;
    }

    public void AdvanceText()
    {
        audioManager.PlayMenuSound();
        currTextChunkIndex++;
        if (currTextPre)
        {
            if (currTextChunkIndex < introStoryEvent.PreDialogChunks[currTextChunk].PersonDialog.Count)
            {
                DialogContainerText.StartEffect(introStoryEvent.PreDialogChunks[currTextChunk].PersonName,
                    introStoryEvent.PreDialogChunks[currTextChunk].PersonDialog[currTextChunkIndex],
                    introStoryEvent.PreDialogChunks[currTextChunk].PersonSprite, introStoryEvent.PreDialogChunks[currTextChunk].PersonTalkSprite
                );
            }
            else
            {
                currTextChunk++;
                // we either need to read the next dialog chunk or move to post
                if (currTextChunk < introStoryEvent.PreDialogChunks.Count)
                {
                    // someone else needs to speak
                    currTextChunkIndex = 0;
                    DialogContainerText.StartEffect(introStoryEvent.PreDialogChunks[currTextChunk].PersonName,
                        introStoryEvent.PreDialogChunks[currTextChunk].PersonDialog[currTextChunkIndex],
                        introStoryEvent.PreDialogChunks[currTextChunk].PersonSprite, introStoryEvent.PreDialogChunks[currTextChunk].PersonTalkSprite
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
            if (currTextChunkIndex < introStoryEvent.PostDialogChunks[currTextChunk].PersonDialog.Count)
            {
                DialogContainerText.StartEffect(introStoryEvent.PostDialogChunks[currTextChunk].PersonName,
                    introStoryEvent.PostDialogChunks[currTextChunk].PersonDialog[currTextChunkIndex],
                    introStoryEvent.PostDialogChunks[currTextChunk].PersonSprite, introStoryEvent.PostDialogChunks[currTextChunk].PersonTalkSprite
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
        Choice1Text.text = introStoryEvent.Choice1;
        Choice2Text.text = introStoryEvent.Choice2;
        DialogContainer.SetActive(false);
        ChoiceContainer.SetActive(true);
    }

    public void ChooseOne()
    {
        StationText.text = introStoryEvent.Choice1;
        MakeChoice();
    }

    public void ChooseTwo()
    {
        StationText.text = introStoryEvent.Choice2;
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
        DialogContainerText.StartEffect(introStoryEvent.PostDialogChunks[currTextChunk].PersonName,
            introStoryEvent.PostDialogChunks[currTextChunk].PersonDialog[currTextChunkIndex],
            introStoryEvent.PostDialogChunks[currTextChunk].PersonSprite, introStoryEvent.PostDialogChunks[currTextChunk].PersonTalkSprite
        );
        StationContainer.SetActive(true);
    }


}

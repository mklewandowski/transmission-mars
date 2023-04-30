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
    GameObject IntroPanel;

    [SerializeField]
    GameObject TextContainer;
    [SerializeField]
    TypewriterUI TextContainerText;
    [SerializeField]
    GameObject PersonContainer;


    int currText = 0;
    int currTextBlock = 0;

    [SerializeField]
    GameObject ChoiceContainer;

    [SerializeField]
    GameObject StationContainer;
    [SerializeField]
    TextMeshProUGUI StationText;

    [SerializeField]
    GameObject InterludeContainer;

    float delayTimer = 0f;
    float delayTimerMax = 1f;

    string [] introText = {
        "We made it to Mars! And we have all the equipment needed to start our new radio station!",
        "Soon, the people of Mars will be in love with lunar lo-fi.",
        "We're in charge of the tech so first we need to pick a frequency to broadcast our music.",
        "I think we should try 1 GHZ or 2 GHZ. What do you think?"
    };
    string [] intro2Text = {
        "Great! Let's start sending lunar lo-fi across the Martian mountains!"
    };

    // Start is called before the first frame update
    void Start()
    {
        GameObject am = GameObject.Find("AudioManager");
        audioManager = am.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer < 0)
            {
                TextContainer.transform.localScale = new Vector3 (.1f, .1f, .1f);
                TextContainer.SetActive(true);
                TextContainer.GetComponent<GrowAndShrink>().StartEffect();
                TextContainerText.StartEffect( "CHLOE: ", introText[currText]);
            }
        }
    }

    public void StartGame()
    {
        audioManager.PlayMenuSound();
        TitlePanel.GetComponent<MoveNormal>().MoveUp();
        IntroPanel.GetComponent<MoveNormal>().MoveUp();
        delayTimer = delayTimerMax;
    }

    public void AdvanceText()
    {
        audioManager.PlayMenuSound();
        currText++;
        if (currText < (currTextBlock == 0 ? introText.Length : intro2Text.Length))
        {
            TextContainerText.StartEffect( "CHLOE: ", currTextBlock == 0 ? introText[currText] : intro2Text[currText]);
        }
        else
        {
            if (currTextBlock == 0)
            {
                currTextBlock = 1;
                ShowChoice();
            }
            else
            {
                // hide this section of story
                TextContainer.SetActive(false);
                audioManager.StartMusic();
                PersonContainer.GetComponent<MoveNormal>().MoveDown();
                InterludeContainer.GetComponent<MoveNormal>().MoveUp();
            }
        }
    }

    public void ShowChoice()
    {
        TextContainer.SetActive(false);
        ChoiceContainer.SetActive(true);
    }

    public void ChooseOne()
    {
        audioManager.PlayMenuSound();
        ChoiceContainer.SetActive(false);
        currText = 0;
        TextContainer.transform.localScale = new Vector3 (.1f, .1f, .1f);
        TextContainer.SetActive(true);
        TextContainer.GetComponent<GrowAndShrink>().StartEffect();
        TextContainerText.StartEffect( "CHLOE: ", intro2Text[currText]);
        StationText.text = "1 GHZ";
        StationContainer.SetActive(true);
    }

    public void ChooseTwo()
    {
        audioManager.PlayMenuSound();
        ChoiceContainer.SetActive(false);
        currText = 0;
        TextContainer.transform.localScale = new Vector3 (.1f, .1f, .1f);
        TextContainer.SetActive(true);
        TextContainer.GetComponent<GrowAndShrink>().StartEffect();
        TextContainerText.StartEffect( "CHLOE: ", intro2Text[currText]);
        StationText.text = "2 GHZ";
        StationContainer.SetActive(true);
    }
}

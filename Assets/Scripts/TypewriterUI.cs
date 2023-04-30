using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypewriterUI : MonoBehaviour
{
    AudioManager audioManager;

    TextMeshProUGUI HUDText;

    float typeTimer = 0;
    float typeTimerMax = .01f;
    string textHeader = "";
    string textToType = "";
    int textLength = 0;
    float clickTimer = 0;
    float clickTimerMax = .075f;
    float talkTimer = 0;
    float talkTimerMax = .1f;

    [SerializeField]
    Sprite[] PersonTalkAnimationFrames;
    [SerializeField]
    Image PersonTalk;
    int talkIndex = 0;

    void Start()
    {
        GameObject am = GameObject.Find("AudioManager");
        audioManager = am.GetComponent<AudioManager>();
        HUDText = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (typeTimer > 0)
        {
            typeTimer -= Time.deltaTime;
            if (typeTimer <= 0)
            {
                textLength++;
                HUDText.text = textHeader + textToType.Substring(0, textLength);
                if (textLength < textToType.Length)
                {
                    typeTimer = typeTimerMax;
                }
                else
                {
                    talkIndex = 0;
                    if (PersonTalk)
                        PersonTalk.sprite = PersonTalkAnimationFrames[talkIndex];
                }
            }
        }
        if (clickTimer > 0 && typeTimer > 0)
        {
            clickTimer -= Time.deltaTime;
            if (clickTimer <= 0)
            {
                audioManager.PlayClickSound();
                clickTimer = clickTimerMax;
            }
        }
        if (talkTimer > 0 && typeTimer > 0)
        {
            talkTimer -= Time.deltaTime;
            if (talkTimer <= 0)
            {
                talkTimer = Random.Range(talkTimerMax - .05f, talkTimerMax + .05f);
                talkIndex = talkIndex == 0 ? 1 : 0;
                if (PersonTalk)
                    PersonTalk.sprite = PersonTalkAnimationFrames[talkIndex];
            }
        }
    }

    public void SetPersonAnimationFrames(Sprite f1, Sprite f2)
    {
        PersonTalkAnimationFrames[0] = f1;
        PersonTalkAnimationFrames[1] = f2;
    }

    public void StartEffect(string staticText, string text, Sprite talk1, Sprite talk2)
    {
        textHeader = staticText;
        textToType = text;
        textLength = 0;
        typeTimer = typeTimerMax;
        clickTimer = clickTimerMax;
        talkTimer = talkTimerMax;
        PersonTalkAnimationFrames[0] = talk1;
        PersonTalkAnimationFrames[1] = talk2;
    }
}

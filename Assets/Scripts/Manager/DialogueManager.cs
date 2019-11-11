using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//일단 세세하게 알아야할 필요가없음
//ShowDialogue 함수와 ExitDialogue 함수가 받는 인자만 보고
//Update 함수만 보도록 하자
[System.Serializable]
public class Dialogue
{
    [Tooltip("말하고 있는 생물의 이미지")]
    public string[] Name;

    [Tooltip("텍스트 내용")]
    [TextArea(1, 2)]

    public string[] sentences;
    [Tooltip("말하고 있는 생물의 이미지")]
    public Sprite[] sprites;

    [Tooltip("대충 말풍선")]
    public Sprite[] dialogueWindows;
}


public class DialogueManager : MonoBehaviour
{
    public Text Name;                                                        
    public Text text;
    public SpriteRenderer rendererSprite;
    public SpriteRenderer rendererDialogueWindow;

    private List<string> listName;                                              
    private List<string> listSentences;
    private List<Sprite> listSprites;
    private List<Sprite> listDialogueCharacter;

    private int count; // 대화 진행 상황 카운트.

    public Animator animDialogueWindow;


    public bool talking = false;
    private bool keyActivated = false;

    // Use this for initialization
    void Start()
    {
        count = 0;
        Name.text = "";
        text.text = "";
        listName = new List<string>();
        listSentences = new List<string>();
        listSprites = new List<Sprite>();
        listDialogueCharacter = new List<Sprite>();
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        talking = true;

        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            listName.Add(dialogue.Name[i]);
            listSentences.Add(dialogue.sentences[i]);
            listSprites.Add(dialogue.sprites[i]);
            listDialogueCharacter.Add(dialogue.dialogueWindows[i]);
        }
        animDialogueWindow.SetBool("Appear", true);

        StartCoroutine(StartDialogueCoroutine());
    }

    IEnumerator StartDialogueCoroutine()
    {
        Name.text += listName[count];
        if (count > 0)
        {
            if (listDialogueCharacter[count] != listDialogueCharacter[count - 1])
            {
                animDialogueWindow.SetBool("Appear", false);

                yield return new WaitForSeconds(0.2f);
                rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueCharacter[count];
                rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                animDialogueWindow.SetBool("Appear", true);
            }
            else
            {
                if (listSprites[count] != listSprites[count - 1])
                {
                    yield return new WaitForSeconds(0.1f);
                    rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                }
                else
                {
                    yield return new WaitForSeconds(0.05f);
                }
            }

        }
        else
        {
            yield return new WaitForSeconds(0.05f);
            rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueCharacter[count];
            rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
        }

        keyActivated = true;

        for (int i = 0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i]; // 1글자씩 출력.
            yield return new WaitForSeconds(0.01f);
        }

    }

    public void ExitDialogue()
    {
        Name.text = "";
        text.text = "";
        count = 0;
        listName.Clear();
        listSentences.Clear();
        listSprites.Clear();
        listDialogueCharacter.Clear();
        animDialogueWindow.SetBool("Appear", false);
        talking = false;
    }

    void Update()
    {
        if (talking && keyActivated)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                keyActivated = false;
                count++;
                text.text = "";
                Name.text = "";

                if (count == listSentences.Count)
                {
                    StopAllCoroutines();
                    ExitDialogue();
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(StartDialogueCoroutine());
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStory : MonoBehaviour
{
    private OrderManager theOrder;

    public Animator IntroTop;
    public Animator introBottom;

    private CameraManager moveCamera;

    private DialogueManager dialogue;


    public Dialogue Tell;

    private void Awake()
    {
        theOrder = FindObjectOfType<OrderManager>();
        moveCamera = FindObjectOfType<CameraManager>();
        dialogue = FindObjectOfType<DialogueManager>();
    }

    private void Start()
    {
        StartCoroutine(startcoroutine());
    }

    IEnumerator startcoroutine()
    {
        theOrder.PlayerNotMove();

        introBottom.SetBool("Appear", true);
        IntroTop.SetBool("Appear", true);
        yield return new WaitForSeconds(3f);
 
        moveCamera.EventCameraMove("RIGHT", 10, 6);
        yield return new WaitUntil(() => moveCamera.eventCamera.CameraMoved == true);

        theOrder.Move("TutorialNPC", "LEFT",2,5);
        yield return new WaitForSeconds(2f);

        theOrder.Move("TutorialNPC", "RIGHT",2,5);
        yield return new WaitForSeconds(2f);

        moveCamera.EventCameraMove("LEFT", 4, 6);
        yield return new WaitUntil(() => moveCamera.eventCamera.CameraMoved == true);

        moveCamera.CameraMove();

        introBottom.SetBool("Appear", false);
        IntroTop.SetBool("Appear", false);
        
        dialogue.ShowDialogue(Tell);
        yield return new WaitForSeconds(2f);
        dialogue.ExitDialogue();
        yield return new WaitForSeconds(2f);
        
        theOrder.PlayerMove();

        this.gameObject.GetComponent<StartStory>().enabled = false;
    }

}

using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class EndCutScene : MonoBehaviour
{
    public int endNumber;
    public Inventory inventory;
    public ItemData flower;
    public GameObject phantom;
    public GameObject player;

    public CinemachineCamera graveyardCam;

    public string[] end1dialogue;
    public string[] end2dialogue;
    public string[] end3dialogue;

    private bool started = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!started && Input.GetKey(KeyCode.E))
        {
            started = true;
            StartCoroutine(StartEndCutscene());
        }
    }

    private IEnumerator StartEndCutscene()
    {
        ScreenFlash.Instance.FadeIn(Color.black, 2f);
        AudioManager.Instance.PlayMusic("Ghost");
        yield return new WaitForSeconds(3f);
        graveyardCam.enabled = true;
        ScreenFlash.Instance.FadeOut(Color.black, 1f);

        if (inventory.items.Contains(flower))
        {
            endNumber = 1;
        }
        else if (NPC.sacrifice != "Daisy")
        {
            endNumber = 2;
        }
        else if (NPC.sacrifice == "Daisy")
        {
            endNumber = 3;
        }
        Debug.Log("Ending is " + endNumber);


        switch (endNumber)
        {
            case 1:
                {
                    StartCoroutine(Ending1());
                    break;
                }
            case 2:    // END 2, player brings any NPC other than DAISY
                {
                    StartCoroutine(Ending2());
                    break;
                }
            case 3:    // END 3, player brings DAISY
                {
                    StartCoroutine(Ending3());
                    break;
                }
        }
    }


    IEnumerator Ending1() // END 1, player has flower in inv
    {
        DialogueManager.Instance.ShowDialogue(end1dialogue);
        yield return new WaitUntil(() => DialogueManager.Instance.isDialogueActive == false);
        ScreenFlash.Instance.FadeIn(Color.black, 0.5f);
    }
    IEnumerator Ending2() // END 2, player brings any NPC other than DAISY
    {
        DialogueManager.Instance.ShowDialogue(end2dialogue);
        yield return new WaitUntil(() => DialogueManager.Instance.isDialogueActive == false);
        ScreenFlash.Instance.FadeIn(Color.white, 0.2f);
    }

    IEnumerator Ending3() // END 3, player brings DAISY
    {
        DialogueManager.Instance.ShowDialogue(end3dialogue);
        yield return new WaitUntil(() => DialogueManager.Instance.isDialogueActive == false);
        ScreenFlash.Instance.FadeIn(Color.white, 0.2f);
    }
}


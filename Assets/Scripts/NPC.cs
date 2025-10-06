using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public static string sacrifice;

    [Header("References")]
    [SerializeField] private GameObject exclamation;
    [SerializeField] private ItemData flower;
    [SerializeField] private GameObject graveSpot;

    [Header("Dialogue")]
    [TextArea]
    [SerializeField] private string[] firstDialogue;
    [SerializeField] private string[] flowerDialogue;

    [Header("Player gives Item")]
    [SerializeField] private ItemData wantedItem;
    [TextArea]
    [SerializeField] private string[] correctItemDialogue;
    [TextArea]
    [SerializeField] private string[] wrongItemDialogue;

    [Header("NPC drops Item after dialogue")]
    [SerializeField] private bool requireCorrectItem = false;
    [SerializeField] private bool dropAlways = false;
    [SerializeField] private GameObject itemDrop = null;

    private void Awake()
    {
        if (itemDrop != null)
        {
            itemDrop.SetActive(false);
        }
    }

    private bool active = false;
    [HideInInspector] public bool playerNearby = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        exclamation.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        exclamation.SetActive(false);
        active = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!active && Input.GetKey(KeyCode.E))
        {
            Interact();
            active = true;
        }
    }

    private void Interact()
    {
        //Basic NPCs
        DialogueManager.Instance.ShowDialogue(firstDialogue);
        if (itemDrop != null && dropAlways) StartCoroutine(DropItem());
    }

    public void ReceiveItem(ItemData item)
    {
        if (item == flower)
        {
            AudioManager.Instance.PlayMusic("Sacrifice");
            Instantiate(gameObject, graveSpot.transform.position, graveSpot.transform.rotation);
            sacrifice = gameObject.name;
            DialogueManager.Instance.ShowDialogue(flowerDialogue);
        }
        else
        {
            if (item == wantedItem)
            {
                DialogueManager.Instance.ShowDialogue(correctItemDialogue);

                if (itemDrop != null && requireCorrectItem == true)
                {
                    StartCoroutine(DropItem());
                }
            }
            else
            {
                DialogueManager.Instance.ShowDialogue(wrongItemDialogue);
            }

            if (itemDrop != null && requireCorrectItem == false)
            {
                StartCoroutine(DropItem());
            }
        }
    }

    private IEnumerator DropItem()
    {
        yield return new WaitUntil(() => DialogueManager.Instance.isDialogueActive == false);
        itemDrop.SetActive(true);
    }
}


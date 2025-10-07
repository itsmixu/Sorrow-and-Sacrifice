using UnityEngine;

public class CharacterSheetList : MonoBehaviour
{
    public static CharacterSheetList Instance;

    [SerializeField] private GameObject charSheetPrefab;

    private Transform slotsParent;
    private CharSheetUI[] slots;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        slotsParent = transform;

        // Create slots in UI
        slots = new CharSheetUI[14];
        for (int i = 0; i < slots.Length; i++)
        {
            GameObject slotGO = Instantiate(charSheetPrefab, slotsParent);
            slots[i] = slotGO.GetComponent<CharSheetUI>();
            //slots[i].ClearSlot();
        }
    }

    public void AddToList()
    {

    }
}

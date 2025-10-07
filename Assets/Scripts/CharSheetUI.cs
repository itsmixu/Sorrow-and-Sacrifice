using UnityEngine;
using UnityEngine.UI;

public class CharSheetUI : MonoBehaviour
{
    public static bool popupActive;

    [SerializeField] private Image faceImage;
    [SerializeField] private Image nameImage;
    [SerializeField] private GameObject popup;

    public void SetItem(CharacterSheet item)
    {
        faceImage.sprite = item.image;
        nameImage.sprite = item.name;
    }

    public void ClearSlot()
    {
        faceImage.sprite = null;
        nameImage.sprite = null;
    }

    public void TogglePopup()
    {
        if (!popupActive)
        {
            popup.SetActive(true);
            popupActive = true;
        }
        else if (popup.activeInHierarchy)
        {
            popup.SetActive(false);
            popupActive = false;
        }
        
    }
}


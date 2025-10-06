using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterSheet", menuName = "Inventory/Character Sheet")]
public class CharacterSheet : ScriptableObject
{
    public string name;
    public Sprite image;
}

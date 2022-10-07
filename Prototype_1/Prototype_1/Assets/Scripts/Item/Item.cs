using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    NormalItem, RareItem, EpicItem, Readable
}

public enum CollectionType
{
    Collectible, NotCollectible
}
public class Item : MonoBehaviour
{
    public string Name;
    public bool Collected;

    public ItemType ItemType;
    public CollectionType CollectionType;

    private readonly List<string> _nouns = new()
    {
        "Crystal",
        "Stone",
        "Tablet",
        "Book",
        "Scroll",
        "Sword",
        "Dagger",
        "Hammer",
        "Cloak",
        "Helmet"
    };
    private readonly List<string> _adjectives = new()
    {
        "Flame",
        "Magic",
        "Evil",
        "Ice",
        "Terror",
        "Invisibility",
        "Loss",
        "Remembrance",
        "Life",
        "Death"
    };

    void Start()
    {
        //assign random item type
        int randomItem = Random.Range(0, 4);
        switch (randomItem)
        {
            case 0:
                ItemType = ItemType.NormalItem;
                break;
            case 1:
                ItemType = ItemType.RareItem;
                break;
            case 2:
                ItemType = ItemType.EpicItem;
                break;
            case 3:
                ItemType = ItemType.Readable;
                break;
        }

        //assign random collection type
        int randomCollect = Random.Range(0, 8);
        switch (randomCollect)
        {
            case 0:
                CollectionType = CollectionType.Collectible;
                break;
            case 1:
                CollectionType = CollectionType.Collectible;
                break;
            case 2:
                CollectionType = CollectionType.Collectible;
                break;
            case 3:
                CollectionType = CollectionType.Collectible;
                break;
            case 4:
                CollectionType = CollectionType.Collectible;
                break;
            case 5:
                CollectionType = CollectionType.NotCollectible;
                break;
            case 6:
                CollectionType = CollectionType.NotCollectible;
                break;
        }

        Name = ItemName();
    } 

    string ItemName()
    {
        string itemName = $"{_nouns[Random.Range(0, _nouns.Count)]} of {_adjectives[Random.Range(0, _adjectives.Count)]}";
        return itemName;
    }

}

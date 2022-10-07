using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    private Journal _journal;
    public AudioClip CollectSound;
    public AudioClip DidNotCollect;

    void Start()
    {
        _journal = GetComponent<Journal>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Dig(other);
        Collect(other);
    }

    //If player touches wall, break wall
    void Dig(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            other.gameObject.GetComponent<Tile>().Health--;
            if (other.gameObject.GetComponent<Tile>().Health <= 0)
            {
                //break wall
                other.gameObject.GetComponent<Collider2D>().enabled = false;
                other.gameObject.GetComponent<Tile>().TileType = Type.Floor;
            }
        }
    }
    //if player touches item, collect (is possible)
    void Collect(Collider2D other)
    {
        var item = other.gameObject.GetComponent<Item>();
        if (other.gameObject.CompareTag("Item") && item.CollectionType == CollectionType.Collectible && !item.Collected)
        {
            _journal.AddToJournal($"Collected {item.Name}!", true);
            item.Collected = true;
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(CollectSound);
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (other.gameObject.CompareTag("Item") && item.CollectionType == CollectionType.NotCollectible)
        {
            _journal.AddToJournal($"Could not collect {item.Name}, it was stuck!", true);
            item.Collected = false;
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(DidNotCollect);
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}

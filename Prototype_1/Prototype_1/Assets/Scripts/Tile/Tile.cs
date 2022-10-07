using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Floor, Wall
}
public class Tile : MonoBehaviour
{
    public int Health;
    public Type TileType;

    public Sprite Floor;
    public Sprite Wall;

    void FixedUpdate()
    {
        if (TileType == Type.Floor)
            GetComponent<SpriteRenderer>().sprite = Floor;
        if (TileType == Type.Wall)
            GetComponent<SpriteRenderer>().sprite = Wall;
    }
}

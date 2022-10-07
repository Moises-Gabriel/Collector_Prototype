using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dig : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D wall)
    {
        if (wall)
        {
            wall.gameObject.GetComponent<Tile>().Health--;
            if (wall.gameObject.GetComponent<Tile>().Health <= 0)
            {
                //break wall
                wall.gameObject.GetComponent<Collider2D>().enabled = false;
                wall.gameObject.GetComponent<Tile>().TileType = Type.Floor;
            }
        }
    }
}

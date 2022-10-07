using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public Vector2 WorldSize;
    public GameObject Cell;
    public Sprite FloorTile;

    public List<GameObject> Layers;

    public GameObject Player;
    public GameObject Exit;
    public GameObject Item;
    public int ItemSpawnCount;

    private readonly List<GameObject> _worldCells = new();
    private readonly List<GameObject> _wallCells = new();
    private readonly List<GameObject> _borderCells = new();
    private List<GameObject> _emptyCells = new();


    void Start()
    {
        Generate();
        Spawn();
    }

    void Generate()
    {
        //Generate World
        for (int x = 0; x < WorldSize.x; x++)
        {
            for (int y = 0; y < WorldSize.y; y++)
            {
                var worldCell = Instantiate(Cell, new Vector3(x, y, 0), Quaternion.identity, Layers[0].transform);
                _worldCells.Add(worldCell);
            }
        }

        //Color cell
        foreach (var cell in _worldCells)
        {
            cell.GetComponent<SpriteRenderer>().sortingOrder = 5;
            Color[] randomColor =
            {
                Color.black, Color.grey
            };
            cell.GetComponent<SpriteRenderer>().color = randomColor[Random.Range(0, randomColor.Length)];

            if (cell.GetComponent<SpriteRenderer>().color == Color.grey)
            {
                _emptyCells.Add(cell);
            }

            if (cell.GetComponent<SpriteRenderer>().color == Color.black)
            {
                _wallCells.Add(cell);
            }
        }
        
        //Add map border
        Edge();

        //Cell components
        foreach (var cell in _wallCells)
        {
            cell.GetComponent<SpriteRenderer>().color = Color.white;

            cell.AddComponent<BoxCollider2D>();
            cell.GetComponent<BoxCollider2D>().isTrigger = true;

            cell.GetComponent<Tile>().Health = Random.Range(2,5);
            cell.GetComponent<Tile>().TileType = Type.Wall;

            cell.name = "Wall";
            cell.tag = "Wall";
        }

        foreach (var cell in _borderCells)
        {
            cell.GetComponent<SpriteRenderer>().color = Color.white;

            cell.GetComponent<Tile>().Health = 10000;
            cell.GetComponent<Tile>().TileType = Type.Wall;

            cell.GetComponent<BoxCollider2D>().isTrigger = true;

            cell.name = "Border";
            cell.tag = "Wall";
        }

        foreach (var cell in _emptyCells)
        {
            cell.GetComponent<SpriteRenderer>().sprite = FloorTile;
            cell.GetComponent<SpriteRenderer>().color = Color.white;

            cell.GetComponent<Tile>().Health = 0;
            cell.GetComponent<Tile>().TileType = Type.Floor;

            cell.name = "Floor";
        }
    }
    void Edge()
    {
        //Add map edge
        for (int x = 0; x < WorldSize.x + 2; x++)
        {
            var worldCell = Instantiate(Cell, new Vector3(x - 1, -1, 0), Quaternion.identity, Layers[2].transform);
            worldCell.GetComponent<SpriteRenderer>().color = Color.black;
            worldCell.AddComponent<BoxCollider2D>();
            _borderCells.Add(worldCell);
        }

        //Add map edge
        for (int x = 0; x < WorldSize.x + 2; x++)
        {
            var worldCell = Instantiate(Cell, new Vector3(x - 1, WorldSize.y, 0), Quaternion.identity, Layers[2].transform);
            worldCell.GetComponent<SpriteRenderer>().color = Color.black;
            worldCell.AddComponent<BoxCollider2D>();
            _borderCells.Add(worldCell);
        }

        //Add map edge
        for (int y = 0; y < WorldSize.y + 2; y++)
        {
            var worldCell = Instantiate(Cell, new Vector3(WorldSize.x, y - 1, 0), Quaternion.identity, Layers[2].transform);
            worldCell.GetComponent<SpriteRenderer>().color = Color.black;
            worldCell.AddComponent<BoxCollider2D>();
            _borderCells.Add(worldCell);
        }

        //Add map edge
        for (int y = 0; y < WorldSize.y + 2; y++)
        {
            var worldCell = Instantiate(Cell, new Vector3(WorldSize.x - (WorldSize.x + 1), y - 1, 0), Quaternion.identity, Layers[2].transform);
            worldCell.GetComponent<SpriteRenderer>().color = Color.black;
            worldCell.AddComponent<BoxCollider2D>();
            _borderCells.Add(worldCell);
        }
    }

    void Spawn()
    {
        //pick random spot
        GameObject playerSpawnPoint = _emptyCells[Random.Range(0, _emptyCells.Count)];
        _emptyCells.Remove(playerSpawnPoint);

        //spawn player
        Instantiate(Player, playerSpawnPoint.transform.position, Quaternion.identity, Layers[1].transform);

        //spawn items
        for (int i = 0; i < Random.Range(3, ItemSpawnCount + 1); i++)
        {
            GameObject itemSpawnPoint = _emptyCells[Random.Range(0, _emptyCells.Count)];
            _emptyCells.Remove(itemSpawnPoint);

            //spawn item
            Instantiate(Item, itemSpawnPoint.transform.position, Quaternion.identity, Layers[1].transform);
        }

        ////pick random spot
        //GameObject exitSpawnPoint = _emptyCells[Random.Range(0, _emptyCells.Count)];
        //_emptyCells.Remove(exitSpawnPoint);

        ////spawn exit
        //Instantiate(Exit, exitSpawnPoint.transform.position, Quaternion.identity, Layers[1].transform);
    }
}

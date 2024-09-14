using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "Tile", order = 0)]
public class Tile : ScriptableObject {
    
    public enum TileMaterial{
        Grass,
        Water,
    }

    public Sprite sprite;
    public TileMaterial material;
    public TileType type;
    public bool usable = false;
    public int generationWeight = 1;
    public bool walkable = true;
    public bool isDestructible = true;
    public int durability;

    public List<TileType> north = new List<TileType>();
    public List<TileType> east = new List<TileType>();
    public List<TileType> south = new List<TileType>();
    public List<TileType> west = new List<TileType>();

    public List<TileType> GetValidTiles(Direction d){
        switch (d){
            case Direction.North:
                return north;
            case Direction.East:
                return east;
            case Direction.South:
                return south;
            case Direction.West:
                return west;
            default:
                return new List<TileType>();
        }


    }

}


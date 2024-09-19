using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "WFCNode", menuName = "WFCNode", order = 0)]
public class WFCNode : ScriptableObject {
    
    public enum TileMaterial{
        Grass,
        Water,
    }

    public Tile tile;
    public TileMaterial material;
    public TileType type;
    public bool usable = false;
    public int generationWeight = 1;
    public bool walkable = true;
    public bool isDestructible = true;
    public int durability;

    public List<WFCNode> north = new List<WFCNode>();
    public List<WFCNode> east = new List<WFCNode>();
    public List<WFCNode> south = new List<WFCNode>();
    public List<WFCNode> west = new List<WFCNode>();

    public void Copy(WFCNode node){
        this.name = node.name;
        this.tile = node.tile;
        this.material = node.material;
        this.type = node.type;
        this.usable = node.usable;
        this.generationWeight = node.generationWeight;
        this.walkable = node.walkable;
        this.isDestructible = node.isDestructible;
        this.durability = node.durability;

        foreach (WFCNode tile in node.north){
            this.north.Add(tile);
        }
        foreach (WFCNode tile in node.south){
            this.south.Add(tile);
        }
        foreach (WFCNode tile in node.east){
            this.east.Add(tile);
        }
        foreach (WFCNode tile in node.west){
            this.west.Add(tile);
        }
        
    }
    public List<WFCNode> GetValidTiles(Direction d){
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
                return new List<WFCNode>();
        }


    }

}


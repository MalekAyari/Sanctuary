using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public enum Direction{
    North,
    East,
    South,
    West,
}

public enum TileType{   
    NW_Corner,
    W_Line,
    SW_Corner,
    E_Peak,
    N_Peak,
    S_Peak,
    Diamond,
    SE_Diagonal,
    NE_Diagonal,
    N_Line,
    BaseTile,
    S_Line,
    W_Peak,
    R_Diamond,
    SW_Diagonal,
    NW_Diagonal,
    NE_Corner,
    E_Line,
    SE_Corner,
    Vertical,
    Horizontal,
    N_D_Corners,
    E_D_Corners,
    NW_R_Corner,
    SW_R_Corner,
    NW_R_ExtraCorner,
    SW_R_ExtraCorner,
    SE_Peak,
    NE_Peak,
    W_D_Corners,
    S_D_Corners,
    NE_R_Corner,
    SE_R_Corner,
    NE_R_ExtraCorner,
    SE_R_ExtraCorner,
    SW_Peak,
    NW_Peak,
    
}


public class RulePopulator
{

    public class Rule
    {
        public Direction direction;
        public TileType type;

        public Rule(Direction direction, TileType type){
            this.direction = direction;
            this.type = type;
        }
    }
    
    private string folderPath;
    private Dictionary<TileType, List<Rule>> RuleSet = new Dictionary<TileType, List<Rule>>();
    
    public RulePopulator(){
        RuleSet[TileType.S_Line] = new List<Rule>{
            new Rule(Direction.South, TileType.NW_Corner),
            new Rule(Direction.South, TileType.NE_Corner),
            new Rule(Direction.South, TileType.N_Line),

            new Rule(Direction.East, TileType.SE_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.S_Line),

            new Rule(Direction.West, TileType.S_Line),
            new Rule(Direction.West, TileType.SW_Corner),
            new Rule(Direction.West, TileType.NW_Diagonal),

            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.BaseTile),
            new Rule(Direction.North, TileType.SW_Diagonal),
            new Rule(Direction.North, TileType.SE_Diagonal),
        };
        RuleSet[TileType.N_Line] = new List<Rule>{
            new Rule(Direction.West, TileType.NW_Corner),
            new Rule(Direction.West, TileType.SW_Diagonal),
            new Rule(Direction.West, TileType.N_Line),

            new Rule(Direction.East, TileType.N_Line),
            new Rule(Direction.East, TileType.NE_Corner),
            new Rule(Direction.East, TileType.SE_Diagonal),

            new Rule(Direction.North, TileType.S_Line),
            new Rule(Direction.North, TileType.SE_Corner),
            new Rule(Direction.North, TileType.SW_Corner),

            new Rule(Direction.South, TileType.S_Line),
            new Rule(Direction.South, TileType.BaseTile),
            new Rule(Direction.South, TileType.NW_Diagonal),
            new Rule(Direction.South, TileType.NE_Diagonal),
        };
        RuleSet[TileType.E_Line] = new List<Rule>{
            new Rule(Direction.West, TileType.NE_Diagonal),
            new Rule(Direction.West, TileType.SE_Diagonal),
            new Rule(Direction.West, TileType.BaseTile),
            new Rule(Direction.West, TileType.W_Line),

            new Rule(Direction.North, TileType.NE_Corner),
            new Rule(Direction.North, TileType.E_Line),
            new Rule(Direction.North, TileType.NE_Diagonal),

            new Rule(Direction.South, TileType.SW_Diagonal),
            new Rule(Direction.South, TileType.E_Line),
            new Rule(Direction.South, TileType.SE_Corner),

            new Rule(Direction.East, TileType.W_Line),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.SW_Corner),
        };
        RuleSet[TileType.W_Line] = new List<Rule>{
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.W_Line),
            new Rule(Direction.North, TileType.NE_Diagonal),

            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),

            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.SW_Diagonal),
            new Rule(Direction.East, TileType.BaseTile),
            new Rule(Direction.East, TileType.E_Line),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
        };
        RuleSet[TileType.BaseTile] = new List<Rule>{
            new Rule(Direction.North, TileType.SW_Diagonal),
            new Rule(Direction.North, TileType.SE_Diagonal),
            new Rule(Direction.North, TileType.BaseTile),
            new Rule(Direction.North, TileType.N_Line),

            new Rule(Direction.East, TileType.SW_Diagonal),
            new Rule(Direction.East, TileType.NW_Diagonal),
            new Rule(Direction.East, TileType.BaseTile),
            new Rule(Direction.East, TileType.E_Line),

            new Rule(Direction.West, TileType.NE_Diagonal),
            new Rule(Direction.West, TileType.SE_Diagonal),
            new Rule(Direction.West, TileType.BaseTile),
            new Rule(Direction.West, TileType.W_Line),

            new Rule(Direction.South, TileType.NE_Diagonal),
            new Rule(Direction.South, TileType.NW_Diagonal),
            new Rule(Direction.South, TileType.BaseTile),
            new Rule(Direction.South, TileType.S_Line),
        };
        RuleSet[TileType.NW_Corner] = new List<Rule>{
            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.SE_Corner),
            new Rule(Direction.North, TileType.S_Line),
            
            new Rule(Direction.East, TileType.N_Line),
            new Rule(Direction.East, TileType.NE_Corner),
            new Rule(Direction.East, TileType.SE_Diagonal),
            
            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
        };
        RuleSet[TileType.SE_Corner] = new List<Rule>{
            new Rule(Direction.South, TileType.NW_Corner),
            new Rule(Direction.South, TileType.N_Line),
            new Rule(Direction.South, TileType.NE_Corner),

            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.W_Line),
            new Rule(Direction.East, TileType.SW_Corner),

            new Rule(Direction.West, TileType.S_Line),
            new Rule(Direction.West, TileType.SW_Corner),
            new Rule(Direction.West, TileType.NW_Diagonal),

            new Rule(Direction.North, TileType.W_Line),
            new Rule(Direction.North, TileType.NE_Corner),
            new Rule(Direction.North, TileType.NW_Diagonal),
            
        };
        RuleSet[TileType.NE_Corner] = new List<Rule>{
            new Rule(Direction.North, TileType.S_Line),
            new Rule(Direction.North, TileType.SE_Corner),
            new Rule(Direction.North, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NW_Corner),
            new Rule(Direction.West, TileType.N_Line),
            new Rule(Direction.West, TileType.SW_Diagonal),

            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.SW_Diagonal),

            new Rule(Direction.South, TileType.E_Line),
            new Rule(Direction.South, TileType.SE_Corner),
            new Rule(Direction.South, TileType.SW_Diagonal),
        };
        RuleSet[TileType.SW_Corner] = new List<Rule>{
            new Rule(Direction.South, TileType.NW_Corner),
            new Rule(Direction.South, TileType.N_Line),
            new Rule(Direction.South, TileType.NE_Corner),

            new Rule(Direction.East, TileType.S_Line),
            new Rule(Direction.East, TileType.SE_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),

            new Rule(Direction.North, TileType.W_Line),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.NE_Diagonal),

            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.W_Line),
            new Rule(Direction.West, TileType.SE_Diagonal),
        }; 
        RuleSet[TileType.SE_Diagonal] = new List<Rule>{
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.NE_Diagonal),
            new Rule(Direction.North, TileType.W_Line),

            new Rule(Direction.South, TileType.S_Line),
            new Rule(Direction.South, TileType.NE_Diagonal),
            new Rule(Direction.South, TileType.BaseTile),
            new Rule(Direction.South, TileType.NW_Diagonal),
            
            new Rule(Direction.West, TileType.NW_Corner),
            new Rule(Direction.West, TileType.N_Line),
            new Rule(Direction.West, TileType.NE_Diagonal),

            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.SE_Diagonal),
            new Rule(Direction.East, TileType.BaseTile),
            new Rule(Direction.East, TileType.E_Line),
        };
        RuleSet[TileType.NE_Diagonal] = new List<Rule>{
            new Rule(Direction.North, TileType.BaseTile),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.SW_Diagonal),
            new Rule(Direction.North, TileType.SE_Diagonal),

            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SE_Diagonal),

            new Rule(Direction.West, TileType.S_Line),
            new Rule(Direction.West, TileType.SW_Corner),
            new Rule(Direction.West, TileType.NW_Diagonal),

            new Rule(Direction.East, TileType.BaseTile),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.SW_Diagonal),
            new Rule(Direction.East, TileType.NW_Diagonal),
        };
        RuleSet[TileType.SW_Diagonal] = new List<Rule>{
            new Rule(Direction.North, TileType.NE_Corner),
            new Rule(Direction.North, TileType.E_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),

            new Rule(Direction.East, TileType.N_Line),
            new Rule(Direction.East, TileType.NE_Corner),
            new Rule(Direction.East, TileType.SE_Diagonal),

            new Rule(Direction.West, TileType.NE_Diagonal),
            new Rule(Direction.West, TileType.BaseTile),
            new Rule(Direction.West, TileType.W_Line),
            new Rule(Direction.West, TileType.SE_Diagonal),

            new Rule(Direction.South, TileType.S_Line),
            new Rule(Direction.South, TileType.NE_Diagonal),
            new Rule(Direction.South, TileType.NW_Diagonal),
            new Rule(Direction.South, TileType.BaseTile),
        };
        RuleSet[TileType.NW_Diagonal] = new List<Rule>{
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.SW_Diagonal),
            new Rule(Direction.North, TileType.SE_Diagonal),
            new Rule(Direction.North, TileType.BaseTile),

            new Rule(Direction.South, TileType.SE_Corner),
            new Rule(Direction.South, TileType.SW_Diagonal),
            new Rule(Direction.South, TileType.E_Line),

            new Rule(Direction.West, TileType.NE_Diagonal),
            new Rule(Direction.West, TileType.BaseTile),
            new Rule(Direction.West, TileType.W_Line),
            new Rule(Direction.West, TileType.SE_Diagonal),

            new Rule(Direction.East, TileType.S_Line),
            new Rule(Direction.East, TileType.SE_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
        };
    }

    public void SetValidNeighbors(Tile tile){
    
        if (!RuleSet.ContainsKey(tile.type)){
            Debug.LogError($"RuleSet does not contain any rules for tile type {tile.type}");
            return;
        }

        foreach (Rule rule in RuleSet[tile.type]){
            switch (rule.direction){
                case Direction.North:
                    tile.north.Add(rule.type);
                    break;
                case Direction.South:
                    tile.south.Add(rule.type);
                    break;
                case Direction.East:
                    tile.east.Add(rule.type);
                    break;
                case Direction.West:
                    tile.west.Add(rule.type);
                    break;
            }
        }
        
        tile.usable = true;
    }
}

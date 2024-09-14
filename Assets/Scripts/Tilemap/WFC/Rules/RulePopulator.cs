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
        RuleSet[TileType.NW_Corner] = new List<Rule>{
            new Rule(Direction.East, TileType.N_Line),
            new Rule(Direction.East, TileType.NE_Corner),
            new Rule(Direction.East, TileType.SE_Diagonal),
            new Rule(Direction.East, TileType.S_D_Corners),
            new Rule(Direction.East, TileType.SE_R_Corner),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.NW_Peak),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.N_Line] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.NE_Corner] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.SE_R_Corner] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.SW_R_Corner] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.W_Line] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.BaseTile] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.E_Line] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.NE_R_Corner] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.NW_Corner] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.SW_Corner] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.S_Line] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.SE_Corner] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.SE_R_ExtraCorner] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.SW_R_ExtraCorner] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.E_Peak] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.W_Peak] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.Vertical] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.NE_R_ExtraCorner] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.NW_R_ExtraCorner] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.N_Peak] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.S_Peak] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.Horizontal] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.SE_Peak] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.SW_Peak] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.Diamond] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            

            

            

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.R_Diamond] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.NE_Peak] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.NW_Peak] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.NE_Diagonal] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.NW_Diagonal] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.SE_Diagonal] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.SW_Diagonal] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.NE_Corner] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.N_D_Corners] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.E_D_Corners] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.S_D_Corners] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
        };
        RuleSet[TileType.W_D_Corners] = new List<Rule>{
            new Rule(Direction.East, TileType.SW_Corner),
            new Rule(Direction.East, TileType.NW_Corner),
            new Rule(Direction.East, TileType.NE_Diagonal),
            new Rule(Direction.East, TileType.E_D_Corners),
            new Rule(Direction.East, TileType.E_Line),
            new Rule(Direction.East, TileType.NW_Peak),

            new Rule(Direction.South, TileType.W_Line),
            new Rule(Direction.South, TileType.SW_Corner),
            new Rule(Direction.South, TileType.SE_Diagonal),
            new Rule(Direction.South, TileType.E_D_Corners),
            new Rule(Direction.South, TileType.SE_R_Corner),
            new Rule(Direction.South, TileType.SW_Corner),

            new Rule(Direction.West, TileType.NE_Corner),
            new Rule(Direction.West, TileType.SE_Corner),
            new Rule(Direction.West, TileType.E_Line),
            new Rule(Direction.West, TileType.NW_Diagonal),
            new Rule(Direction.West, TileType.Diamond),
            new Rule(Direction.West, TileType.W_D_Corners),
            new Rule(Direction.West, TileType.W_D_Corners),

            new Rule(Direction.North, TileType.SW_Corner),
            new Rule(Direction.North, TileType.NW_Corner),
            new Rule(Direction.North, TileType.N_Line),
            new Rule(Direction.North, TileType.NW_Diagonal),
            new Rule(Direction.North, TileType.Diamond),
            new Rule(Direction.North, TileType.N_D_Corners),
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
    }
}

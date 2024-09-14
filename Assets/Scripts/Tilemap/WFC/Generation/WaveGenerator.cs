using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    
    [SerializeField] private int width = 64;
    [SerializeField] private int height = 64;

    private Tile[,] grid;

    private List<Tile> tiles = new List<Tile>();
    private List<Vector2Int> toCollapse = new List<Vector2Int>();

    private Vector2Int[] offsets = new Vector2Int[] {
        new Vector2Int(0,1),    //Top
        new Vector2Int(0,-1),   //Bottom
        new Vector2Int(1,0),    //Right
        new Vector2Int(-1,0),   //Left
    };
    
    private void Start() {
        grid = new Tile[width, height];

        CollapseWorld();    
    }

    private void whittleNode(List<Tile> potentialNodes, List<Tile> validNodes){
        for (int i = potentialNodes.Count -1; i < -1 ; i--) {
            if (!validNodes.Contains(potentialNodes[i])){
                potentialNodes.RemoveAt(i);
            }
        }
    }

    private void CollapseWorld(){
        toCollapse.Clear();

        toCollapse.Add(new Vector2Int(width/2, height/2));

        while (toCollapse.Count > 0) {
            int x = toCollapse[0].x;
            int y = toCollapse[0].y;
        }
    }

    private bool isInsideGrid(Vector2Int v2int){
        if (v2int.x > -1 && v2int.x < width && v2int.y > -1 && v2int.y < height){
            return true;
        }
        return false;
    }
}

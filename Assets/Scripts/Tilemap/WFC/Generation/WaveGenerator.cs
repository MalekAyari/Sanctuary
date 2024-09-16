using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class WaveGenerator : MonoBehaviour
{
    
    [SerializeField] private Tilemap map;
    [SerializeField] private int width = 64;
    [SerializeField] private int height = 64;
    

    private WFCNode[,] grid;
    private List<WFCNode> tiles = new List<WFCNode>();
    private List<Vector2Int> toCollapse = new List<Vector2Int>();

    private Vector2Int[] offsets = new Vector2Int[] {
        new Vector2Int(0,1),    //Top
        new Vector2Int(0,-1),   //Bottom
        new Vector2Int(1,0),    //Right
        new Vector2Int(-1,0),   //Left
    };
    
    public List<WFCNode> GetTiles(){
        List<WFCNode> tiles = new List<WFCNode>();
        string[] guids = AssetDatabase.FindAssets("t:WFCNode");

        foreach (string guid in guids){
            WFCNode tile = AssetDatabase.LoadAssetAtPath<WFCNode>(AssetDatabase.GUIDToAssetPath(guid));
            if (tile.north.Count > 0) {
                tiles.Add(tile);
            }
        }

        return tiles;
    }

    private void Start() {
        grid = new WFCNode[width, height];
        tiles = GetTiles();

        CollapseWorld();    
    }

    private void WhittleNode(List<WFCNode> potentialNodes, List<WFCNode> validNodes){

        for (int i = potentialNodes.Count -1; i > -1 ; i--) {

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

            List<WFCNode> potentialNodes = new List<WFCNode>(tiles);

            for (int i = 0; i < offsets.Length; i ++) {
                Vector2Int neighbor = new Vector2Int(x + offsets[i].x, y + offsets[i].y);

                if (isInsideGrid(neighbor)){
                    WFCNode neighborNode = grid[neighbor.x, neighbor.y];

                    if (neighborNode != null){
                        switch (i){
                            case 0:
                                WhittleNode(potentialNodes, neighborNode.south);
                                break;
                            case 1:
                                WhittleNode(potentialNodes, neighborNode.north);
                                break;
                            case 2:
                                WhittleNode(potentialNodes, neighborNode.west);
                                break;
                            case 3:
                                WhittleNode(potentialNodes, neighborNode.east);
                                break;
                        }
                    } else {
                        if (!toCollapse.Contains(neighbor)) toCollapse.Add(neighbor);
                    }
                }
            }

            if (potentialNodes.Count <= 0) {
                grid[x,y] = null;
                Debug.Log("No nodes available to collapse");
            } else {
                grid[x,y] = potentialNodes[UnityEngine.Random.Range(0, potentialNodes.Count - 1)];
            }

            //Draw cell
            map.SetTile(new Vector3Int(x,y,0), grid[x,y].tile);
            Debug.Log("collapsed cell into a: " + grid[x,y].name);

            toCollapse.RemoveAt(0);

        }
        Debug.Log("collapsed world");
    }


    

    private bool isInsideGrid(Vector2Int v2int){
        if (v2int.x > -1 && v2int.x < width && v2int.y > -1 && v2int.y < height){
            return true;
        }
        return false;
    }
}

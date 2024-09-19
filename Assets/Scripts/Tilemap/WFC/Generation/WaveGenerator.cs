using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class WaveGenerator : MonoBehaviour
{
    
    [SerializeField] private Tilemap map;
    [SerializeField] private int width = 64;
    [SerializeField] private int height = 64;
    [SerializeField] private float delay = 0.6f;
    [SerializeField] private List<WFCNode> tiles = new List<WFCNode>();
    public Tile placeholder;
    public Tile background;

    private WFCNode[,] grid;
    private List<Vector2Int> toCollapse = new List<Vector2Int>();

    private Vector2Int[] offsets = new Vector2Int[] {
        new Vector2Int(0,1),    //Top
        new Vector2Int(0,-1),   //Bottom
        new Vector2Int(1,0),    //Right
        new Vector2Int(-1,0),   //Left
    };
    
    public void LoadTilesFromDirectory()
    {
        tiles.Clear(); // Clear list first to avoid duplicates

        string[] guids = AssetDatabase.FindAssets("t:WFCNode");

        foreach (string guid in guids)
        {
            WFCNode tile = AssetDatabase.LoadAssetAtPath<WFCNode>(AssetDatabase.GUIDToAssetPath(guid));
            if (tile != null && tile.north.Count > 0) // Example of custom filtering
            {
                tiles.Add(tile);
            }
        }
        Debug.Log($"Loaded {tiles.Count} WFCNode assets.");
    }

    private void Start()
    {
        // Load WFCNode assets at start
        LoadTilesFromDirectory();

        grid = new WFCNode[width, height];
        StartCoroutine(CollapseWorld());
    }

    private void WhittleNode(List<WFCNode> potentialNodes, List<WFCNode> validNodes){

        for (int i = potentialNodes.Count -1; i > -1 ; i--) {
            if (!validNodes.Contains(potentialNodes[i])){
                potentialNodes.RemoveAt(i);
            }
        }
    }

    // private void WhittleNode(List<WFCNode> potentialNodes, List<WFCNode> validNodes){
        
    //     potentialNodes = potentialNodes.Intersect(validNodes).ToList();
    // }

    IEnumerator CollapseWorld(){
        toCollapse.Clear();

        toCollapse.Add(new Vector2Int(width/2, height/2));

        while (toCollapse.Count > 0) {
            int x = toCollapse[0].x;
            int y = toCollapse[0].y;

            yield return new WaitForSeconds(delay);
            List<WFCNode> potentialNodes = new List<WFCNode>(tiles);
            
            // List<WFCNode> potentialNodes = new List<WFCNode>();
            // foreach(WFCNode node in tiles){
            //     WFCNode newNode = ScriptableObject.CreateInstance<WFCNode>();
            //     newNode.Copy(node);
            //     potentialNodes.Add(newNode);
            // }

            for (int i = 0; i < offsets.Length; i ++) {
                Vector2Int neighbor = new Vector2Int(x + offsets[i].x, y + offsets[i].y);

                if (isInsideGrid(neighbor)){
                    WFCNode neighborNode = grid[neighbor.x, neighbor.y];

                    if (neighborNode != null){

                        switch (i){

                            case 0:
                                Debug.Log("neighbor: north");
                                Debug.Log(potentialNodes.Count);
                                WhittleNode(potentialNodes, neighborNode.south);
                                Debug.Log(potentialNodes.Count);
                                Debug.Log(neighborNode.name);
                                break;
                            case 1:
                                Debug.Log("neighbor: south");
                                Debug.Log(potentialNodes.Count);
                                WhittleNode(potentialNodes, neighborNode.north);
                                Debug.Log(neighborNode.name);
                                Debug.Log(potentialNodes.Count);
                                break;
                            case 2:
                                Debug.Log("neighbor: east");
                                Debug.Log(potentialNodes.Count);
                                WhittleNode(potentialNodes, neighborNode.west);
                                Debug.Log(neighborNode.name);
                                Debug.Log(potentialNodes.Count);
                                break;
                            case 3:
                                Debug.Log("neighbor: west");
                                Debug.Log(potentialNodes.Count);
                                WhittleNode(potentialNodes, neighborNode.east);
                                Debug.Log(neighborNode.name);
                                Debug.Log(potentialNodes.Count);
                                break;

                        }
                        
                    } else {
                        if (!toCollapse.Contains(neighbor)) {
                            toCollapse.Add(neighbor);
                        }
                    }
                }
            }

            if (potentialNodes.Count <= 0) {
                grid[x,y] = null;
                Debug.Log("No nodes available to collapse");
            } else {
                grid[x,y] = potentialNodes[UnityEngine.Random.Range(0, potentialNodes.Count)];
            }

            //Draw cell
            if (potentialNodes.Count == 0){
                map.SetTile(new Vector3Int(x,y,0), placeholder);
            } else if (grid[x,y].type != TileType.Void){
                map.SetTile(new Vector3Int(x,y,-1), background);
                map.SetTile(new Vector3Int(x,y,0), grid[x,y].tile);
            }

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

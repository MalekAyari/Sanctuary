using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WFCGenerator : MonoBehaviour
{
    
    [SerializeField] private Tile propagatingTile;
    [SerializeField] private Tilemap map;
    [SerializeField] private Vector2Int size = new Vector2Int(16,16);
    [SerializeField] private float delay = 0.2f;
    private Cell[,] grid;

    public List<WFCNode> usedTiles = new List<WFCNode>();
    private List<Cell> cellsToCollapse = new List<Cell>();
    private Vector2Int[] offsets = new Vector2Int[] {
        new Vector2Int(0,1),    //Top
        new Vector2Int(0,-1),   //Bottom
        new Vector2Int(1,0),    //Right
        new Vector2Int(-1,0),   //Left
    };

    public List<WFCNode> LoadTilesFromDirectory()
    {
        List<WFCNode> nodes = new List<WFCNode>();

        string[] guids = AssetDatabase.FindAssets("t:WFCNode");

        foreach (string guid in guids)
        {
            WFCNode tile = AssetDatabase.LoadAssetAtPath<WFCNode>(AssetDatabase.GUIDToAssetPath(guid));
            if (tile != null && tile.north.Count > 0)
            {
                WFCNode node = ScriptableObject.CreateInstance<WFCNode>();
                node.Copy(tile);
                nodes.Add(tile);
            }
        }
        Debug.Log($"Loaded {nodes.Count} WFCNode assets.");
        return nodes;
    }

    private void Start() {
        usedTiles = LoadTilesFromDirectory();
        grid = new Cell[size.x, size.y];

        for (int x = 0; x < size.x; x++){
            for (int y = 0; y < size.y; y++){
                
                Cell cell = new Cell();
                cell.position = new Vector2Int(x, y);

                //Making a deep copy of WFCNodes
                List<WFCNode> nodes = LoadTilesFromDirectory();
                foreach (WFCNode node in nodes){
                    cell.potentialNodes.Add(node);
                }

                Debug.Log($"{x+y} Cells initialized...");
                grid[x,y] = cell;
                cellsToCollapse.Add(cell);

            }
        }

        Debug.Log("Cells have been initialized.");

        StartCoroutine(CollapseWorld());
    }

    IEnumerator CollapseWorld(){

        Debug.Log("Generating...");
        for (int i = 0; i < size.x; i++){
            for (int j = 0; j < size.y; j++){
                yield return new WaitForSeconds(delay);

                Vector2Int cellToCollapsePos = Observe(cellsToCollapse);
                if (grid[cellToCollapsePos.x, cellToCollapsePos.y].potentialNodes.Count != 0){
                    grid[cellToCollapsePos.x, cellToCollapsePos.y].Collapse();

                    cellsToCollapse.Remove(grid[cellToCollapsePos.x, cellToCollapsePos.y]);

                    Debug.Log("Setting tile: ["+ cellToCollapsePos.x + ", " + cellToCollapsePos.y + "] to: "  + grid[cellToCollapsePos.x, cellToCollapsePos.y].tile.name);
                    
                    map.SetTile(new Vector3Int(cellToCollapsePos.x, cellToCollapsePos.y, 0), grid[cellToCollapsePos.x, cellToCollapsePos.y].tile.tile);
                    
                    Propagate(cellToCollapsePos, grid);
                } else {
                    map.SetTile(new Vector3Int(cellToCollapsePos.x, cellToCollapsePos.y, 0), propagatingTile);
                    Time.timeScale = 0f;
                }
            }
        }
    }

    public Vector2Int Observe(List<Cell> cells){
        int minEntropy = usedTiles.Count;
        List<Cell> cellsToCollapse = new List<Cell>();
        
        foreach (Cell cell in cells){
            if (cell.potentialNodes.Count < minEntropy){
                cellsToCollapse.Clear();
                minEntropy = cell.potentialNodes.Count;
            }
            if(cell.potentialNodes.Count == minEntropy){
                cellsToCollapse.Add(cell);
            }
        }

        Vector2Int pos = cellsToCollapse[UnityEngine.Random.Range(0, cellsToCollapse.Count-1)].position;
        Debug.Log($"Chosen cell to collapse: {pos} with {grid[pos.x,pos.y].potentialNodes.Count} nodes available...");

        return pos;
    }

    public void Propagate(Vector2Int position, Cell[,] grid)
    {
        for (int i = 0; i < offsets.Length; i++)
        {
            Vector2Int neighborPos = new Vector2Int(position.x + offsets[i].x, position.y + offsets[i].y);
            if (!isInsideGrid(neighborPos)) continue;

            Cell neighborCell = grid[neighborPos.x, neighborPos.y];
            if (neighborCell.tile == null)
            {
                switch (i)
                {
                    case 0: KeepValidNodes(grid[position.x, position.y].tile.north, neighborCell.potentialNodes); break;
                    case 1: KeepValidNodes(grid[position.x, position.y].tile.south, neighborCell.potentialNodes); break;
                    case 2: KeepValidNodes(grid[position.x, position.y].tile.east, neighborCell.potentialNodes); break;
                    case 3: KeepValidNodes(grid[position.x, position.y].tile.west, neighborCell.potentialNodes); break;
                }

                //neighborCell.history.Add(neighborCell.position, grid[position.x, position.y].potentialNodes);

                Debug.Log($"<color=white> Propagated to cell at {neighborPos}. </color>");
            }
        }
    }



    public void KeepValidNodes(List<WFCNode> allowedTiles, List<WFCNode> neighborPotentialNodes){
        for (int i = neighborPotentialNodes.Count - 1; i >= 0; --i) {
            if (!allowedTiles.Contains(neighborPotentialNodes[i])){
                neighborPotentialNodes.RemoveAt(i);
            }
        }
    }

    private bool isInsideGrid(Vector2Int position){
        return position.x >= 0 && position.x < size.x && position.y >= 0 && position.y < size.y;
    }
}

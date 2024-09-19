using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Cell
{
    public WFCNode tile;
    public Vector2Int position;
    public List<WFCNode> potentialNodes = new List<WFCNode>();

    public Dictionary<Vector2Int, List<WFCNode>> history = new Dictionary<Vector2Int, List<WFCNode>>();

    public void Collapse(){
        if (potentialNodes.Count == 0){
            Debug.Log("<color=red>No options left! Review rules or propagation!</color>");
            
            
            
            // Debug.Log("<color=grey> History:");
            
            // foreach (Vector2Int propagationSource in history.Keys){
            //     Debug.Log($"Cell {propagationSource}, has rendered the list to:");
            //     foreach (WFCNode node in history[propagationSource]){
            //         string subStr = "";
            //         foreach (WFCNode n in potentialNodes){
            //             subStr += n.name + ", ";
            //         }
            //         subStr += ".";
            //         Debug.Log(subStr);
            //     }
            // }
        }

        if (tile == null && potentialNodes.Count > 0){
            tile = potentialNodes[UnityEngine.Random.Range(0, potentialNodes.Count)];
            potentialNodes.Clear();
        }

    }


}

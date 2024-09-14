using System.Collections.Generic;

public class RuleGraph
{
    public bool edge = true;

    public Dictionary<string, Dictionary<Direction, List<string>>> adjacencies;

    public RuleGraph(){
        adjacencies = new Dictionary<string, Dictionary<Direction, List<string>>>();
    }

    public void AddRule(string tile, Direction direction, string neighborTile){
        if (!adjacencies.ContainsKey(tile)){
            adjacencies[tile] = new Dictionary<Direction, List<string>>();
        }
        adjacencies[tile][direction].Add(neighborTile);
    }

    public List<string> GetValidNeighbors(string tile, Direction direction){
        if (adjacencies.ContainsKey(tile) && adjacencies[tile].ContainsKey(direction)){
            return adjacencies[tile][direction];
        }
        return new List<string>();
    }



    
}

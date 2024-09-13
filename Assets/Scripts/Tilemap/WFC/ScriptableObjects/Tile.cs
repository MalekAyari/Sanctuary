using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "Tile", order = 0)]
public class Tile : ScriptableObject {
    
    public Sprite sprite;

    public List<Sprite> north = new List<Sprite>();
    public List<Sprite> northEast = new List<Sprite>();
    public List<Sprite> east = new List<Sprite>();
    public List<Sprite> southEast = new List<Sprite>();
    public List<Sprite> south = new List<Sprite>();
    public List<Sprite> southWest = new List<Sprite>();
    public List<Sprite> west = new List<Sprite>();
    public List<Sprite> northWest = new List<Sprite>();


}


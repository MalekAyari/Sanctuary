using UnityEngine;
public class WalkerObject
{
    
    public Vector2 pos;
    public Vector2 direction;
    public float chanceToChange;

    public WalkerObject(Vector2 pos, Vector2 direction, float chance){
        this.pos = pos;
        this.direction = direction;
        this.chanceToChange = chance;
    }

}

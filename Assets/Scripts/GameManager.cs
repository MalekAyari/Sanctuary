using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    enum GameMode
    {
        Normal,
    }

    public GameManager instance;    
    public GameObject map;
    
    public GameManager(){
        
        if (instance != null){
            Destroy(this);
        } else {
            instance = this;
        }
    }


}

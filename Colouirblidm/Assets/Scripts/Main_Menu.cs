using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu : MonoBehaviour
{
    Game_Manager manager;
    void Start()
    { 
        manager = GameObject.FindObjectOfType<Game_Manager>();
    }
    public void playgame()
    {
        manager.NextLevel();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReferences : MonoBehaviour
{

    public static PlayerMovement playerMovement;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (playerMovement == null)
        {
            print("playerMovement reference not set.");
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}

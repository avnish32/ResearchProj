using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool canPlayerMove = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisablePlayerMovement()
    {
        canPlayerMove = false;
    }

    public void EnablePlayerMovement()
    {
        canPlayerMove = true;
    }

    public bool CanPlayerMove()
    {
        return canPlayerMove;
    }
}

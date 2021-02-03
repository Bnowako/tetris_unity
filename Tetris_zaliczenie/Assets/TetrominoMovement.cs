using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoMovement : MonoBehaviour
{
    bool validMovement = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) && validateMovement('L'))
        {
            transform.position += new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && validateMovement('R'))
        {
            transform.position += new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && validateMovement('D'))
        {
            transform.position += new Vector3(0, -1, 0);
        }
        



       
    }
    bool validateMovement(char direction )
    {
        foreach (Transform child in transform)
        {
            Vector2 vector = GameLogic.RoundVector(child.position);
            validMovement = GameLogic.IsGridPosValid(vector,direction);
            if (!validMovement)
            {
                return false;
            }
        }

        return true;

    }
}

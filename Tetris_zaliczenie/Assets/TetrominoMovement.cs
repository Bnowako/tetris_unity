using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoMovement : MonoBehaviour
{
    //GameObject[] tetrominos;


    bool valid_movement = false;
    float last_piece_fall = 0;

    float falling_speed = 1;
    public static bool next_piece_needed = false;
    // Start is called before the first frame update
    void Start()
    {
        //InitializeNextTetromino();

    }

    // Update is called once per frame
    void Update()
    {
        if (next_piece_needed)
        {
            StopTetromino();

        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) && ValidateMovement('L'))
        {
            transform.position += new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && ValidateMovement('R'))
        {
            transform.position += new Vector3(1, 0, 0);
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || Time.time - last_piece_fall >= falling_speed) && ValidateMovement('D'))
        {
            transform.position += new Vector3(0, -1, 0);
            last_piece_fall = Time.time;
        }
        else if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            RotatePieceIfPossible();
        }
        
        


       
    }
    bool ValidateMovement(char direction )
    {

        foreach (Transform child in transform)
        {

            if(child.position.y == 0)
            {
                next_piece_needed = true;
            }

            Vector2 vector = GameLogic.RoundVector(child.position);
            valid_movement = GameLogic.IsGridPosValid(vector,direction);
            if (!valid_movement)
            {
                return false;
            }
        }

        return true;

    }

    void RotatePieceIfPossible()
    {
        transform.Rotate(0, 0, -90);

        foreach (Transform child in transform)
        {
            Vector2 vector = GameLogic.RoundVector(child.position);
            valid_movement = GameLogic.IsGridPosValid(vector, 'O');
            if (!valid_movement)
            {
                transform.Rotate(0, 0, 90);
                return;
            }
        }
        return;

    }

    void StopTetromino()
    {
        AddTetrominoToGrid();
        GameLogic.next_piece_needed = true;
        enabled = false;
    }

    void AddTetrominoToGrid()
    {
        foreach(Transform child in transform)
        {
     

            Vector2 vector = GameLogic.RoundVector(child.position);
            int x_pos = (int)vector.x;
            int y_pos = (int)vector.y;

   
            GameLogic.grid[x_pos,y_pos] = child;
        }
    }


 

}

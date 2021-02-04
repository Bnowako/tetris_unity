using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoMovement : MonoBehaviour
{
    
    bool valid_movement = false;

    // Falling time management
    float last_piece_fall = 0;

    // 1s For each fall
    float falling_speed = 1;

    public static bool next_piece_needed = false;
  

    // Update is called once per frame
    void Update()
    {

        if (next_piece_needed)
        {
            StopTetromino();

        }

        // Handling input for game
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
            Vector2 vector = GameLogic.RoundVector(child.position);

            if(vector.y == 0)
            {
                next_piece_needed = true;
            }

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
        Debug.Log(gameObject.name);
        transform.Rotate(0, 0, -90);

        foreach (Transform child in transform)
        {
            Vector2 vector = GameLogic.RoundVector(child.position);
            valid_movement = GameLogic.IsGridPosValid(vector, 'O');

            // Little trick :D with gameObject.name
            if (!valid_movement || gameObject.name == "O_Tetromino(Clone)") 
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

            if (y_pos < GameLogic.game_board_height)
            {
                GameLogic.grid[x_pos, y_pos] = child;
            }

        }
    }


 

}

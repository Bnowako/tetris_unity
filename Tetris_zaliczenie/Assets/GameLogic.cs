using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLogic : MonoBehaviour
{
    public GameObject[] tetrominos;

    // Width and haight is in units. 1 unit = 56px
    public static int game_board_width = 10;
    public static int game_board_height = 20;

    // Grid that will corespond to our game board status
    public static new Transform[,] grid;

    
    public static bool next_piece_needed = false;

    void Start()
    {
        grid = new Transform[game_board_width, game_board_height];
        InitializeNextTetromino();
    }

    void Update()
    {
        if (CheckIfLost())
        {
            ResetGame();
        }

        HandleScoredRows();

        if (next_piece_needed)
        {
           InitializeNextTetromino();
            next_piece_needed = false;
            TetrominoMovement.next_piece_needed = false;
        }

    }
 
    // Initialize new tetromino piece at predetermined position
    void InitializeNextTetromino()
    {
        int i = (int)(Random.Range(0, tetrominos.Length));

        int spawn_x = (int)(GameLogic.game_board_width / 2);
        int spawn_y = GameLogic.game_board_height;

        Instantiate(tetrominos[i], new Vector3(spawn_x, spawn_y, 0), Quaternion.identity);
    }


    // Check if there are any scored rows
    // If there are delete them and move rows from above 1 row down
    void HandleScoredRows()
    {
        for(int y = 0; y < game_board_height; ++y)
        {
            if (IsRowScored(y))
            {
                RemoveRow(y);
                MoveRowsDown(y);
                --y;
            }
        }

    }

    // Check if row y is scored.
    // return true if row is scored false if not
    bool IsRowScored(int y)
    {
        // Check each object in a grid array
        // If it is null return false - row is not scored
        // If any of object are false return true - row is scored
        for(int x =0; x < game_board_width; ++x)
        {
            if (!grid[x, y])
            {
                return false;
            }
        }
        Debug.Log(y);
        return true;

    }

    // Iterate through row and destroy gameobject
    // Set each element of array in row y to null
    void RemoveRow(int y)
    {
        for (int x = 0; x < game_board_width; ++x)
        {
            if (grid[x, y])
            {
                Destroy(grid[x, y].gameObject);

            }
            grid[x, y] = null;
        }
    }

    // Iterate through grid and move rows above scored row - down
    void MoveRowsDown(int scored_row_index)
    {
        int y = scored_row_index + 1;
        for(int i = y; i < game_board_height; ++i)
        {
            for(int x= 0; x < game_board_width; ++x)
            {
                if (grid[x, i])
                {
                    Debug.Log(x);
                    Debug.Log(i);

                    grid[x, i - 1] = grid[x, i];
                    grid[x, i] = null;

                    grid[x, i - 1].position += new Vector3(0, -1,0);
                }
            }
        }
    }

    // Reset game by removing all the rows and reseting the flags
    void ResetGame()
    {
        for(int y =0; y< game_board_height;++y)
        {
            RemoveRow(y);
            
        }
        TetrominoMovement.next_piece_needed = true;
        next_piece_needed = true;
    }

    // Check if there are any pieces on the highest row
    // If there are return true
    bool CheckIfLost()
    {
        for (int x = 0; x < game_board_width; ++x)
        {
            //Debug.Log(x);
            if (grid[x,game_board_height-1])
            {
                return true;
            }
        }
        return false;
    }


    // Check if desirable position after any kind of move is valid

    public static bool IsGridPosValid(Vector2 position, char direction)
    {
        int temp_pos_x = (int)position.x;
        int temp_pos_y = (int)position.y;
     

        if (direction == 'L') temp_pos_x--;
        if (direction == 'R') temp_pos_x++;
        if (direction == 'D') temp_pos_y--; 


        bool valid_movement = temp_pos_x >= 0 && temp_pos_x < game_board_width && temp_pos_y >= 0;


        if (!valid_movement)
        {
            return false;
        }

        bool free_space = IsSpaceFree(temp_pos_x, temp_pos_y);


        if (direction == 'D' && !free_space)
        {
            TetrominoMovement.next_piece_needed = true;
            return false;

        }

        valid_movement = valid_movement && free_space;
        return valid_movement;
    }


    // Helper for validating move, checks if there is space for piece
    public static bool IsSpaceFree(int x,int y)
    {
        if (y < 20)
        {
            if (grid[x, y])
            {
                return false;

            }

            return true;
        }
        return true;
        

    }


    // Sometimes vectors slipped from full values
    // Helper function to round them
    public static Vector2 RoundVector(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }




}

    



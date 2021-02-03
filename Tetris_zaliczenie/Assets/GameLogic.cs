using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLogic : MonoBehaviour
{
    public GameObject[] tetrominos;

    //Width and haight is in units. 1 unit = 56px
    public static int game_board_width = 10;
    public static int game_board_height = 20;

    // Declare grid that will corespond to our game board status.
    public static new Transform[,] grid;

    public static bool next_piece_needed = false;

    void Start()
    {
        grid = new Transform[game_board_width, game_board_height];
        InitializeNextTetromino();
    }

    void Update()
    {
        if (next_piece_needed)
        {
           InitializeNextTetromino();
            next_piece_needed = false;
            TetrominoMovement.next_piece_needed = false;
        }

    }


 

    void InitializeNextTetromino()
    {
        int i = (int)(Random.Range(0, tetrominos.Length));

        // Instantiating prefab tetromino
        // https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
        // Quaternion.identity - no rotation

        int spawn_x = (int)(GameLogic.game_board_width / 2);
        int spawn_y = GameLogic.game_board_height;

        Instantiate(tetrominos[i], new Vector3(spawn_x, spawn_y, 0), Quaternion.identity);
    }




    public static bool IsGridPosValid(Vector2 position, char direction)
    {
        

        int temp_pos_x = (int)position.x;
        int temp_pos_y = (int)position.y;
        //Debug.Log(position.x);
        //Debug.Log(position.y);

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

    public static Vector2 RoundVector(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }


}

    



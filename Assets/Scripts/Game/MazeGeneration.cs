using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

// Ability to generate the maze and keep it during edit mode,
// Provides the ability to make tweaks to the maze in case the generation went wrong. etc.
[ExecuteAlways]
public class RecursiveBacktrackingMaze : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public int thickness = 1;
    public int seed = -1; // Optional to randomise the maze or set a specific seed
    
    public Tilemap mazeTilemap;
    public Tilemap objectsTilemap;
    public Tilemap openedTilemap;
        
    public Tile pathTile;
    public TileBase wallRuleTile;
    public Tile chestTile;
    public Tile openedChestTile;
    public Tile doorTile;
    public Tile openedDoorTile;

    private int[,] grid;    // Internal representation of the maze
    private const int N = 1, S = 2, E = 4, W = 8; // Directions

    private readonly Dictionary<int, Vector2Int> directionVectors = new Dictionary<int, Vector2Int>
    {
        { E, new Vector2Int(1, 0) },
        { W, new Vector2Int(-1, 0) },
        { N, new Vector2Int(0, 1) },
        { S, new Vector2Int(0, -1) }
    };

    // private readonly Dictionary<int, int> opposites = new Dictionary<int, int>
    // {
    //     { E, W },
    //     { W, E },
    //     { N, S },
    //     { S, N }
    // };

    private int opposites(int direction)
    {
        switch (direction)
        {
            case E:
                return W;
            case W: 
                return E;
            case N:
                return S;
            case S:
                return N;
            default:
                return N;
        }
    }
    
    /*
     * 1. Clears the exisiting maze
     * 2. Sets the seed
     * 3. Initializes the internal grid representation of the maze
     * 4. Uses the recursive backtracking algorithm to carve the paths
     * 5. Visualizes the maze on the tilemap by calling DrawMaze
     */
    public void GenerateMaze()
    {
        ClearMaze();
        
        if (seed >= 1)
        {
            UnityEngine.Random.InitState(seed);
        }
        else
        {
            seed = UnityEngine.Random.Range(0, int.MaxValue);
        }

        grid = new int[height, width];
        CarvePassagesFrom(0, 0);

        DrawMaze();
    }

    public void ClearMaze()
    {
        if (mazeTilemap != null)
        {
            mazeTilemap.ClearAllTiles();
            objectsTilemap.ClearAllTiles();
            openedTilemap.ClearAllTiles();
            seed = -1;
        }
    }

    /*
     * Recursive method that creates the maze by carving passages between cells
     * Randomly shuffles directions and connects cells that haven't been visited yet
     */
    void CarvePassagesFrom(int cx, int cy)
    {
        var directions = new List<int> { N, S, E, W };
        Shuffle(directions);

        foreach (var direction in directions)
        {
            int nx = cx + directionVectors[direction].x;
            int ny = cy + directionVectors[direction].y;

            if (ny >= 0 && ny < height && nx >= 0 && nx < width && grid[ny, nx] == 0)
            {
                grid[cy, cx] |= direction;
                grid[ny, nx] |= opposites(direction);
                CarvePassagesFrom(nx, ny);
            }
        }
    }

    
    /*
     * Draws the entire maze on the Tilemap:
     * 1. Makes sure that the path tiles do not have a 2d collider
     * 2. Fills the entire area with walls
     * 3. Sets specific tiles as paths based on the internal grid representation
     * 4. Ensures connections between cells are visualized correctly
     */
    void DrawMaze()
    {
        pathTile.colliderType = Tile.ColliderType.None;

        // Fill the entire area with walls
        for (int y = 0; y < height * 2 + 1; y++)
        {
            for (int x = 0; x < width * 2 + 1; x++)
            {
                var position = new Vector3Int(x, y, 0);
                mazeTilemap.SetTile(position, wallRuleTile);
            }
        }

        // List to store all available path tiles (empty spaces in the maze)
        List<Vector3Int> pathTiles = new List<Vector3Int>();

        // Draw the maze based on the grid representation
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int cell = grid[y, x];
                var basePosition = new Vector3Int(x * 2 + 1, y * 2 + 1, 0);

                // Draw the cell itself as a path
                mazeTilemap.SetTile(basePosition, pathTile);

                // Add the path tile to the list
                pathTiles.Add(basePosition);

                // Draw connections based on the carved paths in the grid
                if ((cell & E) != 0) // East connection
                {
                    mazeTilemap.SetTile(basePosition + Vector3Int.right, pathTile);
                }
                if ((cell & S) != 0) // South connection
                {
                    mazeTilemap.SetTile(basePosition + Vector3Int.down, pathTile);
                }
            }
        }

        // Add the exit & entrance as a path tile
        var entrancePosition = new Vector3Int(1, height * 2, 0);
        mazeTilemap.SetTile(entrancePosition, pathTile);
        
        var exitPosition = new Vector3Int(width * 2 - 1, 0, 0);
        objectsTilemap.SetTile(exitPosition, pathTile);

        // Place the exit tile at the specified position
        if (doorTile != null)
        {
            // Ensure the exit tile position is valid
            if (objectsTilemap.HasTile(exitPosition))
            {
                // Set the tile at the exit position to the door tile
                objectsTilemap.SetTile(exitPosition, doorTile);
                openedTilemap.SetTile(exitPosition, openedDoorTile);
            }
        }

        // Randomly place 3 chest tiles on path tiles
        if (chestTile != null && pathTiles.Count > 3)
        {
            for (int i = 0; i < 3; i++)
            {
                // Randomly pick a path tile from the list
                int randomIndex = UnityEngine.Random.Range(0, pathTiles.Count);
                Vector3Int randomTilePosition = pathTiles[randomIndex];

                // Set the tile at the chosen position to the chest tile on the ChestTilemap
                objectsTilemap.SetTile(randomTilePosition, chestTile);
                openedTilemap.SetTile(randomTilePosition, openedChestTile);

                // Optionally remove the placed key from the list to avoid reusing the same tile
                pathTiles.RemoveAt(randomIndex);
            }
        }

        // Surround the maze with walls
        for (int y = 0; y < height * 2 + 1; y++)
        {
            for (int x = 0; x < width * 2 + 1; x++)
            {
                // Skip entrance and exit positions
                if ((y == height * 2 && x == 1) || (y == 0 && x == width * 2 - 1))
                {
                    continue;
                }

                // Ensure no gaps in the boundary
                if (x == 0 || x == width * 2 || y == 0 || y == height * 2)
                {
                    var position = new Vector3Int(x, y, 0);
                    mazeTilemap.SetTile(position, wallRuleTile);
                }
            }
        }
    }
    
    /*
     * Randomly shuffle the elements of a list to randomize the direction order in CarvePassagesFrom
     */
    void Shuffle<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}

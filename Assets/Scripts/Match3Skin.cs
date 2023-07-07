using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using static Unity.Mathematics.math;

/// <summary>
/// To support easy switching of visualization while keeping the game logic the same,
/// we introduce a Match3Skin component type that acts as a proxy for the actual game logic.
/// The main game object will only interact with this skin.
/// 
/// Note that with this approach it would be fairly simple to support multiple skins, 
/// game modes, or multiple games in a single app, by changing the active game.
/// </summary>
public class Match3Skin : MonoBehaviour
{
    [SerializeField]
    Match3Game game;

    Grid2D<Tile> tiles;

    float2 tileOffset;

    [SerializeField]
    Tile[] tilePrefabs;

    public bool IsPlaying => true;

    public bool IsBusy => false;

    public void StartNewGame()
    {
        game.StartNewGame();
        tileOffset = -0.5f * (float2)(game.Size - 1);
        if (tiles.IsUndefined)
        {
            tiles = new(game.Size);
        }
        else
        {
            for (int y = 0; y < tiles.SizeY; y++)
            {
                for (int x = 0; x < tiles.SizeX; x++)
                {
                    tiles[x, y].Despawn();
                    tiles[x, y] = null;
                }
            }
        }


        for (int y = 0; y < tiles.SizeY; y++)
        {
            for (int x = 0; x < tiles.SizeX; x++)
            {
                tiles[x, y] = SpawnTile(game[x, y], x, y);
            }
        }
    }

    Tile SpawnTile(TileState t, float x, float y) =>
    tilePrefabs[(int)t - 1].Spawn(new Vector3(x + tileOffset.x, y + tileOffset.y));

    public void DoWork()
    {

    }

    public bool EvaluateDrag(Vector3 start, Vector3 end)
    {
        return false;
    }
}
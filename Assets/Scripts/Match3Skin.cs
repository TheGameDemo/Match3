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
    public bool IsPlaying => true;

    public bool IsBusy => false;

    public void StartNewGame()
    {

    }

    public void DoWork()
    {

    }

    public bool EvaluateDrag(Vector3 start, Vector3 end)
    {
        return false;
    }
}
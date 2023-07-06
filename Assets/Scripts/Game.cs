using UnityEngine;

/// <summary>
/// The main controller of the game
/// </summary>
public class Game : MonoBehaviour
{
    [SerializeField]
    Match3Skin match3;

    void Awake() => match3.StartNewGame();

    void Update()
    {
        if (match3.IsPlaying)
        {
            if (!match3.IsBusy)
            {
                HandleInput();
            }
            match3.DoWork();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            match3.StartNewGame();
        }
    }

    void HandleInput() { }
}

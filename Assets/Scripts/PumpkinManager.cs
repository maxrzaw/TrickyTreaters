using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinManager : MonoBehaviour
{
    public PumpkinTrace easy;
    public PumpkinTrace normal;
    public PumpkinTrace hard;

    private Subscription<PlayGameEvent> play_game_sub;

    // Start is called before the first frame update
    void Start()
    {
        play_game_sub = EventBus.Subscribe<PlayGameEvent>(onPlayGameEvent);

        // Hide all games
        easy.gameObject.SetActive(false);
        normal.gameObject.SetActive(false);
        hard.gameObject.SetActive(false);


    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(play_game_sub);
    }

    public void onPlayGameEvent(PlayGameEvent e)
    {
        if (e.game != MiniGames.PumpkinCarving)
        {
            // early out
            return;
        }
        switch (e.difficulty)
        {
            case 1:
                easy.gameObject.SetActive(true);
                break;
            case 2:
                normal.gameObject.SetActive(true);
                break;
            case 3:
                hard.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[DisallowMultipleComponent]
public class CameraController : MonoBehaviour
{
    public Vector3 start = new Vector3(0, 0, -12);
    public Vector3 pumpkin = new Vector3(-100, 0, -10);
    public Vector3 maze1 = new Vector3(36.79f, -17.1f, -10);
    public Vector3 maze2 = new Vector3(68.4f, -17.3f, -10);
    public Vector3 maze3 = new Vector3(99.7f, -17.3f, -10);
    public Vector3 apples = new Vector3(0, 0, -10);

    Subscription<PlayGameEvent> play_game_sub;
    Subscription<BackToStreetEvent> back_to_street_sub;
    // Start is called before the first frame update
    void Start()
    {
        play_game_sub = EventBus.Subscribe<PlayGameEvent>(onPlayGame);
        back_to_street_sub = EventBus.Subscribe<BackToStreetEvent>(onBackToStreet);
        transform.position = start;
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(play_game_sub);
        EventBus.Unsubscribe(back_to_street_sub);
    }

    public void onPlayGame(PlayGameEvent e)
    {
        switch (e.game)
        {
            case MiniGames.PumpkinCarving:
                transform.position = pumpkin;
                break;
            case MiniGames.CornMaze:
                switch(e.difficulty) {
                    case 1: 
                        transform.position = maze1;
                        break;
                    case 2:
                        transform.position = maze2;
                        break;
                    case 3: 
                        transform.position = maze3;
                        break;
                }
                break;
            case MiniGames.AppleBobbing:
                transform.position = apples;
                break;
            default:
                transform.position = start;
                break;
        }
    }

    public void onBackToStreet(BackToStreetEvent e)
    {
        transform.position = start;
    }
}

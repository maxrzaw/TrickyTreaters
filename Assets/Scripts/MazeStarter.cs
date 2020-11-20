using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeStarter : MonoBehaviour
{
    public int level = 1;
    Subscription<PlayGameEvent> play_game_sub;
    //private Vector3 startingPositionPlayer;
    //private Vector3 startingPositionCamera;
    // Start is called before the first frame update
    void Start()
    {
        play_game_sub = EventBus.Subscribe<PlayGameEvent>(onPlayGame);
    }

    private void OnDestroy() {
        EventBus.Unsubscribe(play_game_sub);
    }

    public void onPlayGame(PlayGameEvent e) {
        if (e.game == MiniGames.CornMaze && e.difficulty == level) {
            // move player and camera
            EventBus.Publish<ShowHintText>(new ShowHintText("Escape the Corn Maze!"));
            EventBus.Publish<MovePlayerEvent>(new MovePlayerEvent(transform.position));
            // unfreeze player
            EventBus.Publish<PlayerMovementEvent>(new PlayerMovementEvent(true));
        }
    }
}

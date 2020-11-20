using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeEnder : MonoBehaviour
{
    public MazeStarter mazeStarter;
    private bool ended = false;

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player") && ended == false) {
            ended = true;
            EventBus.Publish<PlayerMovementEvent>(new PlayerMovementEvent(false));
            StartCoroutine(WaitAfterEnd());
        }
    }

    private IEnumerator WaitAfterEnd()
    {
        EventBus.Publish<ScoreEvent>(new ScoreEvent(mazeStarter.level * 2 + 1));
        yield return new WaitForSeconds(0.5f);
        EventBus.Publish<BackToStreetEvent>(new BackToStreetEvent());
        EventBus.Publish<PlayerMovementEvent>(new PlayerMovementEvent(true));
        yield return new WaitForSeconds(1.0f);
        ended = false;
    }
}

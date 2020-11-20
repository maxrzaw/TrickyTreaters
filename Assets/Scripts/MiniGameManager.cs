using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{

    Subscription<PlayGameEvent> visit_house_sub;

    // Start is called before the first frame update
    void Start()
    {
        visit_house_sub = EventBus.Subscribe<PlayGameEvent>(onPlayGame);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(visit_house_sub);
    }

    public void onPlayGame(PlayGameEvent e)
    {
        Debug.Log("Playing Game!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

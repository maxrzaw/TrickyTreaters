using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinTrace : MonoBehaviour
{
    public Tracable[] shapes;
    public int candy = 1;
    private bool ended = false;

    private void OnEnable()
    {
        ResetShapes();
        EventBus.Publish<ShowHintText>(new ShowHintText("Click and Trace The Pumpkin!"));
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkShapes() == true && ended == false)
        {
            ended = true;
            StartCoroutine(WaitAfterEnd());
        }
    }

    public void ResetShapes()
    {
        ended = false;
        foreach (Tracable shape in shapes)
        {
            shape.ResetTrace();
        }
    }

    private bool checkShapes()
    {
        foreach (Tracable shape in shapes)
        {
            if (shape.traced == false)
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator WaitAfterEnd()
    {
        EventBus.Publish<ScoreEvent>(new ScoreEvent(candy));
        yield return new WaitForSeconds(0.5f);
        EventBus.Publish<BackToStreetEvent>(new BackToStreetEvent());
        EventBus.Publish<PlayerMovementEvent>(new PlayerMovementEvent(true));
        // Deactivate self
        gameObject.SetActive(false);

    }
}

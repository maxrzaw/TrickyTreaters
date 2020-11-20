using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Apple : MonoBehaviour, IPointerEnterHandler
{
    public Apple(float lifeSpan)
    {
        this.lifeSpan = lifeSpan;
    }

    public int score;

    public float lifeSpan;

    private float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > lifeSpan)
        {
            Destroy(this.gameObject);
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        EventBus.Publish<AppleScoreEvent>(new AppleScoreEvent(score));
        Destroy(this.gameObject);
    }

}

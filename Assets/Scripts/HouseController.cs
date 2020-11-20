using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[DisallowMultipleComponent]
public class HouseController : MonoBehaviour
{
    public BoxCollider2D door;
    public int houseNumber = 0;
    public int difficulty = 1;
    public bool visited = false;
    public MiniGames game;
    public SpriteRenderer lampPost;
    public Sprite lampOn;
    public Sprite lampOff;
    [Range(0, 10)]
    public int candy_min = 1;
    [Range(0, 10)]
    public int candy_max = 5;
    private int candy;

    private void Start()
    {
        visited = false;
        lampPost.sprite = lampOn;
        candy = Random.Range(candy_min, candy_max);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && visited == false)
        {
            visited = true;
            lampPost.sprite = lampOff;
            EventBus.Publish<ShowOptionsEvent>(new ShowOptionsEvent(game, difficulty, candy));
            EventBus.Publish<PlayerMovementEvent>(new PlayerMovementEvent(false));
            EventBus.Publish<VisitHouseEvent>(new VisitHouseEvent(houseNumber));
        }
    }
}

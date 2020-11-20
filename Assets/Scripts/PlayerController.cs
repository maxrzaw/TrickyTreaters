using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 direction = Vector2.zero;
    public bool moving = false;
    private Rigidbody2D rb;

    private Subscription<PlayerMovementEvent> player_movement_sub;
    private Subscription<StartEvent> start_event_sub;
    private Subscription<MovePlayerEvent> move_player_sub;
    private Subscription<BackToStreetEvent> back_to_street_sub;
    private Subscription<EndGameEvent> game_is_alive;
    private Subscription<ShowOptionsEvent> show_options_sub;

    private Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPosition = transform.position;
        player_movement_sub = EventBus.Subscribe<PlayerMovementEvent>(onPlayerMoveEvent);
        start_event_sub = EventBus.Subscribe<StartEvent>(onStartEvent);
        move_player_sub = EventBus.Subscribe<MovePlayerEvent>(onMovePlayerEvent);
        back_to_street_sub = EventBus.Subscribe<BackToStreetEvent>(onBackToStreetEvent);
        game_is_alive = EventBus.Subscribe<EndGameEvent>(onGameEndEvent);
        show_options_sub = EventBus.Subscribe<ShowOptionsEvent>(onShowOptionsEvent);

        moving = false;
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(player_movement_sub);
        EventBus.Unsubscribe(start_event_sub);
        EventBus.Unsubscribe(move_player_sub);
        EventBus.Unsubscribe(back_to_street_sub);
        EventBus.Unsubscribe(game_is_alive);
        EventBus.Unsubscribe(show_options_sub);
    }

    // Update is called once per frame
    void Update()
    {
        if (moving == true)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void onGameEndEvent(EndGameEvent e)
    {
        moving = false;
    }
    public void Movement(InputAction.CallbackContext context)
    {      
        direction = context.ReadValue<Vector2>();        
    }

    public void onPlayerMoveEvent(PlayerMovementEvent e)
    {
       moving = e.moving;
    }

    public void onMovePlayerEvent(MovePlayerEvent e) {
        transform.position = e.location;
    }

    public void onShowOptionsEvent(ShowOptionsEvent e)
    {
        lastPosition = transform.position;
    }

    public void onStartEvent(StartEvent e)
    {
        moving = true;
    }

    public void onBackToStreetEvent(BackToStreetEvent e) {
        transform.position = lastPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;

    public float timeLeft = 10;

    public TextMeshProUGUI scoreText;

    public GameObject timeBarUI;

    private Slider timeBarSlider;
    private bool paused = true;

    
    private Subscription<ScoreEvent> score_event_sub;
    private Subscription<StartEvent> start_event_sub;
    private Subscription<VisitHouseEvent> visit_house_sub;
    private Subscription<BackToStreetEvent> back_to_street_sub;

    private bool[] visits = new bool[12];
    private bool game_over = false;



    // Start is called before the first frame update
    void Start()
    {
        timeBarSlider = timeBarUI.GetComponent<Slider>();
        timeBarSlider.maxValue = timeLeft;
        timeBarSlider.value = timeLeft;

        score_event_sub = EventBus.Subscribe<ScoreEvent>(onScore);
        start_event_sub = EventBus.Subscribe<StartEvent>(onStartEvent);
        visit_house_sub = EventBus.Subscribe<VisitHouseEvent>(onVisitHouse);
        back_to_street_sub = EventBus.Subscribe<BackToStreetEvent>(onBackToStreet);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<ScoreEvent>(score_event_sub);
        EventBus.Unsubscribe<StartEvent>(start_event_sub);
        EventBus.Unsubscribe<VisitHouseEvent>(visit_house_sub);
        EventBus.Unsubscribe<BackToStreetEvent>(back_to_street_sub);
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused && !game_over)
        {
            timeBarSlider.value -= Time.deltaTime;
        }
        

        if (timeBarSlider.value <= 0.0f)
        {
            Debug.Log("Ending game!");
            // Game over logic here            
            endGame();
        }

        if (Keyboard.current.spaceKey.isPressed && game_over)
        {
            game_over = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void onScore(ScoreEvent e)
    {
        score += e.addToScore;
        scoreText.text = "Score: " + score.ToString();
    }

    void onStartEvent(StartEvent e)
    {
        timeBarSlider.value = timeLeft;
        paused = false;
    }

    void onVisitHouse(VisitHouseEvent e)
    {
        visits[e.house] = true;
    }

    void onBackToStreet(BackToStreetEvent e)
    {
        if (checkHouses())
        {
            // End game
            if (!game_over)
            {
                endGame();
            }
        }
    }

    private bool checkHouses()
    {
        // loop through houses
        foreach (bool house in visits)
        {
            // if a house has not been visited, return false
            if (!house)
            {
                return false;
            }
        }

        // if you make it through without returning,
        // then all houses have been visited
        return true;
    }

    private void endGame()
    {
        // For now just reload the scene
        if(!game_over)
        {
            game_over = true;
            EventBus.Publish<EndGameEvent>(new EndGameEvent());
            EventBus.Publish<BackToStreetEvent>(new BackToStreetEvent());
            EventBus.Publish<PlayerMovementEvent>(new PlayerMovementEvent(false));
        }
    }
}

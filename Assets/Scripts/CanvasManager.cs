using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject optionsPanel;
    public GameObject scorePanel;
    public GameObject creditsPanel;
    public GameObject restartPanel;

    public TextMeshProUGUI trickLevelText;
    public TextMeshProUGUI candyCountText;
    public TextMeshProUGUI hintText;
    public Image miniGameImage;
    public Image candyImage;
    public Sprite mazeImage;
    public Sprite pumpkinImage;
    public Sprite appleImage;
    public Sprite candyOne;
    public Sprite candyTwo;
    public Sprite candyThree;
    private int candyValue = 0;
    private MiniGames game;
    private int difficulty;

    private Subscription<ShowOptionsEvent> show_options_sub;
    private Subscription<EndGameEvent> game_is_alive;
    private Subscription<ShowHintText> hint_listener;
    private Subscription<BackToStreetEvent> back_to_street_sub;
    // Start is called before the first frame update
    void Start()
    {
        startPanel.SetActive(true);
        scorePanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        restartPanel.SetActive(false);
        show_options_sub = EventBus.Subscribe<ShowOptionsEvent>(onShowOptions);
        game_is_alive = EventBus.Subscribe<EndGameEvent>(onGameEnd);
        hint_listener = EventBus.Subscribe<ShowHintText>(onHint);
        back_to_street_sub = EventBus.Subscribe<BackToStreetEvent>(onBackToStreet);
        hintText.text = "";

    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(show_options_sub);
        EventBus.Unsubscribe(game_is_alive);
        EventBus.Unsubscribe(hint_listener);
        EventBus.Unsubscribe(back_to_street_sub);
    }

    public void onGameEnd(EndGameEvent e)
    {
        restartPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void onBackToStreet(BackToStreetEvent e)
    {
        hintText.text = "";
    }
    
    public void onCandyPress()
    {
        EventBus.Publish<ScoreEvent>(new ScoreEvent(candyValue));
        EventBus.Publish<BackToStreetEvent>(new BackToStreetEvent());
        EventBus.Publish<PlayerMovementEvent>(new PlayerMovementEvent(true));
        optionsPanel.SetActive(false);
    }

    public void onTrickPress()
    {
        EventBus.Publish<PlayGameEvent>(new PlayGameEvent(game, difficulty));
        optionsPanel.SetActive(false);
    }

    public void onStartPress()
    {
        // Send an event to start the game
        EventBus.Publish<StartEvent>(new StartEvent());
        startPanel.SetActive(false);
    }

    public void onCreditsPress()
    {
        startPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void onCreditsClose()
    {
        creditsPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void onHint(ShowHintText e)
    {
        hintText.text = e.text;
    }


    public void onShowOptions(ShowOptionsEvent e)
    {
        optionsPanel.SetActive(true);
        candyValue = e.candy;
        game = e.game;
        difficulty = e.difficulty;

        // Determine which sprites to show and what text
        // Game Sprite
        switch (e.game)
        {
            case MiniGames.AppleBobbing:
                miniGameImage.sprite = appleImage;
                break;
            case MiniGames.CornMaze:
                miniGameImage.sprite = mazeImage;
                break;
            case MiniGames.PumpkinCarving:
                miniGameImage.sprite = pumpkinImage;
                break;
            default:
                break;
        }

        // Game difficulty
        switch (e.difficulty)
        {
            case 1:
                trickLevelText.text = "Easy";
                break;
            case 2:
                trickLevelText.text = "Normal";
                break;
            case 3:
                trickLevelText.text = "Hard";
                break;
            default:
                break;
        }

        // Candy Sprite
        switch (e.candy)
        {
            case 0:
                // no candy :(
                break;
            case 1:
                candyImage.sprite = candyOne;
                break;
            case 2:
                candyImage.sprite = candyOne;
                break;
            case 3:
                candyImage.sprite = candyTwo;
                break;
            case 4:
                candyImage.sprite = candyTwo;
                break;
            case 5:
                candyImage.sprite = candyThree;
                break;
            case 6:
                candyImage.sprite = candyThree;
                break;
            default:
                break;
        }

        // Candy Text
        candyCountText.text = e.candy.ToString() + " Candy!";

    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AppleBobbingManager : MonoBehaviour
{
    public GameObject apple_template;
    public GameObject bad_avocado_template;

    public float yMax = 4.5f;
    public float yMin = -4.5f;
    public float xMax = 9.0f;
    public float xMin = -9.0f;

    public int appleTarget = 10;
    private int currApples = 0;

    private bool playing = false;

    Subscription<PlayGameEvent> play_game_sub;
    Subscription<AppleScoreEvent> apple_score_sub;

    public TextMeshProUGUI appleScoreText;

    private int numApplesInSpawnRound = 3;
    private float timeBetweenSpawnRounds = 2.0f;
    private float currTime = 0.0f;
    private int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        play_game_sub = EventBus.Subscribe<PlayGameEvent>(onPlayGame);
        apple_score_sub = EventBus.Subscribe<AppleScoreEvent>(onAppleScored);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(play_game_sub);
        EventBus.Unsubscribe(apple_score_sub);
        StopAllCoroutines();
    }

    public void onPlayGame(PlayGameEvent e)
    {
        // Prepare the minigame ot be played by resetting variables and swapping the displayed score text
        if (e.game == MiniGames.AppleBobbing)
        {
            EventBus.Publish<ShowHintText>(new ShowHintText("Click on the Apples!"));
            currApples = 0;
            playing = true;

            appleScoreText.text = currApples.ToString() + "/" + appleTarget.ToString() + " Apples";
            appleScoreText.enabled = true;

            difficulty = e.difficulty;

            currTime = 0.0f;
        }
    }

    private IEnumerator WaitAfterEnd()
    {
        EventBus.Publish<ScoreEvent>(new ScoreEvent(difficulty * 2 + 1));
        yield return new WaitForSeconds(1.0f);
        appleScoreText.enabled = false;
        EventBus.Publish<BackToStreetEvent>(new BackToStreetEvent());
        EventBus.Publish<PlayerMovementEvent>(new PlayerMovementEvent(true));
        // Deactivate self

    }

    public void onAppleScored(AppleScoreEvent e)
    {
        currApples = Mathf.Max(currApples + e.addToScore, 0);
        appleScoreText.text = currApples.ToString() + " / " + appleTarget.ToString() + " Apples";

        // End the game
        if (currApples >= appleTarget && playing)
        {
            playing = false;
            StartCoroutine(WaitAfterEnd());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            currTime += Time.deltaTime;
            if (currTime >= timeBetweenSpawnRounds)
            {
                currTime = 0.0f;
                for (int i = 0; i < numApplesInSpawnRound; i++)
                {
                    Vector3 spawnLocation = new Vector3(0, 0, 0);
                    // Spawn apples randomly here away from the player's cursor
                    do
                    {
                        spawnLocation.x = Random.Range(xMin, xMax);
                        spawnLocation.y = Random.Range(yMin, yMax);

                    }
                    while (Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue())), transform.position + spawnLocation) <= 2.0f);

                    apple_template.GetComponent<Apple>().lifeSpan = 2.0f / (float)difficulty;
                    Instantiate(apple_template, this.transform.position + spawnLocation, new Quaternion(-0.7071068f, 0, 0, 0.7071068f));
                }

                // Spawn rotten apples on hard
                if (difficulty == 3)
                {
                    Vector3 spawnLocation = new Vector3(0, 0, 0);
                    do
                    {
                        spawnLocation.x = Random.Range(xMin, xMax);
                        spawnLocation.y = Random.Range(yMin, yMax);
                    } while (Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue())), transform.position + spawnLocation) <= 2.0f);

                    bad_avocado_template.GetComponent<Apple>().lifeSpan = 2.0f / (float)difficulty;
                    Instantiate(bad_avocado_template, this.transform.position + spawnLocation, new Quaternion(-0.7071068f, 0, 0, 0.7071068f));
                }
            } 
        }
    }
}

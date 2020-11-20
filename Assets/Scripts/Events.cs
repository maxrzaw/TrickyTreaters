using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum MiniGames
{
    PumpkinCarving,
    CornMaze,
    AppleBobbing
}

public class PlayGameEvent
{
    public MiniGames game = MiniGames.PumpkinCarving;
    public int difficulty = 1;
    public PlayGameEvent(MiniGames _game, int _difficulty)
    {
        game = _game;
        difficulty = _difficulty;
    }

    public override string ToString()
    {
        return "Playing game " + game.ToString() + " with difficulty " + difficulty;
    }
}

public class PlayerMovementEvent
{
    public bool moving = true;
    public PlayerMovementEvent(bool _moving) { moving = _moving; }
    public override string ToString()
    {
        return "Player moving = " + moving;
    }
}

public class ShowOptionsEvent
{
    public MiniGames game;
    public int difficulty;
    public int candy;
    public ShowOptionsEvent(MiniGames _game, int _difficulty, int _candy)
    {
        game = _game;
        difficulty = _difficulty;
        candy = _candy;
    }

    public override string ToString()
    {
        return "Showing Options: " + game.ToString() + " difficulty " + difficulty + " and " + candy + " candy";
    }
}

public class BackToStreetEvent
{
    public BackToStreetEvent() { }
    public override string ToString()
    {
        return "Back to Street";
    }
}

public class ScoreEvent
{
    public int addToScore = 0;
    public ScoreEvent(int add_to_score) { addToScore = add_to_score; }

    public override string ToString()
    {
        return "Adding " + addToScore + " candy!";
    }
}

public class AppleScoreEvent
{
    public int addToScore = 0;
    public AppleScoreEvent(int add_to_score) { addToScore = add_to_score; }

    public override string ToString()
    {
        return "Adding " + addToScore + " apples!";
    }
}


public class StartEvent
{
    public override string ToString()
    {
        return "Starting Game :)";
    }
}

public class MovePlayerEvent {
    public Vector3 location;
    public MovePlayerEvent(Vector3 newLocation) {
        location = newLocation;
    }
    public override string ToString() {
        return "Player moving to " + location.ToString();
    }
}

public class VisitHouseEvent
{
    public int house;
    public VisitHouseEvent(int _house) { house = _house; }
    public override string ToString()
    {
        return "Visiting House " + house.ToString() + "!";
    }
}

public class EndGameEvent
{
    public override string ToString()
    {
        return "Game Ending";
    }
}

public class ShowHintText
{
    public string text = "";
    public ShowHintText(string _text) { text = _text; }
    public override string ToString()
    {
        return "Showing hint: " + text;
    }
}
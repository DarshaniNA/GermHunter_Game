using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant {

    //login url
    //public const string LOGIN_URL = "http://gamerdata.gear.host/api/Login";
    //public const string SCORE_URL = "http://gamerdata.gear.host/api/Score";
    //public const string POST_DATA_URL = "http://gamerdata.gear.host/api/Players";

    public const string LOGIN_URL = "http://germhunter.gear.host/api/Login";
    public const string SCORE_URL = "http://germhunter.gear.host/api/Score";
    public const string POST_DATA_URL = "http://germhunter.gear.host/api/Players";

    //for sounds
    public const string SOUND_SHOOT = "shoot";
    public const string SOUND_PLAYER_ATTACK = "p_attack";
    public const string SOUND_ENEMY_ATTACK = "e_attack";
    public const string SOUND_PICKUP = "pickup";
    public const string SOUND_CLICK = "click";
    public const string SOUND_GAME = "gamesound";

    //for playerPrefs
    public const string PREFS_SOUND = "Sound";

    public const float LANE_DISTANCE = 2.5f;
    public const float TURN_SPEED = 0.05f;
    public const float DEADZONE = 100.0f;
    public const float DISTANCE_TO_RESPAWN = 10.0f;
    public const int COIN_SCORE_AMOUNT = 5;
    //private const bool SHOW_COLLIDERS = true;
    // Level spawning
    public const float DISTANCE_BEFORE_SPWAN = 150f;
    public const int INITIAL_SEGMENT = 10;
    public const int INITIAL_TRANSITION_SEGMENT = 2;
    public const int MAX_SEGMENT_ON_SCREEN = 15;

    public const string PANEL_HOME = "HomePanel";
    public const string PANEL_LOGIN = "LoginPanel";
    public const string PANEL_REGISTER = "RegisterPanel";

    //scenes
    public const string SCENE_START = "Start";
    public const string SCENE_GAME = "Game";

    public static bool CheckNetworkAvailability()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable) {
            return true;
        } else
        {
            return false;
        }
    }
}

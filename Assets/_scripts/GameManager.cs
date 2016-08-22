using UnityEngine;
using System.Collections;

using XInputDotNetPure;

public class GameManager : MonoBehaviour {

    public GameObject[] players_chars;
    public bool game_started;
    public bool game_over = false;

    private bool timer_started = false;

    private int nb_players = 0;
    private int nb_active_players = 0;
    public float timer_duration;

    private GUIText countdown_text;

    private bool end_game = false;

    private int previous_winner_index = -1;

    // Use this for initialization
    void Start () {
        players_chars[0] = GameObject.Find("Girouette");
        players_chars[1] = GameObject.Find("Character (1)");
        players_chars[2] = GameObject.Find("Character (2)");
        players_chars[3] = GameObject.Find("Character (3)");

        players_chars[0].GetComponent<GirouetteController>().char_activated = false;
        for (int i = 1; i < 4; ++i)
        {
            players_chars[i].GetComponent<CharController>().char_activated = false;
        }

        game_started = false;
        countdown_text = GetComponent<GUIText>();
        countdown_text.text = "Join";
        countdown_text.pixelOffset = new Vector2((Screen.width / 2),((float)2.6 * Screen.height / 3));

        AkSoundEngine.StopAll();
        AkSoundEngine.PostEvent("start_game", gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKey("escape"))
        {
            Application.Quit();
            Debug.Log("You can't exit in debug mode");
        }

        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel("scene");

        nb_active_players = 0;
        if (players_chars[0].GetComponent<GirouetteController>().char_activated)
            ++nb_active_players;
        for (int i = 1; i < 4; ++i)
        {
            if (players_chars[i].GetComponent<CharController>().char_activated)
                ++nb_active_players;
        }

        if(game_over)
        {
            timer_duration -= Time.deltaTime;
            if (timer_duration < 0)
            {
                RestartGame(previous_winner_index);
            }
        }

        if (end_game && !game_over)
        {
            timer_duration -= Time.deltaTime;
            if (timer_duration < 0)
            {
                int winner = 0;
                for (int i = 1; i < 4; ++i)
                {
                    if (players_chars[i].GetComponent<CharController>().char_activated)
                        winner = i;
                }
                ShowWinner(winner);
            }
            return;
        }

        if (game_started && !game_over)
        {
            timer_duration += Time.deltaTime;
            if (nb_active_players <= 2)
            {
                CallEndGame();
            }
            return;
        }

        if (timer_started)
        {
            UpdateTimer();
            return;
        }

        nb_players = 0;
        for (PlayerIndex index = PlayerIndex.One; index <= PlayerIndex.Four; ++index)
        {
            if (GamePad.GetState(index).IsConnected)
                ++nb_players;
        }

        

        /*Debug.Log("nb players :" + nb_players);
        Debug.Log("nb active players :" + nb_active_players);*/

        if (nb_players >= 2 && nb_players == nb_active_players)
        {
            StartTimer();
        }
	
	}

    void StartGame()
    {
        game_started = true;
        Debug.Log("Start game!");
    }

    void StartTimer()
    {
        timer_started = true;
        timer_duration = 3.0f;
    }

    void UpdateTimer()
    {
        timer_duration -= Time.deltaTime;
        //Debug.Log(timer_duration);

        string text = Mathf.Round(timer_duration).ToString();

        if (text != "0")
            countdown_text.text = text;
        else
            countdown_text.text = "";

        if (timer_duration < 0)
        {
            timer_started = false;
            StartGame();
        }
    }

    void CallEndGame()
    {
        timer_duration = 1.0f;
        end_game = true;
    }

    void ShowWinner(int winner)
    {
        /*if (winner == 0)
            players_chars[winner].GetComponent<GirouetteController>().WinAnimation();
        else
            players_chars[winner].GetComponent<CharController>().WinAnimation();*/

        countdown_text.text = "Player " + (winner+1) + " wins";

        game_over = true;

        previous_winner_index = winner;

        timer_duration = 3.0f;
        return;
    }

    void RestartGame(int previous_winner_index)
    {
        //reLOAD SCENE

        Application.LoadLevel("scene");

        /*if (previous_winner_index != 0)
        {

            GameObject.Find("Girouette").GetComponent<GirouetteController>().player_index = (PlayerIndex)previous_winner_index;
            

            if (GameObject.Find("Character (1)").GetComponent<CharController>().player_index == (PlayerIndex)previous_winner_index)
            {
                GameObject.Find("Character (1)").GetComponent<CharController>().player_index = (PlayerIndex)0;
            }
            else if (GameObject.Find("Character (2)").GetComponent<CharController>().player_index == (PlayerIndex)previous_winner_index)
            {
                GameObject.Find("Character (2)").GetComponent<CharController>().player_index = (PlayerIndex)0;
            }
            else if (GameObject.Find("Character (3)").GetComponent<CharController>().player_index == (PlayerIndex)previous_winner_index)
            {
                GameObject.Find("Character (3)").GetComponent<CharController>().player_index = (PlayerIndex)0;
            }

           

        }*/
    }
}

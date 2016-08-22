using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class GirouetteController : MonoBehaviour {

    public PlayerIndex player_index;
    public GameObject wind_particles;
    private windScript wind_script;

    GamePadState currentState;
    float xStick;

    public bool char_activated;
    bool b_Played;

    GameObject buttonA;
    GameObject player_indicator;

    // Use this for initialization
    void Start () {
        wind_particles = GameObject.Find("WindParticles");
        wind_script = GetComponent<windScript>();
        currentState = GamePad.GetState(player_index);
        xStick = currentState.ThumbSticks.Left.X;

        buttonA = transform.Find("button_A").gameObject;
        b_Played = false;

        switch (player_index)
        {
            case PlayerIndex.One:
                player_indicator = buttonA.transform.Find("P1").gameObject;
                break;
            case PlayerIndex.Two:
                player_indicator = buttonA.transform.Find("P2").gameObject;
                break;
            case PlayerIndex.Three:
                player_indicator = buttonA.transform.Find("P3").gameObject;
                break;
            case PlayerIndex.Four:
                player_indicator = buttonA.transform.Find("P4").gameObject;
                break;
        }

        if (currentState.IsConnected)
            player_indicator.GetComponent<MeshRenderer>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        // rotate girouette and wind
        if(char_activated)
        {
            currentState = GamePad.GetState(player_index);

            if (xStick != currentState.ThumbSticks.Left.X)
            {
                xStick = currentState.ThumbSticks.Left.X;
                //AkSoundEngine.PostEvent("girouette", gameObject);
            }
            
            this.transform.Rotate(new Vector3(0, 5 * currentState.ThumbSticks.Left.X, 0));
            wind_particles.GetComponent<Transform>().transform.RotateAround(Vector3.zero, Vector3.up, 5 * currentState.ThumbSticks.Left.X);
        }
        // modify speed of wind by using the micro
        wind_script.ApplyForce(MicInput.MicLoudness);

        if (!char_activated && GamePad.GetState(player_index).Buttons.A == ButtonState.Pressed)
        {
            if (!b_Played)
            {
                b_Played = true;

                if ((int)Random.Range(0, 1) == 1)
                    AkSoundEngine.PostEvent("ui1", gameObject);
                else
                    AkSoundEngine.PostEvent("ui2", gameObject);
            }

            char_activated = true;
            buttonA.GetComponent<MeshRenderer>().enabled = false;
            player_indicator.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void WinAnimation()
    {
        //bWinTrigger = true;

        transform.Rotate(new Vector3(0.0f, -25.0f, 0.0f));
        transform.Translate(new Vector3(0.0f, 0.25f, 0.0f));
    }
}

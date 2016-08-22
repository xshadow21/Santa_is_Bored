using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CharController : MonoBehaviour {

    public PlayerIndex player_index;
    public GameObject char_light;

    public bool char_activated;
    private Rigidbody body;
    private Light char_spotlight;
    private GameObject head;

    private GameManager game_man;

    public bool is_alive;
    bool b_Played;

    bool bWinTrigger;
    int nSkipSound;
    GameObject buttonA;
    GameObject player_indicator;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody>();
        body.isKinematic = true;

        char_spotlight = char_light.GetComponent<Light>();
        char_spotlight.enabled = false;

        head = this.transform.Find("Sphere").gameObject;
        game_man = GameObject.Find("GameManager").GetComponent<GameManager>();
        bWinTrigger = false;
        nSkipSound = 25;
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

        if (GamePad.GetState(player_index).IsConnected)
            player_indicator.GetComponent<MeshRenderer>().enabled = true;
        else
            buttonA.GetComponent<MeshRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (bWinTrigger)
        {
            AkSoundEngine.PostEvent("stop_game", gameObject);
            bWinTrigger = false;
        }

        GamePadState currentState = GamePad.GetState(player_index);

        if (char_activated && game_man.game_started)
        {

            body.isKinematic = false;
            is_alive = true;

            Vector3 stick_force = new Vector3(-5 * currentState.ThumbSticks.Left.X, 0.0f, -4 * currentState.ThumbSticks.Left.Y);
            body.AddForceAtPosition(stick_force, head.transform.position);

            if (Vector3.Dot(this.transform.up, new Vector3(0.0f, 1.0f, 0.0f)) < 0.85f)
            {
                if (nSkipSound >= 25)
                {
                    if (this.name == "Character (1)")
                        AkSoundEngine.PostEvent("c1", gameObject);
                    else if (this.name == "Character (2)")
                        AkSoundEngine.PostEvent("c2", gameObject);
                    else if (this.name == "Character (3)")
                        AkSoundEngine.PostEvent("c3", gameObject);

                    nSkipSound = 0;
                }

                nSkipSound++;
            }

            if (Vector3.Dot(this.transform.up, new Vector3(0.0f, 1.0f, 0.0f)) < 0.3f)
            {
                SpringJoint[] springJoints = GetComponents<SpringJoint>();
                foreach (SpringJoint joint in springJoints)
                {
                    if (this.name == "Character (1)")
                        AkSoundEngine.PostEvent("c1_fall", gameObject);
                    else if (this.name == "Character (2)")
                        AkSoundEngine.PostEvent("c2_fall", gameObject);
                    else if (this.name == "Character (3)")
                        AkSoundEngine.PostEvent("c3_fall", gameObject);

                    Destroy(joint);
                    char_activated = false;
                    is_alive = false;
                }
            }
        }
        else
        {
            if (currentState.Buttons.A == ButtonState.Pressed)
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
                char_spotlight.enabled = true;
                buttonA.GetComponent<MeshRenderer>().enabled = false;
                player_indicator.GetComponent<MeshRenderer>().enabled = false;

                //WinAnimation();
            }
            else if (game_man.game_started)
            {
                SpringJoint[] springJoints = GetComponents<SpringJoint>();
                foreach (SpringJoint joint in springJoints)
                {
                    Destroy(joint);
                }
                body.isKinematic = false;
                buttonA.GetComponent<MeshRenderer>().enabled = false;
                

            }

        }
		
		//Debug.Log (.ToString());

    }

    public void WinAnimation()
    {
        //bWinTrigger = true;

        transform.Rotate(new Vector3(0.0f, -25.0f, 0.0f));
        transform.Translate(new Vector3(0.0f, 0.25f, 0.0f));
    }
}

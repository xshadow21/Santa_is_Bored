using UnityEngine;
using System.Collections;

using XInputDotNetPure;

public class JoinScript : MonoBehaviour {

    PlayerIndex player_index;
    int nNbPlayerActivated;

    GameObject playerInstance0;
    GameObject playerInstance1;
    GameObject playerInstance2;
    GameObject playerInstance3;

    bool bJoinPress;
    int[] nJoinedArray;

    // Use this for initialization
    void Start () {
        nNbPlayerActivated = 0;
        bJoinPress = false;

        nJoinedArray = new int[4];
        for (int j = 0; j < 4; j++)
        {
            nJoinedArray[j] = 4;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*for (int i = 0; i < 4; i++)
        {
            GamePadState currentState = GamePad.GetState((PlayerIndex)i);

            //check to see if the player pushed the A button
            if (currentState.Buttons.A == ButtonState.Pressed)
            {
                bJoinPress = true;
            }

            if (currentState.Buttons.A == ButtonState.Released && bJoinPress)
                StartCoroutine(Vibrate(0.25f, (PlayerIndex)i));
        }*/

        /////////////////////////////

        /*Debug.LogFormat("OOOOOOH");
        for (int i = 0; i < 4; i++)
        {
            if (nJoinedArray[i] == 4)
            {
                Debug.LogFormat("YEAAAAH");
                Debug.LogFormat(((PlayerIndex)i).ToString());
                GamePadState currentState = GamePad.GetState((PlayerIndex)i);

                //check to see if the player pushed the A button
                if (currentState.Buttons.A == ButtonState.Pressed)
                {
                    bJoinPress = true;
                }

                if (currentState.Buttons.A == ButtonState.Released && bJoinPress)
                {
                    bJoinPress = false;

                    Debug.LogFormat("SET ITTTTT***********************");
                    Debug.LogFormat(((PlayerIndex)i).ToString());
                    Debug.LogFormat(nNbPlayerActivated.ToString());

                    nJoinedArray[i] = i;

                    // Set player index
                    if (playerInstance0 == null)
                    {
                        Debug.LogFormat("FIRST");
                        playerInstance0 = GameObject.Find("Girouette");
                        playerInstance0.GetComponent<GirouetteController>().player_index = (PlayerIndex)i;
                    }
                    else if (playerInstance1 == null)
                    {
                        Debug.LogFormat("DOS");
                        playerInstance1 = GameObject.Find("Character (1)");
                        playerInstance1.GetComponent<CharController>().player_index = (PlayerIndex)i;
                    }
                    else if (playerInstance2 == null)
                    {
                        Debug.LogFormat("TRES");
                        playerInstance2 = GameObject.Find("Character (2)");
                        playerInstance2.GetComponent<CharController>().player_index = (PlayerIndex)i;
                    }
                    else if (playerInstance3 == null)
                    {
                        Debug.LogFormat("QUATRO");
                        playerInstance3 = GameObject.Find("Character (3)");
                        playerInstance3.GetComponent<CharController>().player_index = (PlayerIndex)i;
                    }

                    StartCoroutine(Vibrate(0.25f, (PlayerIndex) i));
                }

                Debug.LogFormat("IF CHECK DONE");
            }

            Debug.LogFormat("BOUCLE DONE");
        }
        /*else
        {
            //NOTE:  Doesn't work with some XInput emulated device drivers like the popular PS3 Controller one
            //destroy the player instance if the controller disconnects
            if (currentState.IsConnected == false)
            {
                Destroy(playerInstance0);
                return;
            }
            else
            {

                //destroy the player instance if the player pushed the Back button
                if (currentState.Buttons.Back == ButtonState.Pressed)
                {
                    Destroy(playerInstance0);
                    return;
                }
            }
        }*/
    }

    IEnumerator Vibrate(float duration, PlayerIndex p_player)
    {
        GamePad.SetVibration(p_player, 1.0f, 0.0f);
        yield return new WaitForSeconds(duration);
        GamePad.SetVibration(p_player, 0.0f, 0.0f);
    }
}

using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class SplashManager : MonoBehaviour {

    public float splash_duration;

	// Use this for initialization
	void Start () {
        GetComponent<GUITexture>().pixelInset.Set(Screen.width / 2, Screen.height / 2, 0.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKey("escape"))
        {
            Application.Quit();
            Debug.Log("You can't exit in debug mode");
        }


        splash_duration -= Time.deltaTime;

        for (int i = 0; i < 4; i++)
        {
            GamePadState currentState = GamePad.GetState((PlayerIndex)i);
            if (currentState.Buttons.A == ButtonState.Pressed)
                Application.LoadLevel("scene");
        }

        if (splash_duration < 0 || Input.GetKeyDown(KeyCode.Space))
            Application.LoadLevel("scene");

    }
}
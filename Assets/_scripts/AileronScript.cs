using UnityEngine;
using System.Collections;

public class AileronScript : MonoBehaviour
{
    int nNbFrameTurn;
    int nDistance;
    public int MAX_DISTANCE_ADVANCE;
    public int MAX_DISTANCE_TURN;
    public float VIRAGE_TURN;
    bool bIsTurning;

    // Use this for initialization
    void Start ()
    {
        nNbFrameTurn = 0;
        nDistance = 0;
        bIsTurning = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!bIsTurning && nDistance < MAX_DISTANCE_ADVANCE)
        {
            Advance();
            nDistance++;
        }
        else
        {
            nDistance = 0;
            bIsTurning = HalfTurn();
        }
    }

    void Advance()
    {
        transform.Translate(new Vector3(0.0f, -0.2f, 0.0f));
    }

    bool HalfTurn()
    {
        if (nNbFrameTurn < MAX_DISTANCE_TURN)
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, VIRAGE_TURN));
            transform.Translate(new Vector3(0.0f, -0.2f, 0.0f));
            nNbFrameTurn++;
        }
        else
        {
            nNbFrameTurn = 0;
            return false;
        }

        return true;
    }
}

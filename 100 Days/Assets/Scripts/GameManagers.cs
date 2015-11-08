using UnityEngine;
using System.Collections;

public class GameManagers : MonoBehaviour {

    public int days;
    public int Hour;
    public int r1;
    public int r2;
    public bool DayTime;
	// Use this for initialization
	void Update () {
            if ( Input.GetKeyDown(KeyCode.E) == true )
            {
                EndTurn();
            }
            if ( Hour ==18 || Hour == 0)
            {
                DayTime = false;
            }
            if (Hour == 6 || Hour ==12)
            {
                DayTime = true;
            }
	}

    void EndTurn()
    {
        Hour += 6;
        if (Hour == 24)
        {
            Hour = 0;
            days++;
        }
    }

}

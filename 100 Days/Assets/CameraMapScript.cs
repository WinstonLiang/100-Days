using UnityEngine;
using System.Collections;

public class CameraMapScript : MonoBehaviour {
	
	// Update is called once per frame

     public int triggerMove;
     public float moveIncrement;

	void Update () {
          if (Input.mousePosition.x < triggerMove)
               transform.position += new Vector3(-moveIncrement, 0, 0);
          else if (Input.mousePosition.x > Screen.width - triggerMove)
               transform.position += new Vector3(moveIncrement, 0, 0);
          if (Input.mousePosition.y < triggerMove)
               transform.position += new Vector3(0, -moveIncrement, 0);
          else if (Input.mousePosition.y > Screen.height - triggerMove)
               transform.position += new Vector3(0, moveIncrement, 0);
	}
}

using UnityEngine;
using System.Collections;

public class CameraMapScript : MonoBehaviour {
	
	// Update is called once per frame

     public int triggerMove;
     public float moveIncrement;
     public int min_x;
     public int max_x;
     public int min_y;
     public int max_y;
     private float mousePosX;
     private float mousePosY;

	void Update () {
          mousePosX = Input.mousePosition.x;
          mousePosY = Input.mousePosition.y;
          if (mousePosX < triggerMove && transform.position.x > min_x)
               transform.position += new Vector3(-moveIncrement, 0, 0);
          else if (mousePosX > Screen.width - triggerMove && transform.position.x < max_x)
               transform.position += new Vector3(moveIncrement, 0, 0);
          if (mousePosY < triggerMove && transform.position.y > min_y)
               transform.position += new Vector3(0, -moveIncrement, 0);
          else if (mousePosY > Screen.height - triggerMove && transform.position.y < max_y)
               transform.position += new Vector3(0, moveIncrement, 0);
	}
}

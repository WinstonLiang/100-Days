using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CameraMapScript : MonoBehaviour {

     public int triggerMove;
     public float moveIncrement, mousePosX, mousePosY;
     public int min_x, min_y, max_x, max_y;
     public float dragFactor = 1.0f;

     public  GameObject tileInfo;

    void Start()
     {
         tileInfo = GameObject.Find("Tile Info");
         tileInfo.SetActive(false);
     }

	void Update () 
    {
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

    public void OnDrag(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;

        if (pointerData.button.ToString() == "Right")
        {
            transform.position += new Vector3(pointerData.delta.x * dragFactor * 0.01f, pointerData.delta.y * dragFactor * 0.01f, 0);

            if (tileInfo.activeSelf)
            {
                tileInfo.transform.position += new Vector3(pointerData.delta.x * dragFactor * 0.01f, pointerData.delta.y * dragFactor * 0.01f, 0);
            }
        }
    }
}

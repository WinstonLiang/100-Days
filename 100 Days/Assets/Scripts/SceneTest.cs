using UnityEngine;
using System.Collections;

public class SceneTest : MonoBehaviour {
     public int count = 0;

	// Use this for initialization
	void Start () {
          print("start");
          if (Application.loadedLevelName == "TestScene")
          {
               Application.LoadLevel("WinstonTest");
          }
	}
	
	// Update is called once per frame
	void Update () {
          count++;
          print(count);
          if (count > 1000)
          {
               Application.LoadLevel("TestScene");
          }
	}
}

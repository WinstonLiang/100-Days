using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTest : MonoBehaviour {
     public int count = 0;

	// Use this for initialization
	void Start () {
          print("start");
          if (SceneManager.GetActiveScene().name == "TestScene")
          {
              SceneManager.LoadScene("WinstonTest");
          }
	}
	
	// Update is called once per frame
	void Update () {
          count++;
          print(count);
          if (count > 1000)
          {
              SceneManager.LoadScene("TestScene");
          }
	}
}

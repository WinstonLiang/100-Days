using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {

    string armySceneName = "MenuArmy";
    string squadSceneName = "MenuSquad";
    string mapSceneName = "MapNode";
    string rdSceneName = "MenuRD";

    public void loadArmyScene()
    {
        if (SceneManager.GetActiveScene().name != armySceneName)
            SceneManager.LoadScene(armySceneName);
    }

    public void loadSquadScene()
    {
        if (SceneManager.GetActiveScene().name != squadSceneName)
            SceneManager.LoadScene(squadSceneName);
    }

    public void loadMapScene()
    {
        if (SceneManager.GetActiveScene().name != mapSceneName)
            SceneManager.LoadScene(mapSceneName);
    }

    public void loadRDScene()
    {
        if (SceneManager.GetActiveScene().name != rdSceneName)
            SceneManager.LoadScene(rdSceneName);
    }
}

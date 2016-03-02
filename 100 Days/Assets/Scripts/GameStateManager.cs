using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameStateManager : MonoBehaviour 
{    
    // All the data that needs to be saved goes in here
    [System.Serializable]
    public class AllUnits
    {
        public List<UnitClass> allPlayerUnits;
        public List<UnitClass>[][] allEnemyUnits;
    }

    public void saveGameData()
    {
        // Fill in allUnits class using data from unitManagerScript
        UnitManager unitManagerScript = GameObject.Find("UnitsData").GetComponent<UnitManager>();
        AllUnits allUnits = new AllUnits();
        allUnits.allPlayerUnits = unitManagerScript.allPlayerUnits;
        allUnits.allEnemyUnits = unitManagerScript.allEnemyUnits;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.dat");
        bf.Serialize(file, allUnits);
        file.Close();

        // Set PlayerPrefs saved flag to enable continue button on start menu
        PlayerPrefs.SetInt("saves", 1);
        print("Saved data.");
    }

    public static void loadGameData()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.dat"))
        {
            UnitManager unitManagerScript = GameObject.Find("UnitsData").GetComponent<UnitManager>();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.dat", FileMode.Open);
            AllUnits allunits = (AllUnits)bf.Deserialize(file);
            file.Close();

            // Load the data from AllUnits class
            unitManagerScript.allPlayerUnits = allunits.allPlayerUnits;
            unitManagerScript.allEnemyUnits = allunits.allEnemyUnits;
            print("Savefile found, loading...");
        }
        else
        {
            print("No savefile found.");
            SceneManager.LoadScene("StartMenu");
        }            
    }
}

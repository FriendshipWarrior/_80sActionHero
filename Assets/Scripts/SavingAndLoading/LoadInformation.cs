using UnityEngine;
using System.Collections;

public class LoadInformation : MonoBehaviour {

	public static void LoadAllInformation()
    {
        GameInformation.PlayerName = PlayerPrefs.GetString("PLAYERNAME");
        GameInformation.PlayerLevel = PlayerPrefs.GetInt("PLAYERLEVEL");
        GameInformation.Stamina = PlayerPrefs.GetInt("STAMINA");
        GameInformation.Strength = PlayerPrefs.GetInt("STRENGTH");
        GameInformation.Intelligence = PlayerPrefs.GetInt("INTELLIGENCE");
    }
}

using UnityEngine;
using System.Collections;

public class SaveInformation : MonoBehaviour {

	public static void SaveAllInformation()
    {
        PlayerPrefs.SetInt("PLAYERLEVEL",GameInformation.PlayerLevel);
        PlayerPrefs.SetString("PLAYERNAME", GameInformation.PlayerName);
        PlayerPrefs.SetInt("STAMINA", GameInformation.Stamina);
        PlayerPrefs.SetInt("STRENGTH", GameInformation.Strength);
        PlayerPrefs.SetInt("INTELLIGENCE", GameInformation.Intelligence);
    }
}

using UnityEngine;
using System.Collections;

public class CreateCharacter : MonoBehaviour {

    private BasePlayer newPlayer;
    private bool isWarrior;
    private bool isRanger;
    private bool isMage;
    private string playerName = "Enter Name";
	// Use this for initialization
	void Start () {
        newPlayer = new BasePlayer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        playerName = GUILayout.TextArea(playerName, 15);
        isWarrior = GUILayout.Toggle(isWarrior, "Warrior Class");
        isRanger = GUILayout.Toggle(isRanger, "Ranger Class");
        isMage = GUILayout.Toggle(isMage, "Mage Class");
        if (GUILayout.Button("Create")) {
            if (isWarrior)
            {
                newPlayer.PlayerClass = new BaseWarriorClass();
            } else if (isRanger)
            {
                newPlayer.PlayerClass = new BaseRangerClass();
            } else if (isMage)
            {
                newPlayer.PlayerClass = new BaseMageClass();
            }
            newPlayer.PlayerLevel = 0;
            newPlayer.Stamina = newPlayer.PlayerClass.Stamina;
            newPlayer.Strength = newPlayer.PlayerClass.Strength;
            newPlayer.Intelligence = newPlayer.PlayerClass.Intelligence;
            newPlayer.PlayerName = playerName;
            StoreNewPlayerInfo();
            SaveInformation.SaveAllInformation();
        }
        if (GUILayout.Button("Load"))
        {
            Application.LoadLevel("test");
        }
    }


    private void StoreNewPlayerInfo()
    {
        GameInformation.PlayerName = newPlayer.PlayerName;
        GameInformation.PlayerLevel = newPlayer.PlayerLevel;
        GameInformation.Stamina = newPlayer.Stamina;
        GameInformation.Strength = newPlayer.Strength;
        GameInformation.Intelligence = newPlayer.Intelligence;
    }
}

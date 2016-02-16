using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasePlayer
{

    private List<BaseStat> _playerStats = new List<BaseStat>();
    private List<BaseItem> _inventory = new List<BaseItem>();
    private string playerName;
    private int playerLevel;
    private CharacterClass playerClass;

    public string PlayerName { get; set; }
    public int PlayerLevel { get; set; }
    public CharacterClass PlayerClass { get; set; }
}

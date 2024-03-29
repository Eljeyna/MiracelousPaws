using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem Instance { get; private set; }

    private string json;

    private void Awake()
    {
        if (ReferenceEquals(Instance, null))
        {
            Instance = this;
        }

        if (!Directory.Exists(Game._SAVE_FOLDER))
        {
            Directory.CreateDirectory(Game._SAVE_FOLDER);
        }
    }

    public void Save()
    {
        //if (Player.Instance.fightCount > 0 || GameDirector.Instance.noControl)
        if (GameDirector.Instance.noControl)
        {
            return;
        }
        
        StringBuilder sb = new StringBuilder(Path.Combine(Game._SAVE_FOLDER, "save0.json"));
        
        json = string.Empty;
        foreach (var monoBehaviour in FindObjectsOfType<MonoBehaviour>())
        {
            if (monoBehaviour is ISaveState persist)
            {
                json += persist.Save();
            }
        }
        
        File.WriteAllText(sb.ToString(), json);
    }

    public async Task Load()
    {
        if (GameDirector.Instance.noControl)
        {
            return;
        }
        
        await Preload();
        AfterLoadSystem.Instance.Load();
    }

    public async Task Preload()
    {
        foreach (var monoBehaviour in FindObjectsOfType<MonoBehaviour>())
        {
            if (monoBehaviour is ISaveState persist)
            {
                await persist.Load();
            }
        }
    }
}

public struct Saveable
{
    public string scene;
    public string[] questID;
    public int[] questTask;
    public Dictionary<string, int> completedQuestsID;
    public string activeQuestID;
    
    public string[] itemsID;
    public int[] itemsAmount;

    public string[] buffsID;
    public float[] buffsDuration;
    public int[] buffsStacks;

    public int maxStamina;
    public int stamina;
    public int staminaRegen;
    public float staminaTimeRegen;
    /*
    public int level;
    public int exp;
    public int talentPoints;
    public float weight;
    public int strength;
    public int agility;
    public int intelligence;
    public int oratory;
    */
    public int qualitativeMaterial;
    public int badQualityMaterial;

    public float maxHealth;
    public float health;
    public float healthPercent;
    public float[] resistances;
    public bool invinsibility;

    /*
    public string head;
    public string torso;
    public string legs;
    public string foots;
    */

    public string weapon;

    public bool finishTutorial;
    /*
    public string weaponRanged;
    public float positionX;
    public float positionY;
    */
}

public struct CompletedQuestsID
{
    public Dictionary<string, int> completedQuestsID;
}

internal interface ISaveState
{
    string Save();
    Task Load();
}

using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.Networking;
using static StaticGameVariables;

public class Translate : MonoBehaviour
{
    public static Translate Instance { get; private set; }
    public Dictionary<string, string> translationUI = new Dictionary<string, string>();

    private int language = -1;

    private void Awake()
    {
        if (ReferenceEquals(Instance, null))
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        
        InitializeLanguage();
    }

    public void GetTranslate()
    {
        if (language != (int)StaticGameVariables.language)
        {
            language = (int)StaticGameVariables.language;
        }
        
        StringBuilder sb = new StringBuilder(GetAsset(Path.Combine("Localization", languageKeys[language], "UI.json")));

#if UNITY_ANDROID
        if (sb.ToString() == string.Empty)
        {
            return;
        }
        
        translationUI = JsonConvert.DeserializeObject<Dictionary<string, string>>(sb.ToString());
#else
        if (!File.Exists(sb.ToString()))
        {
            return;
        }
        
        translationUI = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(sb.ToString()));
#endif
        if (translationUI == null)
        {
            return;
        }
    
        foreach (var monoBehaviour in FindObjectsOfType<MonoBehaviour>())
        {
            if (monoBehaviour is ITranslate persist)
            {
                persist.GetTranslate();
            }
        }
    }
}

internal interface ITranslate
{
    void GetTranslate();
}
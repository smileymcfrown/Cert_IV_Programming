using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyBinder : MonoBehaviour
{
    [SerializeField] public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    [System.Serializable]
    public struct KeyUISetup
    {
        public string keyName;
        public Text keyDisplayText;
        public string defaultKey;
    }

    public KeyUISetup[] baseSetup;
    public GameObject currentKey;
    public Color32 changedKey = new Color32(128, 128, 128,255);
    public Color32 selectedKey = new Color32(64, 64, 64,255);

    public void SetKeyText()
    {
        for (int i = 0; i < baseSetup.Length; i++)
        {
            baseSetup[i].keyDisplayText.text = keys[baseSetup[i].keyName].ToString(); 
        }
    }
    
    public void SetDefaultKeys()
    {
        for (int i = 0; i < baseSetup.Length; i++)
        {
            keys.Add(baseSetup[i].keyName, (KeyCode)System.Enum.Parse(typeof(KeyCode),baseSetup[i].defaultKey));
            baseSetup[i].keyDisplayText.text = keys[baseSetup[i].keyName].ToString();
            if (SettingsData.settingsData.keyBinds.ContainsKey(baseSetup[i].keyName))
            {
                SettingsData.settingsData.keyBinds[baseSetup[i].keyName] = baseSetup[i].defaultKey;
            }
            else
            {
                SettingsData.settingsData.keyBinds.Add(baseSetup[i].keyName,baseSetup[i].defaultKey);
            }
        }

        String keyBindings = "Default keys: ";
        foreach (var key in SettingsData.settingsData.keyBinds)
        {
            keyBindings += key.Key + ": " + key.Value + " , ";
        }
        Debug.Log(keyBindings);
    }
    
    public void LoadKeys()
    {
        for (int i = 0; i < baseSetup.Length; i++)
        {
            keys.Add(baseSetup[i].keyName, (KeyCode)System.Enum.Parse(typeof(KeyCode),
                SettingsData.settingsData.keyBinds[baseSetup[i].keyName]));
            baseSetup[i].keyDisplayText.text = keys[baseSetup[i].keyName].ToString();
        }
        
        String keyBindings = "Loaded Keys: ";
        foreach (var key in SettingsData.settingsData.keyBinds)
        {
            keyBindings += key.Key + ": " + key.Value + " , ";
        }
        Debug.Log(keyBindings);
    }

    public void ChangeKey(GameObject clickedKey)
    {
        if (clickedKey != null)
        {
            currentKey = clickedKey;
            currentKey.GetComponent<Image>().color = selectedKey;
        }
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            SettingsData.settingsData.keyBinds[key.Key] = key.Value.ToString();
            // PlayerPrefs.SetString(key.Key,key.Value.ToString());
        }
        // PlayerPrefs.Save();
    }

    private void OnGUI()
    {
        string newKey = "";
        Event e = Event.current;
        if (currentKey != null)
        {
            if (e.isKey)
            {
                newKey = e.keyCode.ToString();
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                newKey = "LeftShift";
            }

            if (Input.GetKey(KeyCode.RightShift))
            {
                newKey = "RightShift";
            }

            if (newKey != "")
            {
                keys[currentKey.name] = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKey);
                currentKey.GetComponentInChildren<Text>().text = newKey;
                currentKey.GetComponent<Image>().color = changedKey;
                currentKey = null;
                
            }
        }

    }
}

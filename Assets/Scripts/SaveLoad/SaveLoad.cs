﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using EasyButtons;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [SerializeField]
    GameController gm;

    [Button]
    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();

        // Add Surrogates for missing serializers
        SurrogateSelector surrogateSelector = new SurrogateSelector();

        Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();
        surrogateSelector.AddSurrogate(typeof(Vector3),
            new StreamingContext(StreamingContextStates.All),
            vector3Surrogate);

        bf.SurrogateSelector = surrogateSelector;

        // Get a timestamp for the file name
        DateTime dt = DateTime.UtcNow;
        string fileName = dt.ToString("ddmmyy-HH-mm");

        FileStream stream = new FileStream(Application.persistentDataPath + "/saved_game.sav", FileMode.Create);

        PlayerData data = new PlayerData();

        // Get Moth Position
        data.MothPosition = gm.Moth.transform.position;
        // Get Camera Position
        data.CameraPosition = gm.GameCamera.transform.position;

        // Get all interactables
        Interactable[] interactables = GameObject.FindObjectsOfType<Interactable>();
        // Get all interactable states
        Dictionary<int, bool> dict = new Dictionary<int, bool>();
        foreach (var item in interactables)
        {
            dict.Add(item.gameObject.GetInstanceID(), item.HasPlayed);
            if(item.gameObject.name == "fragment_tattoo")
            {
                print(item.gameObject.GetInstanceID());
            }
        }
        data.InteractableStates = dict;

        bf.Serialize(stream, data);
        stream.Close();
    }

    [Button]
    public void Load()
    {
        if (File.Exists((Application.persistentDataPath + "/saved_game.stls")))
        {
            FileStream stream = new FileStream(Application.persistentDataPath + "/saved_game.stls", FileMode.Open);
            PlayerData data = new PlayerData();

            // Disable StoryEvent System
            GameObject StoryEventSystem = GameObject.Find("StoryEvent System");
            if(StoryEventSystem != null)
            {
                StoryEventSystem.SetActive(false);
            }
            // Disable ParticleSystems
            try
            {
                BinaryFormatter bf = new BinaryFormatter();

                // Add Surrogates for missing serializers
                SurrogateSelector surrogateSelector = new SurrogateSelector();

                Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();
                surrogateSelector.AddSurrogate(typeof(Vector3),
                    new StreamingContext(StreamingContextStates.All),
                    vector3Surrogate);

                bf.SurrogateSelector = surrogateSelector;

                data = (PlayerData) bf.Deserialize(stream);
            }
            catch (SerializationException ex)
            {
                Debug.LogError("Could not deserialize the saved_game.stls");
                Debug.LogError(ex.Message);
            }
            finally
            {
                stream.Close();
            }

            // Set moth position
            gm.Moth.transform.position = data.MothPosition;
            // Set Camera Position
            gm.GameCamera.transform.position = data.CameraPosition;

            // Get all instance ID's of objects with Interactable
            Interactable[] allInteractables = GameObject.FindObjectsOfType<Interactable>();
            Dictionary<int, GameObject> idGameObjectDict = new Dictionary<int, GameObject>();

            foreach(var item in allInteractables)
            {
                idGameObjectDict.Add(item.gameObject.GetInstanceID(), item.gameObject);
            }

            // Set Interactable states
            Dictionary<int, bool> loadedStates = data.InteractableStates;

            foreach (var item in idGameObjectDict)
            {
                Interactable component = item.Value.GetComponent<Interactable>();
                component.HasPlayed = loadedStates[item.Key];
                if(component.HasPlayed)
                {
                    if(component is Puzzle)
                    {
                        ((Puzzle)component).SolvePuzzleNow();
                    }
                    
                    component.InvokeInteractableCall();
                }
            }

            // Enable StoryEvent System
            if(StoryEventSystem != null)
            {
                StoryEventSystem.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Save file does not exist");
        }
    }
}

[System.Serializable]
public class PlayerData
{
    [SerializeField]
    public Vector3 MothPosition;
    [SerializeField]
    public Vector3 CameraPosition;

    public Dictionary<int, bool> InteractableStates;
}

sealed class Vector3SerializationSurrogate : ISerializationSurrogate
{

    // Method called to serialize a Vector3 object
    public void GetObjectData(System.Object obj,
        SerializationInfo info, StreamingContext context)
    {

        Vector3 v3 = (Vector3) obj;
        info.AddValue("x", v3.x);
        info.AddValue("y", v3.y);
        info.AddValue("z", v3.z);
    }

    // Method called to deserialize a Vector3 object
    public System.Object SetObjectData(System.Object obj,
        SerializationInfo info, StreamingContext context,
        ISurrogateSelector selector)
    {

        Vector3 v3 = (Vector3) obj;
        v3.x = (float) info.GetValue("x", typeof(float));
        v3.y = (float) info.GetValue("y", typeof(float));
        v3.z = (float) info.GetValue("z", typeof(float));
        obj = v3;
        return obj;
    }
}
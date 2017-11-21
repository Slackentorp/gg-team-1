using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;



public class SaveLoad : MonoBehaviour
{
   
    public class PlayerData
    {
        public float positionOfCamera;
        public float cx, cy, cz;
        public float positionOfMoth;
        public float mx, my, mz;

        public string fragment;

        public int[] doors;
        public float[] positionsOfCamera;
        public float[] positionsOfMoth;
        public string[] fragments;
    }

    public int fs = 2;
    public static void SaveGame()
    {//in the brackets put the palce where to save from
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/gameSave.sav", FileMode.Create);

        PlayerData data = new PlayerData();//here is the relevant information
        data.positionOfCamera = 2;
        bf.Serialize(stream, data);
        stream.Close();
    }
   
    public static void LoadPlayer()
    {
        if (File.Exists((Application.persistentDataPath + "/gameSave.sav")))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/gameSave.sav", FileMode.Open);


            PlayerData data = bf.Deserialize(stream) as PlayerData;

            //PlayerData data = bf.Deserialize(stream);

            stream.Close();
            
            
        }
        else
        {
            Debug.Log("Save file does not exist");
        }
    }

    public void Load()
    {
        

    }

}
/*
public void Save()
{
    SaveLoad.SaveGame(this);//if its in the class we want it to be,
}
*/
//just an example
[Serializable]
    public class PlayerData2
        {
            public int[] settings;


        public PlayerData2()
            {
                settings = new int[4];//check it out
                settings[0] = 1;
                settings[1] = 2;
                settings[2] = 3;
                settings[3] = 4;
            }
        }

/*
   public void Save()
   {
       SaveLoad.SaveGame(this);//for example
   }
   public void Load()
   {
       int[] loadedStats = SaveLoad.LoadPlayer();
       level = loadedStats[0];


   }
   */
   
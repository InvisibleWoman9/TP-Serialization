using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;


//[Serializable]
public class SaveAndLoadScript : MonoBehaviour
{
    public GameObject[] cubes;


    void Start()
    {
        cubes = GameObject.FindGameObjectsWithTag("MOVETHIS");
        Load();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) Save();
        if(Input.GetKeyDown(KeyCode.Alpha2)) Load();
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (File.Exists(Application.persistentDataPath + "/save.dat")) File.Delete(Application.persistentDataPath + "/save.dat");
            SceneManager.LoadScene(0);
            
        }
    }

    void OnApplicationQuit()
    {
        Save();
    }
    
    void Save()
    {
        if(cubes.Length < 1) return;
        Debug.Log("Saving");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.OpenOrCreate);
        SaveData saveData = new SaveData ();
        
        saveData.cubepositionsX = new float[cubes.Length];
        saveData.cubepositionsY = new float[cubes.Length];
        saveData.cubepositionsZ = new float[cubes.Length];
        
        saveData.cuberotationsW = new float[cubes.Length];
        saveData.cuberotationsX = new float[cubes.Length];
        saveData.cuberotationsY = new float[cubes.Length];
        saveData.cuberotationsZ = new float[cubes.Length];
        
        saveData.cubecolorsR = new float[cubes.Length];
        saveData.cubecolorsG = new float[cubes.Length];
        saveData.cubecolorsB = new float[cubes.Length];
        
        for (int i = 0; i < cubes.Length; i++)
        {
            saveData.cubepositionsX[i] = cubes[i].transform.position.x;
            saveData.cubepositionsY[i] = cubes[i].transform.position.y;
            saveData.cubepositionsZ[i] = cubes[i].transform.position.z;
            Debug.Log(saveData.cubepositionsX[i] + " " + saveData.cubepositionsY[i] + " " + saveData.cubepositionsZ[i]);
            
            saveData.cuberotationsW[i] = cubes[i].transform.rotation.w;
            saveData.cuberotationsX[i] = cubes[i].transform.rotation.x;
            saveData.cuberotationsY[i] = cubes[i].transform.rotation.y;
            saveData.cuberotationsZ[i] = cubes[i].transform.rotation.z;
            
            MeshRenderer cubematerial = cubes[i].GetComponentInChildren<MeshRenderer>();
            
            saveData.cubecolorsR[i] = cubematerial.material.color.r;
            saveData.cubecolorsG[i] = cubematerial.material.color.g;
            saveData.cubecolorsB[i] = cubematerial.material.color.b;

        }
        bf.Serialize(file, saveData);
        file.Close();
    }
    
    void Load()
    {
        Debug.Log("Loading");
        if (File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);
            SaveData saveData = bf.Deserialize(file) as SaveData;
            file.Close();

            if (saveData.cubepositionsX.Length != cubes.Length)
            {
                Save();
                return;
            } 
            
            for (int i = 0; i < cubes.Length; i++)
            {
                Vector3 cubeposition = new Vector3( saveData.cubepositionsX[i], saveData.cubepositionsY[i], saveData.cubepositionsZ[i]);
                Debug.Log(cubeposition);
                cubes[i].transform.position = cubeposition;
                
                Quaternion cuberotation = new Quaternion( saveData.cuberotationsX[i], saveData.cuberotationsY[i], saveData.cuberotationsZ[i], saveData.cuberotationsW[i]);
                cubes[i].transform.rotation = cuberotation;
                
                Color cubecolor = new Color(saveData.cubecolorsR[i], saveData.cubecolorsG[i], saveData.cubecolorsB[i]);
                cubes[i].GetComponentInChildren<MeshRenderer>().material.color = cubecolor;

            }
        }
        
    }
}

[System.Serializable]
public class SaveData
{
    public float[] cubepositionsX;
    public float[] cubepositionsY;
    public float[] cubepositionsZ;
    
    public float[] cuberotationsW;
    public float[] cuberotationsX;
    public float[] cuberotationsY;
    public float[] cuberotationsZ;
    
    public float[] cubecolorsR;
    public float[] cubecolorsG;
    public float[] cubecolorsB;
    
}
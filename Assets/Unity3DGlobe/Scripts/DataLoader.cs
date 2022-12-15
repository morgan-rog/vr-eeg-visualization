using UnityEngine;
using System.Collections;

public class DataLoader : MonoBehaviour {
    public DataVisualizer Visualizer;
   // public DataViz Visualizer;
    // Use this for initialization
    void Start () {
        //loads selected json data set
        TextAsset jsonData = Resources.Load<TextAsset>("simulated_braincoordinates");
        //TextAsset jsonData = Resources.Load<TextAsset>("NeoBrainCoordinates");
        string json = jsonData.text;
        SeriesArray data = JsonUtility.FromJson<SeriesArray>(json);
        //accesses all data variable where all the timestamp arrays are stored. 
        Visualizer.CreateMeshes(data.AllData);

    }
	
	void Update () {
	
	}
}
[System.Serializable]
public class SeriesArray
{
    // makes the AllData public and avaliable to all classes. 
    public SeriesData[] AllData;
}
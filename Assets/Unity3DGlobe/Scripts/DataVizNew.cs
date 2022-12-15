using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataVizNew: MonoBehaviour
{
    public Material PointMaterial;
    public Gradient Colors;
    //access Brain game object here. 
    public GameObject Brain;
    public GameObject PointPrefab;
    public float ValueScaleMultiplier = 1;
    GameObject[] SObjects;
    int currentSeries = 0;
    //acessing the AllData through the SeriesArray class in the DataLoader script
    //used to access the mesh and create the render
    public void CreateMeshes(SeriesData[] AllSeries)
    {
        SObjects = new GameObject[AllSeries.Length];
        GameObject p = Instantiate<GameObject>(PointPrefab);
        Vector3[] verts = p.GetComponent<MeshFilter>().mesh.vertices;
        int[] indices = p.GetComponent<MeshFilter>().mesh.triangles;

        //What is meshVertices?
        List<Vector3> meshVertices = new List<Vector3>(65000);
        //What is meshIndices?
        List<int> meshIndices = new List<int>(117000);
        List<Color> meshColors = new List<Color>(65000);

        for (int i = 0; i < AllSeries.Length; i++)
        {
            GameObject SObj = new GameObject(AllSeries[i].Name);
            SObj.transform.parent = Brain.transform;
            SObjects[i] = SObj;
            SeriesData Sdata = AllSeries[i];
            for (int j = 0; j <Sdata.Data.Length; j += 3)
            {
                float lat = Sdata.Data[j];
                float lng = Sdata.Data[j + 1];
                float value = Sdata.Data[j + 2];
                AppendPointVertices(p, verts, indices, lng, lat, value, meshVertices, meshIndices, meshColors);
                if (meshVertices.Count + verts.Length > 65000)
                {
                    //Create actual render here? Using meshVertices, meshIndices, meshColors, SObjs
                    CreateObject(meshVertices, meshIndices, meshColors, SObj);
                    meshVertices.Clear();
                    meshIndices.Clear();
                    meshColors.Clear();
                }
            }
            CreateObject(meshVertices, meshIndices, meshColors, SObj);
            meshVertices.Clear();
            meshIndices.Clear();
            meshColors.Clear();
            SObjects[i].SetActive(false);
        }


        SObjects[currentSeries].SetActive(true);
        Destroy(p);
    }
    private void AppendPointVertices(GameObject p, Vector3[] verts, int[] indices, float lng, float lat, float value, List<Vector3> meshVertices,
    List<int> meshIndices,
    List<Color> meshColors)
    {
        ///used to correlate color with signal strength
        Color valueColor = Colors.Evaluate(value);
        Vector3 pos;
        /// actual x,y, z coordinates for vertex positions
        pos.x = 0.5f * Mathf.Cos((lng) * Mathf.Deg2Rad) * Mathf.Cos(lat * Mathf.Deg2Rad);
        pos.y = 0.5f * Mathf.Sin(lat * Mathf.Deg2Rad);
        pos.z = 0.5f * Mathf.Sin((lng) * Mathf.Deg2Rad) * Mathf.Cos(lat * Mathf.Deg2Rad);
        p.transform.parent = Brain.transform;
        p.transform.position = pos;
        ////VERY IMPORTANT, this is the width and volume of each rendered data point.
        p.transform.localScale = new Vector3(1, 1, Mathf.Max(0.001f, value * ValueScaleMultiplier));
        p.transform.LookAt(pos * 2);

        int prevVertCount = meshVertices.Count;

        for (int k = 0; k < verts.Length; k++)
        {
            meshVertices.Add(p.transform.TransformPoint(verts[k]));
            meshColors.Add(valueColor);
        }
        for (int k = 0; k < indices.Length; k++)
        {
            meshIndices.Add(prevVertCount + indices[k]);
        }
    }
    private void CreateObject(List<Vector3> meshertices, List<int> meshindecies, List<Color> meshColors, GameObject seriesObj)
    {
        //create the mesh for the visualization
        Mesh mesh = new Mesh();
        //accesses the SeriesArray using the List.ToArray method
        mesh.vertices = meshertices.ToArray();
        //NOT THE SAME AS MeshIndices?
        mesh.triangles = meshindecies.ToArray();
        mesh.colors = meshColors.ToArray();
        //creates the GameObject in Unity scene
        GameObject obj = new GameObject();
        obj.transform.parent = Brain.transform;
        obj.AddComponent<MeshFilter>().mesh = mesh;
        obj.AddComponent<MeshRenderer>().material = PointMaterial;
        obj.transform.parent = seriesObj.transform;
    }
    //change current active data set array here
    public void ActivateSeries(int seriesIndex)
    {
        if (seriesIndex >= 0 && seriesIndex < SObjects.Length)
        {
            //Log the number of datasets
            //Debug.Log (seriesObjects.Length);
            SObjects[currentSeries].SetActive(false);
            currentSeries = seriesIndex;
            SObjects[currentSeries].SetActive(true);

        }
    }
}
[System.Serializable]
public class NewSeriesData
{
    public string Name;
    public float[] Data;
}

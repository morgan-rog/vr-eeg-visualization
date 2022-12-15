using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataVisualizer : MonoBehaviour {
    public Material PointMaterial;
    public Gradient Colors;
    //access Brain game object here. 
    public GameObject Brain;
    public GameObject PointPrefab;
    public float ValueScaleMultiplier = 1;
    GameObject[] seriesObjects;
    int currentSeries = 0;
    //acessing the AllData through the SeriesArray class in the DataLoader script
    public void CreateMeshes(SeriesData[] allSeries)
    {
        //Length is looking at the length of the allSeries the length of the json file datasets. 
        seriesObjects = new GameObject[allSeries.Length];
        GameObject p = Instantiate<GameObject>(PointPrefab);
        Vector3[] verts = p.GetComponent<MeshFilter>().mesh.vertices;
        int[] indices = p.GetComponent<MeshFilter>().mesh.triangles;

        List<Vector3> meshVertices = new List<Vector3>(65000);
        List<int> meshIndices = new List<int>(11700);
        List<Color> meshColors = new List<Color>(65000);

        for (int i = 0; i < allSeries.Length; i++)
        {
            GameObject seriesObj = new GameObject(allSeries[i].Name);
            seriesObj.transform.parent = Brain.transform;
            seriesObjects[i] = seriesObj;
            SeriesData seriesData = allSeries[i];
            for (int j = 0; j < seriesData.Data.Length; j+=3)
            {
                float lat = seriesData.Data[j];
                float lng = seriesData.Data[j + 1];
                float value = seriesData.Data[j + 2];
                AppendPointVertices(p, verts, indices, lng, lat, value, meshVertices, meshIndices, meshColors);
                //changing the greater then for meshVertice.Count seems to do nothing?
                if (meshVertices.Count + verts.Length > 65000)
                {
                    CreateObject(meshVertices, meshIndices, meshColors, seriesObj);
                    meshVertices.Clear();
                    meshIndices.Clear();
                    meshColors.Clear();
                }
            }
            CreateObject(meshVertices, meshIndices, meshColors, seriesObj);
            meshVertices.Clear();
            meshIndices.Clear();
            meshColors.Clear();
            seriesObjects[i].SetActive(false);
        }


        seriesObjects[currentSeries].SetActive(true);
        Destroy(p);
    }
    private void AppendPointVertices(GameObject p, Vector3[] verts, int[] indices, float lng,float lat,float value, List<Vector3> meshVertices,
    List<int> meshIndices,
    List<Color> meshColors)
    {
        Color valueColor = Colors.Evaluate(value);
        Vector3 pos;
        pos.x = 0.1f * Mathf.Cos((lng) * Mathf.Deg2Rad) * Mathf.Cos(lat * Mathf.Deg2Rad);
        pos.y = 0.1f * Mathf.Sin(lat * Mathf.Deg2Rad);
        pos.z = 0.1f * Mathf.Sin((lng) * Mathf.Deg2Rad) * Mathf.Cos(lat * Mathf.Deg2Rad);
        p.transform.parent = Brain.transform;
        p.transform.position = pos;
        ////VERY IMPORTANT, this is the width and volume of each rendered data point.
        p.transform.localScale = new Vector3(20, 20, Mathf.Max(0.01f, value * ValueScaleMultiplier));
        //change orientation of data points.
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
        //the visualization is instastited as a GameObject
        Mesh mesh = new Mesh();
        mesh.vertices = meshertices.ToArray();
        mesh.triangles = meshindecies.ToArray();
        mesh.colors = meshColors.ToArray();
        GameObject obj = new GameObject();
        obj.transform.parent = Brain.transform;
        obj.AddComponent<MeshFilter>().mesh = mesh;
        obj.AddComponent<MeshRenderer>().material = PointMaterial;
        obj.transform.parent = seriesObj.transform;
    }
    //change current active data set array here
    public void ActivateSeries(int seriesIndex)
    {
        if (seriesIndex >= 0 && seriesIndex < seriesObjects.Length)
        {
            //Log the number of datasets
            //Debug.Log (seriesObjects.Length);
            seriesObjects[currentSeries].SetActive(false);
            currentSeries = seriesIndex;
            seriesObjects[currentSeries].SetActive(true);

        }
    }
}
[System.Serializable]
public class SeriesData
{
    public string Name;
    public float[] Data;
}

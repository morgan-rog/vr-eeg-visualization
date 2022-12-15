using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bx3_main : MonoBehaviour
{


    GameObject participant;
    GameObject l_hem;
    GameObject r_hem;

    Vector3[] vertices_l;
    Mesh mesh_l;
    Vector3[] vertices_r;
    Mesh mesh_r;
    public List<Vector3> MNI;

    //public float[] distances;
    List<GameObject> col_vertices_l;
    List<GameObject> col_vertices_r;

    Color[] colors4surfP; 

    GameObject locator;
    int maxElec;
    bool once;
    private void Awake()
    {
     
    }


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log ("hello world");
        //log.LogMessage("hello world");

        l_hem = GameObject.Find("Left-Cerebral-Cortex");
        r_hem = GameObject.Find("Right-Cerebral-Cortex");

        mesh_l = l_hem.GetComponent<MeshFilter>().mesh;
        vertices_l = mesh_l.vertices;

        mesh_r = r_hem.GetComponent<MeshFilter>().mesh;
        vertices_r = mesh_r.vertices;

        createColliders();

        Debug.Log(vertices_l.Length);

        createElectrodes();

        maxElec = 13;
        colors4surfP = new Color[maxElec];

        colors4surfP[0] = Color.black;
        colors4surfP[1] = new Color(0.25f, 0, 0);
        colors4surfP[2] = new Color(0.5f, 0, 0);
        colors4surfP[3] = new Color(0.75f, 0f, 0);
        colors4surfP[4] = new Color(1, 0f, 0);
        colors4surfP[5] = new Color(1f, .25f, 0);
        colors4surfP[6] = new Color(1, 0.5f, 0);
        colors4surfP[7] = new Color(1, 0.75f, 0);
        colors4surfP[8] = new Color(1, 1, 0); 
        colors4surfP[9] = new Color(1, 1, 0.2f); 
        colors4surfP[10] = new Color(1, 1, 0.4f);
        colors4surfP[11] = new Color(1, 1, 0.6f);
        colors4surfP[12] = new Color(1f, 1f, 0.8f);




    }

    void Update()
    {
    
        // this should be in the start function but info from colliders is not updated

        Color[] colors_l = new Color[vertices_l.Length];
        for (int i = 0; i < col_vertices_l.Count; i++)
        {
            if (col_vertices_l[i].GetComponent<col_component>().ncloseElectrodes >= maxElec)
            {
                colors_l[i] = new Color(1, 1, 1f);
            }

            for (int j = 0; j < colors4surfP.Length; j++)
            {
                if (col_vertices_l[i].GetComponent<col_component>().ncloseElectrodes == j)
                    colors_l[i] = colors4surfP[j];
            }


        }
        mesh_l.colors = colors_l;





    }

    private int getMaxElec()
    {
        int max = 0;
        for (int i = 0; i < col_vertices_l.Count; i++)
        {
            int num = col_vertices_l[i].GetComponent<col_component>().ncloseElectrodes;
            //Debug.Log(num);
            if (num > max)
            {
                max = num;
            }
        }
        return max;
    }


    void createColliders()
    {
        col_vertices_l = new List<GameObject>();
        GameObject colliders_vL = new GameObject("Vertex_colliders_L"); 
        foreach (Vector3 v3 in vertices_l)
        {
            GameObject go = new GameObject("vertex_L");
            go.transform.position = v3;
            go.transform.parent = colliders_vL.transform;
            go.AddComponent<SphereCollider>();
            go.AddComponent<col_component>();
            col_vertices_l.Add(go);
        }


    }

    void createElectrodes()
    {

        GameObject goP = new GameObject("Electrodes");
        GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        s.AddComponent<Rigidbody>().isKinematic = true;
        s.GetComponent<SphereCollider>().isTrigger = true;
        s.GetComponent<SphereCollider>().radius = 12.5f; // original radius is doubled

        for (int i = 0; i < MNI.Count; i++)
        {
            GameObject go = Instantiate(s);
            go.transform.parent = goP.transform;
            go.transform.position = MNI[i];
            //go.transform.localScale = new Vector3(2, 2, 2);
            go.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Specular"));
            go.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        goP.transform.eulerAngles = new Vector3(-90, 0, 0);
        Destroy(s);

    }



}

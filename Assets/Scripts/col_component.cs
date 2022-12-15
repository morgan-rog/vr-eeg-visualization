using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col_component : MonoBehaviour
{
    bx3_main bx3;
    //public List<int> closeElectrodes;
    public int ncloseElectrodes;
    // Start is called before the first frame update
    void Start()
    {
        bx3 = GameObject.Find("brainX3").GetComponent<bx3_main>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ncloseElectrodes++;   
    }

    private void OnTriggerExit(Collider other)
    {
        ncloseElectrodes--;
    }


}

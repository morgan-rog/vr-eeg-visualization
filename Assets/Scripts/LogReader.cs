using UnityEngine;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

public class LogReader : MonoBehaviour {


public bool readLog;

bx3_main bx3; 
StreamReader reader;
public List <string> lines;
public List <float> times;

string message;

int index;

float counter;
    // Use this for initialization

void Awake(){

        bx3 = GameObject.Find("brainX3").GetComponent<bx3_main>();
        //Debug.Log(exp);
        //Debug.Log(Application.dataPath);

        readLogfromText();


    }

void Start () {


}


    public void readLogfromText()
    {
        FileInfo txt = new FileInfo(Application.dataPath + "/MNI/" + "example_coordinates.csv");
        lines = new List<string>();
        if (txt != null)
        {
            reader = txt.OpenText();
        }
        string xT = reader.ReadToEnd();
        //Debug.Log (xT);
        char[] lineSeparator = new char[] { '\n' };
        string[] linesS = xT.Split(lineSeparator);
        for (int i = 0; i <= linesS.Length - 1; i++)
        {
            lines.Add(linesS[i]);
            //Debug.Log (linesS[i]);
        }

        char[] dataSeparator = new char[] { ';' };
        char[] colon = new char[] { ',' };

        foreach (string line in lines)
        {
            //Debug.Log(line);
            if (line != null)
            {
                if (line == "#Starting log sesion")
                    continue;
                if (line == "#Ending log sesion")
                    break;
                string[] data = line.Split(dataSeparator);

                //Debug.Log(data.Length);

                if (data.Length == 4 && data[1] != "RAS-Slicer")
                {
                    char[] dataSeparator1 = new char[] { ' ' };
                    string[] datainrow = data[2].Split(dataSeparator1);
                    //Debug.Log(datainrow.Length);
                    int count = 0;
                    string[] realdatainrow = new string[3];
                    for (int i = 0; i < datainrow.Length; i++)
                    {
                        if (datainrow[i] != "[" && datainrow[i] != " " && datainrow[i] != "]" && datainrow[i] != "")
                        {
                            //Debug.Log(datainrow[i]);
                            realdatainrow[count] = datainrow[i];
                            count++;
                            //Debug.Log(realdatainrow[count]);

                        }

                    }
                    float mniX = float.Parse(realdatainrow[0].Replace("[", ""));
                    float mniY = float.Parse(realdatainrow[1]);
                    //Debug.Log(datainrow[2]);
                    float mniZ = float.Parse(realdatainrow[2].Replace("]", ""));
                    //Debug.Log(mniZ);

                    bx3.MNI.Add (new Vector3(mniX, mniY, mniZ));

                }
            }


        }
    }
    }


        





	




























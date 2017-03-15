using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class DataDriven : MonoBehaviour {

    private GameObject mUIManager;
    private GameObject mBeatManager;

    public List<string> readBeatTags;
    public List<Vector3> readBeatPos;

    private const string FOLDERLOC = "/DataDrivenTexts/";
    private const string PATTERNNAME = "PatternName: ";
    private const string BEATLOC = "BeatPos: ";
    private const string BEATTAG = "BeatTag: ";
    private const string FILEEXT = ".txt";

	// Use this for initialization
	void Start () {

        mUIManager = GameObject.FindGameObjectWithTag("UIManager");
        mBeatManager = GameObject.FindGameObjectWithTag("BeatManager");

        readBeatTags = new List<string>();
        readBeatPos = new List<Vector3>();

	}

    public void writeToFile()
    {
        string name = mUIManager.GetComponent<UIManager>().fileName.text + FILEEXT;
        string patternName = PATTERNNAME + mUIManager.GetComponent<UIManager>().patternName.text;

        if (name != null)
        {
            StreamWriter writer = new StreamWriter(Application.dataPath + FOLDERLOC + name, true);
            writer.WriteLine(patternName);

            string tag;
            string pos;

            for (int i = 0; i < mBeatManager.GetComponent<BeatManager>().beatsList.Count; i++)
            {
                tag = BEATTAG + mBeatManager.GetComponent<BeatManager>().beatsList[i].tag;
                pos = BEATLOC + mBeatManager.GetComponent<BeatManager>().beatsList[i].transform.localPosition;

                writer.WriteLine(tag);
                writer.WriteLine(pos);
            }

            writer.WriteLine("\n\n");
            writer.Close();
        }
    }

    public void readFromFile()
    {
        readBeatTags.Clear();
        readBeatPos.Clear();

        string name = mUIManager.GetComponent<UIManager>().fileName.text + FILEEXT;
        string patternName = PATTERNNAME + mUIManager.GetComponent<UIManager>().patternName.text;

        if (name != null)
        {
            StreamReader reader = new StreamReader(Application.dataPath + FOLDERLOC + name);

            string line;
            bool onSelectedPattern = false;
            while (reader.Peek() >= 0)
            {
                line = reader.ReadLine();
                if(line.StartsWith(patternName))
                {
                    onSelectedPattern = true;
                }
                else if (line.StartsWith(PATTERNNAME))
                {
                    onSelectedPattern = false;
                }

                if(onSelectedPattern)
                {
                    if(line.StartsWith(BEATTAG))
                    {
                        //print(line);
                        string newline = line.TrimStart(BEATTAG.ToCharArray());
                        readBeatTags.Add(newline);
                        //print(newline);
                    }
                    else if (line.StartsWith(BEATLOC))
                    {
                        string newline = line.TrimStart(BEATLOC.ToCharArray());

                        if (newline.StartsWith("(") && newline.EndsWith(")"))
                        {
                            newline = newline.Substring(1, newline.Length - 2);
                        }

                        // split the items
                        string[] sArray = newline.Split(',');

                        // store as a Vector3
                        Vector3 pos = new Vector3(float.Parse(sArray[0]), float.Parse(sArray[1]), float.Parse(sArray[2]));

                        readBeatPos.Add(pos);
                        //print(pos);
                    }
                }
            }

            reader.Close();
        }
    }
}

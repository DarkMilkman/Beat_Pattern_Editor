using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeatManager : MonoBehaviour {

    private Vector3 XBeatStartPos = new Vector3(-1.5f, 0.5f, 0.0f);
    private Vector3 YBeatStartPos = new Vector3(-0.5f, 0.5f, 0.0f);
    private Vector3 BBeatStartPos = new Vector3(0.5f, 0.5f, 0.0f);
    private Vector3 ABeatStartPos = new Vector3(1.5f, 0.5f, 0.0f);

    public GameObject aBeatGO;
    public GameObject bBeatGO;
    public GameObject xBeatGO;
    public GameObject yBeatGO;

    private GameObject mUIManager;
    private GameObject mDataDriven;

    private GameObject mParentObject;

    public List<GameObject> beatsList;
    public GameObject[] beatPrefabs;
    private Vector3[] mBeatStartPos;

	// Use this for initialization
	void Start () {

        mUIManager = GameObject.FindGameObjectWithTag("UIManager");
        mDataDriven = GameObject.FindGameObjectWithTag("DataDrivenManager");
        mParentObject = GameObject.FindGameObjectWithTag("BeatParentObject");

        beatsList = new List<GameObject>();

        mBeatStartPos = new[] { ABeatStartPos, XBeatStartPos, BBeatStartPos, YBeatStartPos };
	}
	
	// Update is called once per frame
	void Update () {

        checkToInstantiate();
	}

    public void clearAllBeats()
    {
        for(int i = 0; i < beatsList.Count; i++)
        {
            Destroy(beatsList[i].gameObject);
        }

        beatsList.Clear();
    }

    public void deleteBeat()
    {
        for(int i = 0; i < beatsList.Count; i++)
        {
            if(beatsList[i].GetComponent<BeatScript>().isClicked)
            {
                Destroy(beatsList[i].gameObject);
                beatsList.RemoveAt(i);
            }
        }
    }

    public void changePos(float diatance)
    {
        for (int i = 0; i < beatsList.Count; i++)
        {
            if (beatsList[i].GetComponent<BeatScript>().isClicked)
            {
                Vector3 newPos = beatsList[i].transform.position;
                newPos.y += diatance;

                beatsList[i].transform.position = newPos;
            }
        }
    }

    public void createLoadedPattern()
    {
        clearAllBeats();

        if(mDataDriven.GetComponent<DataDriven>().readBeatPos.Count == mDataDriven.GetComponent<DataDriven>().readBeatTags.Count)
        {
            for(int i = 0; i < mDataDriven.GetComponent<DataDriven>().readBeatPos.Count; i++)
            {
                string tag = mDataDriven.GetComponent<DataDriven>().readBeatTags[i];
                Vector3 pos = mDataDriven.GetComponent<DataDriven>().readBeatPos[i];

                for(int j = 0; j < beatPrefabs.Length; j++)
                {
                    if(beatPrefabs[j].tag.ToUpper() == tag.ToUpper())
                    {
                        instatiateBeat(beatPrefabs[j], pos, beatPrefabs[j].transform.rotation, true);
                    }
                }
            }
        }
    }

    void checkToInstantiate()
    {
        //print(beatsList.Count);
        if (mUIManager.GetComponent<UIManager>().prefabDropDown.value != 0)
        {
            if (mUIManager.GetComponent<UIManager>().prefabDropDown.value == 1)
            {
                instatiateBeat(beatPrefabs[0], mBeatStartPos[0], beatPrefabs[0].transform.rotation, false);

                mUIManager.GetComponent<UIManager>().resetDropDown();
            }
            else if (mUIManager.GetComponent<UIManager>().prefabDropDown.value == 2)
            {
                instatiateBeat(beatPrefabs[1], mBeatStartPos[1], beatPrefabs[1].transform.rotation, false);

                mUIManager.GetComponent<UIManager>().resetDropDown();
            }
            else if (mUIManager.GetComponent<UIManager>().prefabDropDown.value == 3)
            {
                instatiateBeat(beatPrefabs[2], mBeatStartPos[2], beatPrefabs[2].transform.rotation, false);

                mUIManager.GetComponent<UIManager>().resetDropDown();
            }
            else if (mUIManager.GetComponent<UIManager>().prefabDropDown.value == 4)
            {
                instatiateBeat(beatPrefabs[3], mBeatStartPos[3], beatPrefabs[3].transform.rotation, false);

                mUIManager.GetComponent<UIManager>().resetDropDown();
            }
        }
    }

    private void instatiateBeat(GameObject beatGO, Vector3 pos, Quaternion rot, bool fromFile)
    {
        if (fromFile)
        {
            GameObject go = Instantiate(beatGO, pos, rot) as GameObject;

            go.transform.parent = mParentObject.transform;
            //go.transform.TransformPoint(pos);
            mParentObject.transform.TransformPoint(pos);
            go.transform.localPosition = pos;

            beatsList.Add(go);
        }
        else
        {
            GameObject go = Instantiate(beatGO, pos, rot) as GameObject;

            go.transform.parent = mParentObject.transform;

            beatsList.Add(go);
        }
    }
}

using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour {

    private GameObject controlsText;
    private GameObject mBeatsManager;
    private GameObject mDataDriven;

    private bool mShowControlsText;

	// Use this for initialization
	void Start () {

        mShowControlsText = true;

        controlsText = GameObject.FindGameObjectWithTag("Texts");
        mBeatsManager = GameObject.FindGameObjectWithTag("BeatManager");
        mDataDriven = GameObject.FindGameObjectWithTag("DataDrivenManager");
	}
	
	// Update is called once per frame
	void Update () {

        checkInput();
	}

    void checkInput()
    {
        //move beat
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScroll != 0)
        {
            mBeatsManager.GetComponent<BeatManager>().changePos(mouseScroll);
        }

        //hide controls or show them
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mShowControlsText = !mShowControlsText;

            if(mShowControlsText)
            {
                controlsText.SetActive(true);
            }
            else
            {
                controlsText.SetActive(false);
            }
        }

        //clears
        if(Input.GetKeyDown(KeyCode.C))
        {
            mBeatsManager.GetComponent<BeatManager>().clearAllBeats();
        }

        //deletes
        if (Input.GetKeyDown(KeyCode.D))
        {
            mBeatsManager.GetComponent<BeatManager>().deleteBeat();
        }

        //saves to file
        if (Input.GetKeyDown(KeyCode.F1))
        {
            print("Saved");
            mDataDriven.GetComponent<DataDriven>().writeToFile();
        }

        //loads from a file
        if (Input.GetKeyDown(KeyCode.F3))
        {
            mDataDriven.GetComponent<DataDriven>().readFromFile();
            mBeatsManager.GetComponent<BeatManager>().createLoadedPattern();
        }
    }
}

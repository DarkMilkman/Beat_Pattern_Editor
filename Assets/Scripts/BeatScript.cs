using UnityEngine;
using System.Collections;

public class BeatScript : MonoBehaviour {

    public bool isClicked;

	// Use this for initialization
	void Start () {

        isClicked = false;
	}
	
	// Update is called once per frame
	void Update () {

        if(isClicked)
            checkClicked();
    }

    void checkClicked()
    {
        //unclicks on beat
        if (Input.GetMouseButtonUp(0))
        {
            isClicked = false;
        }
    }

    void OnMouseOver()
    {
        //clicks on beat
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
        }
    }

}

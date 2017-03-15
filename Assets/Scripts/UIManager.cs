using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Dropdown prefabDropDown;
    public InputField fileName;
    public InputField patternName;

	// Use this for initialization
	void Start () {

	}

    public void resetDropDown()
    {
        prefabDropDown.value = 0;
    }
}

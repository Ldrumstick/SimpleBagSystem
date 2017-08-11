using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(2))
        {
            int index = Random.Range(0, 8);
            BagManager._instance.StoreItem(index);
        }
	}
}

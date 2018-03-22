using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        var pickable = other.GetComponent<Pickable>();

        GameObject.Destroy(pickable.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public Transform targetTransform;
    public float distanceBehind;
    public float distanceOver;

	// Update is called once per frame
	void Update ()
    {
        if (targetTransform == null)
        {
            Debug.LogWarning("FollowCamera does not have a target set.");
            return;
        }

        var targetPosition = targetTransform.position
            + targetTransform.forward * -distanceBehind
            + Vector3.up * distanceOver;

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);
        transform.LookAt(targetTransform);
	}
}

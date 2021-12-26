using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFace : MonoBehaviour {

    [SerializeField]
	int value;

	// To detect the dice number that is up when it stops
	void OnTriggerStay(Collider col) {
		if (Dice.Instance.GetVelocity() == Vector3.zero) {
			if (col.gameObject.name == "DicePlatform") {
				Dice.Instance.SetValue(value);
			}
		}
	}
}

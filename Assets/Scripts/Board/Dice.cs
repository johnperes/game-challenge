using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {

	public static Dice Instance { get; private set; }
	void Awake() {
		Instance = this;
		rb = GetComponent<Rigidbody>();
	}

	[SerializeField]
	[Tooltip("Starting height of the dice")]
	float heightPosition = 22f;

	[SerializeField]
	[Tooltip("Minimun random direction number to apply a torque to the dice")]
	float randomDirectionRangeMin = -700f;

	[SerializeField]
	[Tooltip("Maximum random direction number to apply a torque to the dice")]
	float randomDirectionRangeMax = 700f;

	[SerializeField]
	[Tooltip("Minimun random launch force to apply to the dice")]
	float launchForceMin = 450f;

	[SerializeField]
	[Tooltip("Maximum random launch force to apply to the dice")]
	float launchForceMax = 550f;

	[SerializeField]
	[Tooltip("Minimun mass of the dice, to increase randomness")]
	float massMin = 0.9f;

	[SerializeField]
	[Tooltip("Maximum mass of the dice, to increase randomness")]
	float massMax = 1.1f;

	int currentValue = 0;

	static Rigidbody rb;

	bool isRolling = false;

	// Roll the dice randomly
	public void Launch() {
		currentValue = 0;
		float dirX = UnityEngine.Random.Range(randomDirectionRangeMin, randomDirectionRangeMax);
		float dirY = UnityEngine.Random.Range(randomDirectionRangeMin, randomDirectionRangeMax);
		float dirZ = UnityEngine.Random.Range(randomDirectionRangeMin, randomDirectionRangeMax);
		transform.position = new Vector3 (0, heightPosition, 0);
		transform.rotation = Quaternion.identity;
		rb.mass = UnityEngine.Random.Range(massMin, massMax);
		rb.AddForce (transform.up * UnityEngine.Random.Range(launchForceMin, launchForceMax));
		rb.AddTorque (dirX, dirY, dirZ, ForceMode.Impulse);
	}

	void Update() {
		// Check when dice stops, or when it starts rolling
		if (GetVelocity() == Vector3.zero && isRolling) {
			isRolling = false;

		} else if (GetVelocity() != Vector3.zero && !isRolling) {
			isRolling = true;
		}
	}

	// Gets the dice actual velocity, to check if it stoped
	public Vector3 GetVelocity() {
		return rb.velocity;
	}

	// Gets the dice value
	public int GetValue()
	{
		return currentValue;
	}

	// Sets the dice value, when it stops
	public void SetValue(int value) {
		currentValue = value;
	}

	// Changes the dice material, do differentiate between players
	public void ChangeMaterial(Material mat) {
		GetComponent<MeshRenderer>().material = mat;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    // Singleton
    public static EffectController Instance { get; private set; }
    void Awake() {
        Instance = this;
    }

    public void PlayDiceCollide(Vector3 position) {
        Instantiate(Resources.Load<GameObject>("Prefabs/Effects/DiceCollideEffect"), position, Quaternion.Euler(-90, 0, 0));
    }

    public void PlayGetHit(Vector3 position) {
        Debug.Log("Get hit effect played");
        Instantiate(Resources.Load<GameObject>("Prefabs/Effects/GetHitEffect"), position, Quaternion.Euler(-90, 0, 0));
    }

    public void PlayItemGet(Vector3 position) {
        Instantiate(Resources.Load<GameObject>("Prefabs/Effects/ItemGetEffect"), position, Quaternion.Euler(-90, 0, 0));
    }

    public void PlayWalk(Vector3 position, Quaternion direction) {
        Instantiate(Resources.Load<GameObject>("Prefabs/Effects/PlayerWalkEffect"), position, direction);
    }
}

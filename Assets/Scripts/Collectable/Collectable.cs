using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum CollectableType { AttackPower, ExtraDice, ExtraMove, Heal, COUNT };

    CollectableType type;

    [SerializeField]
    int quantity = 1;

    public void SetType(CollectableType typeParam) {
        type = typeParam;
    }

    public void Pick(PlayerAttributes playerAttributes) {
        switch (type) {
            case CollectableType.AttackPower:
                playerAttributes.AttackBuff(quantity);
                break;
            case CollectableType.ExtraDice:
                playerAttributes.DiceBuff(quantity);
                break;
            case CollectableType.ExtraMove:
                playerAttributes.MovementBuff(quantity);
                break;
            case CollectableType.Heal:
                playerAttributes.Heal(quantity);
                break;
        }
        // Plays the particle effect
        EffectController.Instance.PlayItemGet(transform.position);
        // Plays the sound effect
        AudioController.Instance.Play(AudioController.AudioType.GetItem);
    }
}

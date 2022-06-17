using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMineable<T> {
    int Health {get; set;}
    void TakeDamage(T dmg);
}

public interface IGrabable<T> {
    void Pickup(T inventory);
}

public interface IInteractable {}
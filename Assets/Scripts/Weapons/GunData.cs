﻿using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GunData/New Gun Data")]
public class GunData : ScriptableObject
{
    public WeaponDamageType damageType;
    public float damage;
    public float radius;
    public float range;
    public float delay;
    public float lateDelay;
    public float fireRatePrimary;
    public float fireRateSecondary;
    public float impactForce;

    public float reloadTime;

    public bool autoreload = true;

    public int maxClip;
    public int maxAmmo;
}

﻿using System;
using UnityEngine;

public class ShootController : MonoBehaviour {

	public float ShotDuration = 0.7f;
    public Transform GunEnd;
    public Transform Camera;

    private Weapon SelectedWeapon;
	private float nextFireTime;
    private float FireRate;
	private GameOverlord GameOverlord;

    void Start ()
	{
		GameOverlord = GameObject.FindGameObjectWithTag("Overlord").GetComponent<GameOverlord>();
		SelectedWeapon = GameOverlord.SelectedPlayerWeapon;
		FireRate = SelectedWeapon.GetComponent<Weapon>().FireRate;
	}

	void Update () {
		if (WasFireButtonPressed() && CanShoot()) {
			UpdateNextFireTime();
			Shoot();
		}
		HandleWeaponChange();
	}

	private void Shoot()
	{
		Vector3 destination = GetBulletDestination();
		SelectedWeapon.GetComponent<Weapon>().Shoot(GunEnd.position, destination);
	}

	private void HandleWeaponChange()
	{
		float mouseScrollChange = Input.GetAxisRaw("Mouse ScrollWheel");
		if (Math.Abs(mouseScrollChange) > 0)
		{
			GameOverlord.processMessage(OverlordMessage.CHANGE_WEAPON, mouseScrollChange);
			SelectedWeapon = GameOverlord.SelectedPlayerWeapon;
			FireRate = SelectedWeapon.GetComponent<Weapon>().FireRate;  
		}
	}

	private Vector3 GetBulletDestination()
	{
		Ray ray = new Ray(Camera.position, Camera.forward);
		RaycastHit hit;
		bool wasHit = Physics.Raycast(ray, out hit);
		Vector3 destination;
		if (wasHit) {
			destination = hit.point;
            Debug.Log(hit.transform.name);
		} else {
			destination = ray.GetPoint(100);
		}
		return destination;
	}
    

	private bool CanShoot()
	{
		return Time.time > nextFireTime;
	}

	private bool WasFireButtonPressed()
	{
		return Input.GetButtonDown ("Fire1");
	}

	private void UpdateNextFireTime()
	{
		nextFireTime = Time.time + FireRate;
	}
    
}

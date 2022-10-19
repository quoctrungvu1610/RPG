﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5;
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player"){
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }
        void Update()
        {
            RotateWeapon();
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            //transform.GetChild(0).gameObject.SetActive(shouldShow);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        private void RotateWeapon()
        {
            Vector3 rotateAngle = new Vector3(0, 6, 0);
            transform.Rotate(rotateAngle);
        }
    }
}


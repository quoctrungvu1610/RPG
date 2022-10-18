using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player"){
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }
        void Update()
        {
            RotateWeapon();
        }

        private void RotateWeapon()
        {
            Vector3 rotateAngle = new Vector3(0, 6, 0);
            transform.Rotate(rotateAngle);
        }
    }
}


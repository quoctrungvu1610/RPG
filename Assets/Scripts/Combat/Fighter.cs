using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        //[SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        //[SerializeField] float weaponDamage = 5f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        //112
        Weapon currentWeapon = null;
      
        private void Start()
        {
            EquipWeapon(defaultWeapon);
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            // Neu target == null thi khong lam gi
            if (target == null) return;
            // Neu target IsDead() thi khong lam gi
            if (target.IsDead()) return;
            //Neu target khac null va khong o trong tam ngam
            if (target != null && !GetIsInRange())
            {
                //Dau tien di den target
                GetComponent<Mover>().MoveTo(target.transform.position, 0.8f);
            }
            else
            {
                //Sau khi di den target roi thi dung lai sau do bat dau AttackBehaviour()
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }
        // Ham GetInRange() tra ve true khi player o trong tam ngam cua enemy
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
        }

        private void AttackBehaviour()
        {
            //Khi tan cong target thi nhin vao target
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0;

            }

        }    //Animation Event
        //Ham Trigger Attack set Animations
        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        public void Hit()
        {
            if (target == null) { return; }
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LanuchProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.GetWeaponDamage());
            }
        }

        public void Shoot()
        {
            Hit();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;

            //Newwwwwwww
            GetComponent<Mover>().Cancel();
        }
        //Ham stop attack set Animation
        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
        //CanAttack nhan vao 1 combatTarget
        //Neu khong co combat target tra ve false
        //Gan targetToTest vao combatTarget sau do lay Health Component
        //Neu co targetToTest va targetToTest !isDead() thi tra ve true

        // Ham CanAttack tra ve true neu co target va target !isDead()
        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        //Ham Attack nhan vao 1 combatTarget
        //GetComponent ActionScheduler cua doi tuong va sau do goi ham StartAction va dua vao action hien tai
        //Gan target = combatTarget.GetComponent<Health>();
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        //Weapons Part
        public void EquipWeapon(Weapon weapon)
        {
            //if(weapon == null) return;
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

    }
    
}


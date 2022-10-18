
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        //110
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        //111
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] bool isRightHanded = true;

        //115
        [SerializeField] Projectile projectile = null;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if(equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                Instantiate(equippedPrefab, handTransform);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        // Shoot Projectile Part
        public void LanuchProjectile(Transform rightHand, Transform lefthand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, lefthand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
        }
        
        public bool HasProjectile()
        {
            return projectile != null;
        }

        

        public float GetWeaponRange()
        {
            return weaponRange;
        }
        public float GetWeaponDamage()
        {
            return weaponDamage;
        }
    }
}
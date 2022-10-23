﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour,ISaveable
    {
        //145
        [SerializeField] float regenerationPercentage = 70;

        float healthPoints = -1f;
        bool isDead = false;
        
        private void Start()
        {
            //145 Level up event
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;


            //141 fixed Health Bug

            if (healthPoints < 0)
            {
                //135 fixed
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
            //135 fixed
            //healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);

        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }
        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }


        public bool IsDead()
        {
            return isDead;
        }

        //133 fix
        public void TakeDamage(GameObject instigator, float damage)
        {
            //146
            print(gameObject.name + "took damage: " + damage);

            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);
            if(healthPoints == 0)
            {
                Die();
                //Experience part
                AwardExperience(instigator);
            }
        }

        //146
        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
      

        public float GetPercentage()
        {
            //135 fixed
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        //Experience part
        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            //135
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
           
        }

        //145 Level up event
        private void RegenerateHealth()
        {
            float regenHealPoints  = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints = Mathf.Max(healthPoints,regenHealPoints);
        }


        public object CaptureState()
        {
            return healthPoints;
        }
        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints == 0)
            {
                Die();
            }
        }
    }
}


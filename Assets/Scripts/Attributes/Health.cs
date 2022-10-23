﻿using System.Collections;
using System.Collections.Generic;
using GameDevTV.Utils;
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

        //float healthPoints = -1f;
        bool isDead = false;

        LazyValue<float> healthPoints;

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }



        private void Start()
        {
            //145 Level up event
            //GetComponent<BaseStats>().onLevelUp += RegenerateHealth;


            //141 fixed Health Bug

            //if (healthPoints < 0)
            //{
            //    //135 fixed
            //    healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            //}
            //135 fixed
            //healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            healthPoints.ForceInit();
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

            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            if(healthPoints.value == 0)
            {
                Die();
                //Experience part
                AwardExperience(instigator);
            }
        }

        //146
        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
      

        public float GetPercentage()
        {
            //135 fixed
            return 100 * (healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health));
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
            healthPoints.value = Mathf.Max(healthPoints.value,regenHealPoints);
        }


        public object CaptureState()
        {
            return healthPoints.value ;
        }
        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;
            if (healthPoints.value <= 0)
            {
                Die();
            }
        }
    }
}


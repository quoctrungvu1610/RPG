using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour,ISaveable
    {
        [SerializeField] float experiencePoints = 0;

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
        }

        public void RestoreState(object state)
        {
           experiencePoints = (float)state;
        }
        //137 EXP
        public float GetPoints()
        {
            return experiencePoints;
        }
    }
}


using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        public float GetStat(Stat stat,CharacterClass characterClass, int level)
        {
            //OLD Method
            //foreach (ProgressionCharacterClass progressionClass in characterClasses)
            //{
            //    if(progressionClass.characterClass == characterClass)
            //    {
            //        //return progressionClass.health[level - 1];
            //    }
            //}
            //return 0;

            //New Method Finding a Stat by enum
            foreach(ProgressionCharacterClass progressionClass in characterClasses)
            {
                if (progressionClass.characterClass != characterClass) continue;

                foreach(ProgressionStat progressionStat in progressionClass.stats)
                {
                    if(progressionStat.stat != stat) continue;
                    if (progressionStat.levels.Length < level) continue;
                    return progressionStat.levels[level - 1];
                }
            }
            return 0;
        }
        //Phai them dong nay moi co the su dung duoc dong tren
        [System.Serializable]
        class ProgressionCharacterClass
        {
            //phai de all variable public (SerializeField OK nhung phai co System.Serializable);
            //Xem trong ScriptableObject
            public CharacterClass characterClass;
            //public float[] health;
            public ProgressionStat[] stats; 

        }
        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}
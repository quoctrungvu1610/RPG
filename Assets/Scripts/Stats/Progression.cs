using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if(progressionClass.characterClass == characterClass)
                {
                    return progressionClass.health[level - 1];
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
            public float[] health;
        }
    }
}
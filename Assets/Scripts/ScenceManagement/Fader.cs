using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
      
        public IEnumerator FadeOut(float time)
        {
            //Update Alpha every frame
            while (canvasGroup.alpha < 1)//alpha is not 1
            {
                //moving alpha torward 1
                //equation in phone
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }
        public IEnumerator FadeIn(float time)
        {
            //Update Alpha every frame
            while (canvasGroup.alpha > 0)//alpha is not 1
            {
                //moving alpha torward 1
                //equation in phone
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}


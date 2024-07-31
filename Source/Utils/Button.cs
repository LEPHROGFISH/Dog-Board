using System.Collections;
using UnityEngine;

namespace DogBoard.Utils
{
    public class Button : GorillaPressableButton
    {
        private static bool Pressed = false;

        
        private void OnTriggerEnter(Collider other)
        {
            if (!Pressed)
            {
                StartCoroutine(Press());
            }
        }


        /*
        
        Can't get it to work but ima keep it just incase

        public override void ButtonActivationWithHand(bool isLeftHand)
        {
            base.ButtonActivationWithHand(isLeftHand);
            StartCoroutine(Press());
        }
        */

        private IEnumerator Press()
        {
            Pressed = true;
            GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(211, true, 0.12f);
            Plugin.instance.PressEvent();
            yield return(object) new WaitForSeconds(0.25f);
            Pressed = false;
        }



    }
}




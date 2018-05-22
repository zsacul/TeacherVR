using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
    public class ClawController : MonoBehaviour
    {

        public ClawControl cntrl;
        public Animator head;
        public LeverTempControll LeverX;
        public LeverTempControll LeverY;
        public LeverTempControll LeverZ;

	void Update()
        {
            cntrl.moveX = LeverX.val/10;
            cntrl.moveY = LeverY.val/10;
            cntrl.moveClaw = LeverZ.val / 15;
        }
    }
}



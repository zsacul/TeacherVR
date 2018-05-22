using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VRTK.Examples
{
    public class LeverControll : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void HandleChange(object sender, Control3DEventArgs e)
        {
            Debug.Log(100 - e.normalizedValue);
        }
    }
}


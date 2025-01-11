using System;
using MapObjects;
using NonDestroyObject;
using UnityEngine;

namespace MapReset
{
    public class ResetCollisionObject : ResetableObject
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.layer == 9)
            {
                gameObject.SetActive(false);
                col.gameObject.SetActive(false);
            }
            if (col.gameObject.layer != 6) return;
            MapResetManager.Instance.ResetMap();
            SoundManager.Instance.GenerateAudioSourceAndPlay( transform, AudioClipEnum.ResetPortal);
        }
    }
}
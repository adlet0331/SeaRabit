using MapObjects;
using UnityEngine;

namespace MapReset
{
    public class ControllMapReset : MonoBehaviour
    {
        [SerializeField] private ResetableObject[] MapReset;

        public void ResetMap()
        {
            foreach (var mapobject in MapReset)
            {
                mapobject.ResetStatus();
            }
        }
    }
}
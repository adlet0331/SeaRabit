using MapReset;
using UnityEngine;

namespace NonDestroyObject
{
    public class MapResetManager : Singleton<MapResetManager>
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
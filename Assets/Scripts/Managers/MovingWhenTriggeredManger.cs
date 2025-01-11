using MapObjects;
using UnityEngine;

namespace NonDestroyObject
{
    public class MovingWhenTriggeredManger : Singleton<MovingWhenTriggeredManger>
    {
        [SerializeField] private MovingObjectWhenTriggered[] MovingObjectWhenTriggereds;

        public void ObjectTriggered(int index)
        {
            MovingObjectWhenTriggereds[index].MoveTriggered();
        }
    }
}
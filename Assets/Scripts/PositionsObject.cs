using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PositionsObject", menuName = "PositionsObject")]
public class PositionsObject : ScriptableObject
{
    public List<PointInTime> pointsInTime;

    public void Record(Vector3 position, Quaternion rotation)
    {
        pointsInTime.Insert(0, new PointInTime(position, rotation));
    }


    [System.Serializable]
    public struct PointInTime
    {
        public Vector3 position;
        public Quaternion rotation;

        public PointInTime(Vector3 _position, Quaternion _rotation)
        {
            position = _position;
            rotation = _rotation;
        }
    }

    public void ForceSerialization()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
    #endif
    }
}



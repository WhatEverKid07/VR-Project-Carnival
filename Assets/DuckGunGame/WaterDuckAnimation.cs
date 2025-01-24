// File: MoveObjectsInList.cs
using UnityEngine;
using System.Collections.Generic;

public class WaterDuckAnimation : MonoBehaviour
{
    [System.Serializable]
    public class MovingObject
    {
        [Tooltip("The GameObject to move.")]
        public GameObject gameObject;

        [Tooltip("The speed of the object.")]
        public float speed = 2f;

        [Tooltip("The distance the object will move.")]
        public float distance = 5f;

        [Tooltip("The initial direction of movement. Use 1 for right, -1 for left.")]
        public int direction = 1; // 1 = right, -1 = left
    }

    [Header("Objects to Move")]
    [Tooltip("List of objects to move with their individual settings.")]
    public List<MovingObject> objectsToMove = new List<MovingObject>();

    private Dictionary<GameObject, Vector3> startPositions = new Dictionary<GameObject, Vector3>();

    void Start()
    {
        // Store the starting positions of all objects
        foreach (var obj in objectsToMove)
        {
            if (obj.gameObject != null)
            {
                startPositions[obj.gameObject] = obj.gameObject.transform.position;
            }
        }
    }

    void Update()
    {
        // Move each object in the list
        foreach (var obj in objectsToMove)
        {
            if (obj.gameObject != null)
            {
                // Calculate movement
                float movement = Mathf.PingPong(Time.time * obj.speed, obj.distance);
                float offset = obj.direction == 1 ? movement : obj.distance - movement;

                // Apply movement
                obj.gameObject.transform.position = startPositions[obj.gameObject] + Vector3.right * offset;
            }
        }
    }
}

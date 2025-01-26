using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsLink : MonoBehaviour
{
    public PointsManager pointsManager;

    public void AddPointsLink()
    {
        pointsManager.AddPoints();
    }
    public void RemovePointsLink()
    {
        pointsManager.RemovePoints();
    }
}

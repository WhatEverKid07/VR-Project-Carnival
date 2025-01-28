using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsLink : MonoBehaviour
{
    public PointsManager pointsManager;
    public NewDuckScript newDuckScript;
    //[SerializeField] private bool isKnocked;

    public void AddPointsLink()
    {
        pointsManager.AddPoints();
    }
    public void RemovePointsLink()
    {
        pointsManager.RemovePoints();
    }

    public void DuckDie()
    {
        StartCoroutine(DuckDie2());
        newDuckScript.RemoveDuck(this.gameObject);
        //newDuckScript.AddNewDuckToKeepUpdated();
    }

    public IEnumerator DuckDie2()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("DuckDie");

    }
}

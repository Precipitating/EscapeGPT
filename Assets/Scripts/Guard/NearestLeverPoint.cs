using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NearestLeverPoint : MonoBehaviour
{
    [SerializeField] private List<Transform> leverPoints;
    [SerializeField] private Transform guardPos;
    [SerializeField] private Transform characterPos;

    private Vector3 leverPos;



    private void Start()
    {
        if (leverPoints == null)
        {
            Debug.LogError("Assign lever points in NearestLeverPoint script (guard)");
        }
    }





    public Vector3 GetNearestLeverPoint()
    {
        Vector3 answer = Vector3.zero;
        float currentDist = 0f;

        // get nearest converse point from character -> converse point
        foreach (var t in leverPoints)
        {
            // set the first position so we have something to compare (first in list)
            if (answer == Vector3.zero)
            {
                currentDist = Vector3.Distance(t.position, characterPos.position);
                answer = t.position;
                continue;
            }

            if (Vector3.Distance(t.position, characterPos.position) < currentDist)
            {
                currentDist = Vector3.Distance(t.position, characterPos.position);
                answer = t.position;
            }


        }


        return answer;
    }



}

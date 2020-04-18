using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardTest : MonoBehaviour
{
    [SerializeField]
    private string username;
    private LeaderBoardHandler testable;
    private int i = 0;
    [SerializeField]
    private int limit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (limit == i) {
            testable = GetComponent<LeaderBoardHandler>();
            testable.StopTime();
            Debug.Log(testable.UploadTime_and_get_place(username));
            i++;
        }
        else {
            i++;
        }
    }
}

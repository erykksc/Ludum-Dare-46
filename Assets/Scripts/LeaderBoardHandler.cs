﻿/*
LeaderBoardHandler:
    - Co robi:
        - Na początku rozpoczyna odliczanie czasu
        - this.StopTime() zatrzymuje odliczanie czasu
        - uploaduje za pomocą RestSharp czas do tablicy wyników
        i zwraca miejsce w rankingu (UploadTime_and_get_place(username))
    - Na czym powinien być:
        - tak
    - specjalne ustawienia obiektu:
        - LevelName: nazwa poziomu

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using RestSharp;

public class LeaderBoardHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string LevelName;
    private System.DateTime startTime;
    private System.DateTime stopTime;
    private RestClient client = new RestClient("http://grzegorzkoperwas.site:5000");

    // Starts timer on class load
    void Start()
    {
        startTime = System.DateTime.Now;
    }

    // Stops the timer
    public void StopTime() {
        stopTime = System.DateTime.Now;
    }

    // returns timer as ISO string
    public string GetFinalTimeISO() {
        var diff = stopTime.Subtract(startTime);
        return diff.ToString(@"hh\:mm\:ss");
    }

    // Uploads time to server under (username) and returns place in ranking
    public int UploadTime_and_get_place(string username = "testUsername") {
        var request = new RestRequest("/api/postTime", Method.POST);
        request.AddParameter("nick", username);
        request.AddParameter("time", GetFinalTimeISO());
        request.AddParameter("level", LevelName);
        try
        {
            var response = client.Execute<PostResponse>(request);
            if (response.StatusCode == HttpStatusCode.OK) {
               return response.Data.place; 
            }
            else {
                return -1;
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    // class for RestRequest returns
    public class PostResponse {
        public bool success { get; set; }
        public int place { get; set; }
    }
}
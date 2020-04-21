/*
LeaderBoardHandler:
    - Co robi:
        - this.StartTimer() rozpoczyna odliczanie czasu
        - this.GetCurrentTime() zwraca string mm:ss,f
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
using UnityEngine.Networking;
using System.Net;
using System.Text;

public class LeaderBoardHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public string LevelName;
    private System.DateTime startTime;
    private System.DateTime stopTime;
    [SerializeField]
    private bool debug_server;
    private string url;




    public void Awake()
    {
        if (debug_server) {
            url = "http://localhost:5000";
            Debug.LogWarning("Running leaderboard in debug mode");
        }
        else {
            url = "http://grzegorzkoperwas.site:5000";
        }
        StartTimer();
    }




    // Starts timer on class load
    public void StartTimer()
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
    public string GetCurrentTime() {
        var currentTime = System.DateTime.Now;
        var diff = currentTime.Subtract(startTime);
        return diff.ToString(@"mm\:ss\,f");
    }

    // Uploads time to server under (username) and returns place in ranking
    public int UploadTime_and_get_place(string username = "testUsername") {
        var args = new List<IMultipartFormSection>();
        args.Add(new MultipartFormDataSection("nick", Encoding.UTF8.GetBytes(username)));
        args.Add(new MultipartFormDataSection("time", Encoding.UTF8.GetBytes(GetFinalTimeISO())));
        args.Add(new MultipartFormDataSection("level", Encoding.UTF8.GetBytes(LevelName)));
        var addres = url + "/api/postTime";
        Debug.Log("Connecting…");
        var response = UnityWebRequest.Post(addres, args);
        response.SendWebRequest();
        while (!response.isDone)
        {
            
        }
        if (response.isNetworkError || response.isHttpError) {
            Debug.LogWarning("http or network error");
            return -1;
        }
        else {
            try
            {
                var val = response.downloadHandler.text;
                return int.Parse(val);
            }
            catch (System.Exception e)
            {
                Debug.Log("Failed decoding response");
                Debug.Log(e);
                return -1;
            }
        }
    }
}

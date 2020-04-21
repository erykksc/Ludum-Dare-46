using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreHandler : MonoBehaviour
{
    private LeaderBoardHandler LBH;
    [SerializeField] private Text target;
    [SerializeField] private Text display_place_leaderboard;
    
    

    public void Start()
    {
      LBH = GameObject.FindGameObjectWithTag("Player").GetComponent<LeaderBoardHandler>();
      LBH.StopTime();
        GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<Text>().text = LBH.GetCurrentTime();

        LevelManager.reading_input = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

        public void PlayerNameInputComplete()
    {
        int place = LBH.UploadTime_and_get_place(target.text);
        if(place>0) display_place_leaderboard.text = "Your place on the leaderboard: " + place;
        else { display_place_leaderboard.text = "Error While Uploading"; }

    } 
}

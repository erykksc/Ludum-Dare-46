/*
LeaderboardTest: przykładowa implementacja i test
LeaderBoardHandler
    - Co robi:
        - po upłycięciu (limit) ticków wysyła do tablicy wyników
        wynik pod nickiem (username)
    - Na czym powinien być:
        - tak
    - Jakich komponentów wymaga:
        - LeaderBoardHandler na tym samym GameObject
    - Specjalne ustawienia objektu:
        - username - nick pod którym wysłany będzie wynik
        - limit - opóźnienie czasowe
*/


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
    void FixedUpdate()
    {
        if (limit == i) {
            // Przykładowy call do wysłania wyniku
            testable = GetComponent<LeaderBoardHandler>();
            // Na koniec zatrzymaj czas
            testable.StopTime();
            // poproś gracza o nick, wyślij czas
            Debug.Log(testable.UploadTime_and_get_place(username));
            i++;
        }
        else {
            i++;
        }
    }
}

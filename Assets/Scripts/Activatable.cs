/*
    Activatable Class
        - Co robi:
            - może być aktywowane przez system logiki lub w edytorze
        - Na czym powinien być:
            - na niczym, roszerzać;
        - Specjalne ustawienia
            - is_logic_enabled;
                Czy korzysta z logiki
            - active
                Czy jest aktywne (nadpisywane logiką jeśli jest aktywna)
            - logicGate_input
                wejście logiki jeśli jest ona aktywna
        
    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activatable : MonoBehaviour
{
    public bool active;

    //Override me

    virtual public void activate(){}
    virtual public void de_activate() {}
}

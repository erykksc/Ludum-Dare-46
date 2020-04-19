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
    [SerializeField] public bool Use_logic;
    [SerializeField, Tooltip("Only active when does not use logic")]
    private bool _active;
    public bool active {
        get => _active;
        set {
            if (value != active) {
                if (value) {
                    activate();
                    _active = true;
                }
                else {
                    de_activate();
                    _active = false;
                }
            }
        }
    }
    void Start() {
        if (!Use_logic) {
            if (_active) {
                activate();
            }
            else {
                de_activate();
            }
        }
    }

    //Override me

    virtual public void activate() {}
    virtual public void de_activate() {}
}

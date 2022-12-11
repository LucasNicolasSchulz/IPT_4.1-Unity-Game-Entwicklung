using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonClicks : MonoBehaviour
{
// Eine Variable namens "inputManager" vom Typ "InputManager" wird deklariert.
InputManager inputManager;
private void Awake()
{
// Der inputManager-Variable wird das InputManager-Komponente des GameObjects zugewiesen, dem das Skript angefügt ist.
inputManager = GetComponent<InputManager>();
}


private void FixedUpdate()
{
    // Wenn der "p_Input"-Wert des inputManager-Objekts "true" ist...
    if (inputManager.p_Input)
    {
        // ...wird der Wert von "menu_active" im UI-Komponente des GameObjects "Canvas 1" umgekehrt.
        GameObject.Find("Canvas 1").GetComponent<UI>().menu_active = !GameObject.Find("Canvas 1").GetComponent<UI>().menu_active;
        // Der "p_Input"-Wert des inputManager-Objekts wird auf "false" gesetzt.
        inputManager.p_Input = false;
    }
}
}

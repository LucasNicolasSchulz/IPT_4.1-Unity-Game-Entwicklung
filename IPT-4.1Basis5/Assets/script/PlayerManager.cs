using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
// Eine Variable namens "inputManager" vom Typ "InputManager" wird deklariert.
InputManager inputManager;
// Eine Variable namens "cameraManager" vom Typ "CameraManager" wird deklariert.
CameraManager cameraManager;
// Eine Variable namens "playerLocomotion" vom Typ "PlayerLocomotion" wird deklariert.
PlayerLocomotion playerLocomotion;
// Eine Variable namens "animator" vom Typ "Animator" wird deklariert.
Animator animator;

// Eine Variable namens "isInteracting" vom Typ "bool" wird deklariert und öffentlich gemacht.
public bool isInteracting;

private void Awake()
{
    // Der animator-Variable wird das Animator-Komponente des GameObjects zugewiesen, dem das Skript angefügt ist.
    animator = GetComponent<Animator>();
    // Der inputManager-Variable wird das InputManager-Komponente des GameObjects zugewiesen, dem das Skript angefügt ist.
    inputManager = GetComponent<InputManager>();
    // Der cameraManager-Variable wird das CameraManager-Komponente des ersten gefundenen GameObjects zugewiesen, das im Szenengraph vorhanden ist.
    cameraManager = FindObjectOfType<CameraManager>();
    // Der playerLocomotion-Variable wird das PlayerLocomotion-Komponente des GameObjects zugewiesen, dem das Skript angefügt ist.
    playerLocomotion = GetComponent<PlayerLocomotion>();
}

private void Update()
{
    // Die "HandleAllInputs"-Methode des inputManager-Objekts wird aufgerufen.
    inputManager.HandleAllInputs();
}

private void FixedUpdate()
{
    // Die "HandleAllMovement"-Methode des playerLocomotion-Objekts wird aufgerufen.
    playerLocomotion.HandleAllMovement();
}
// Diese Methode wird aufgerufen, wenn das Update-Ereignis ausgelöst wird
private void LateUpdate()
{
// Die Kamerabewegung wird verarbeitet
cameraManager.HandleAllCameraMovement();
// Der Wert "isInteracting" wird aus dem Animator-Komponente des Spielers gelesen
isInteracting = animator.GetBool("isInteracting");
// Der Wert "isJumping" wird im Animator-Komponente des Spielers gesetzt
playerLocomotion.isJumping = animator.GetBool("isJumping");
// Der Wert "isGrounded" wird im Animator-Komponente des Spielers gesetzt
animator.SetBool("isGrounded", playerLocomotion.isGrounded);
}

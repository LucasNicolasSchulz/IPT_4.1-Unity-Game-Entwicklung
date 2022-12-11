using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Eine Referenz auf die PlayerControls-Klasse, die für die Verarbeitung von Eingaben verwendet wird
    PlayerControls playerControls;

    // Referenzen auf andere Komponenten im GameObject, die für die Verarbeitung von Eingaben benötigt werden
    AnimatorManager animatorManager;
    PlayerLocomotion playerLocomotion;

    // Öffentliche Variablen, die die X- und Y-Eingaben der Kamera speichern
    public float cameraInputX;
    public float cameraInputY;

    // Öffentliche Variablen, die die Eingaben des Spielers für Bewegung und Kamera speichern
    public Vector2 movementInput;
    public Vector2 cameraInput;

    // Öffentliche Variablen, die die Bewegungsmenge des Spielers und die Eingaben für Bewegung in Richtungen entlang der X- und Y-Achsen speichern
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    // Öffentliche Variablen, die Eingaben für Sprints, Sprünge und das Pausenmenü speichern
    public bool shift_Input;
    public bool jump_Input;
    public bool p_Input;

    // Der Awake-Callback wird aufgerufen, bevor das Skript initialisiert wird
    private void Awake()
    {
        // Holt sich eine Referenz auf den AnimatorManager- und PlayerLocomotion-Komponenten im GameObject
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    // Der OnEnable-Callback wird aufgerufen, wenn das Skript aktiviert wird
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            // Zuweisen der ausgeführten und stornierten Ereignisse für die Bewegungs- und Kameraeingaben
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            // Zuweisen der ausgeführten und stornierten Ereignisse für die Sprint- und Sprungaktionen
            playerControls.PlayerActions.Sprint.performed += i => shift_Input = true;
            playerControls.PlayerActions.Sprint.canceled += i => shift_Input = false;
            playerControls.PlayerActions.Jump.performed += i => jump_Input = true;

            // Zuweisen der ausgeführten und stornierten Ereignisse für die Pausemenüaktion
            playerControls.ButtonAction.PauseMenu.performed += i => p_Input = true;
            playerControls.ButtonAction.PauseMenu.canceled += i => p_Input = false;
        }

        // Aktivieren der Spielersteuerungen
        playerControls.Enable();
    }

    private void OnDisable()
    {
        // Deaktivieren der Spielersteuerungen
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        // Verarbeiten der Bewegungs-, Sprint- und Sprungseingaben
        HandleMovementInput();
        SprintingInput();
        HandleJumpingInput();
    }

    private void HandleMovementInput()
    {
        // Zuweisen der horizontalen und vertikalen Eingabewerte aus dem Bewegungseingabevektor
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        // Zuweisen der Kameraeingabewerte aus dem Kameraeingabevektor
        cameraInputX = cameraInput.y;
        cameraInputY = cameraInput.x;

        // Berechnen der Bewegungsmenge und Aktualisieren des Animators
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, shift_Input);
    }
    private void Start()
    {
        // Sperren des Cursors in der Mitte des Bildschirms
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SprintingInput()
    {
        // Überprüfen, ob der Shift-Eingabe wahr ist und setzen Sie den Sprintwert entsprechend
        if (shift_Input)
        {
            playerLocomotion.issprinting = true;
        }
        else
        {
            playerLocomotion.issprinting = false;
        }
    }

    private void HandleJumpingInput()
    {
        // Überprüfen, ob der Sprungeingabe true ist und rufen Sie die HandleJumping-Methode auf, wenn sie es ist
        if (jump_Input)
        {
            jump_Input = false;
            playerLocomotion.HandleJumping();
        }
    }
}

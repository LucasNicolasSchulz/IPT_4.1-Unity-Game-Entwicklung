using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager; // Referenz auf das InputManager-Skript
    UI ui; // Referenz auf das UI-Skript
    public Transform targetTransform; // Transform des Zielobjekts, dem die Kamera folgt
    public Transform cameraPivot; // Transform des Pivots, um den sich die Kamera dreht
    public Transform cameraTransform; // Transform der Hauptkamera
    private float defaultPosition; // Standard-z-Position der Kamera
    private Vector3 cameraFollowVelocity = Vector3.zero; // Geschwindigkeit für die sanfte Dämpfung der Kamerabewegung
    public float cameraCollisionRadius = 0.2f; // Radius der Kugel, die für Kollisionsüberprüfungen verwendet wird
    public float lookAngle; // Aktueller y-Drehwinkel der Kamera
    public float pivotAngle; // Aktueller x-Drehwinkel des Pivots
    public float minPivotAngle = -20; // Minimaler erlaubter x-Drehwinkel des Pivots
    public float maxPivotAngle = 35; // Maximaler erlaubter x-Drehwinkel des Pivots
    public LayerMask collisionLayers; // Schichten, die auf Kollisionen überprüft werden sollen
    public float cameraFollowSpeed = 0.1f; // Geschwindigkeit, mit der die Kamera dem Ziel folgt
    public float cameraLookSpeed = 0.4f; // Geschwindigkeit, mit der die Kamera um das Ziel herumdreht
    public float cameraPivotSpeed = 0.4f; // Geschwindigkeit, mit der die Kamera hoch- und runter schwenkt
    public float cameraCollisionOffset = 0.2f; // Abstand zwischen Kamera und Pivot, wenn keine Kollisionen erkannt werden
    public float minCollisionOffset = 0.2f; // Minimaler erlaubter Abstand zwischen Kamera und Pivot bei Kollisionen
    private Vector3 cameraVectorPosition; // Aktuelle Position der Kamera


    private void Awake()
    {
        // Initialisiert Referenzen auf andere Skripte und Objekte
        ui = FindObjectOfType<UI>();
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }
    public void HandleAllCameraMovement()
    {
        FollowAstronout();
        RotateCamera();
        HandleCameraCollisions();
    }
    private void FollowAstronout()
    {
        // Berechnet die Zielposition der Kamera anhand des Zielobjekts und der Geschwindigkeit der Kamera
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        // Setzt die Position der Kamera auf die berechnete Zielposition
        transform.position = targetPosition;
    }
    private void RotateCamera()
    {
        // Überprüft, ob das Menü aktiv ist. Wenn ja, wird die Funktion beendet.
        if (ui.menu_active)
        {
            return;
        }

        // Berechnet die neuen Drehwinkel der Kamera und des Pivots anhand der Eingabe des Spielers
        Vector3 rotation;
        Quaternion targetRotation;
        lookAngle = lookAngle + (inputManager.cameraInputY * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputX * cameraPivotSpeed);

        // Stellt sicher, dass der Drehwinkel des Pivots innerhalb der erlaubten Grenzen bleibt
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

        // Setzt die Rotation des Kameratransforms auf den berechneten Drehwinkel
        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        // Setzt die Rotation des Pivot-Transforms auf den berechneten Drehwinkel
        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    private void HandleCameraCollisions()
    {
        // Berechnet die Zielposition der Kamera anhand des Pivots und der Kollisionsüberprüfungen
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        // Wenn die Kamera eine Kollision mit anderen Objekten auf den angegebenen Schichten erkennt, wird der Abstand zwischen Kamera und Pivot entsprechend angepasst
        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = targetPosition - (distance - cameraCollisionRadius);
        }

        // Stellt sicher, dass der Abstand zwischen Kamera und Pivot innerhalb der erlaubten Grenzen bleibt
        if (Mathf.Abs(targetPosition) < minCollisionOffset)
        {
            targetPosition = targetPosition - minCollisionOffset;
        }

        // Interpoliert die z-Position der Kamera zu ihrer Zielposition, um eine sanfte Bewegung zu erzeugen
        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}

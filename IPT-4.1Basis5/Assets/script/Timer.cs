using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
// Zeit, die noch übrig ist
public float timeRemaining = 10;
// Timer läuft gerade
public bool timerIsRunning = false;
// Textkomponente, die die Zeit anzeigt
public Text timeText;
// Referenz auf UI-Skript
public UI ui;

// Startet den Timer beim Start des Spiels
private void Start()
{
timerIsRunning = true;
if (SceneManager.GetActiveScene().name != "IntroScene")
{
// Lädt gespeicherte Zeit aus dem PlayerPrefs
timeRemaining = PlayerPrefs.GetFloat("Timer");
}
}

// Aktualisiert die Zeit jeden Frame
void Update()
{
if (timerIsRunning)
{
if (timeRemaining > 0)
{
// Zeit wird pro Frame um deltaTime reduziert
timeRemaining -= Time.deltaTime;
DisplayTime(timeRemaining);
}
else
{
Debug.Log("Zeit ist abgelaufen!");
timeRemaining = 0;
timerIsRunning = false;
}
}
}

// Zeigt die Zeit in der Textkomponente an
void DisplayTime(float timeToDisplay)
{
// Korrigiert die angezeigte Zeit um 1 Sekunde
timeToDisplay += 1;
float minutes = Mathf.FloorToInt(timeToDisplay / 60);
float seconds = Mathf.FloorToInt(timeToDisplay % 60);
timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
}
}

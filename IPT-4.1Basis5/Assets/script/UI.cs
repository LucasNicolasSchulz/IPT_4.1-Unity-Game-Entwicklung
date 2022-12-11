using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
  public int startlife_amount; //Max Leben
  public int current_life; // aktuelles Leben
  public bool menu_active; // Bool für Aktivierung vom Menu
  private GameObject pausemenu; // Pause menu
  private GameObject healthbar; // Lebensanzeige
  void Start()
  {
    GameObject.Find("BossBar").SetActive(SceneManager.GetActiveScene().name == "Bossfight"); // Wenn die aktuelle Szene der Bosskampf ist, aktiviert sich der Bossbar
    pausemenu = GameObject.Find("pause"); // Sucht das Pausemenu GameObject
    healthbar = GameObject.Find("player_life"); // Sucht das Lebensanzeige GameObject
    healthbar.GetComponent<Slider>().maxValue = startlife_amount; // Setzt den max. Wert von der Lebensanzeige auf das Max Leben
  }

  void FixedUpdate()
  {
    healthbar.GetComponent<Slider>().value = current_life; //Wert der Lebensanzeige
    current_life = Mathf.Clamp(current_life, 0, startlife_amount); // aktuelles leben zwischen 0 und max leben
    pausemenu.SetActive(menu_active ? true : false); //Ändert sich, damit Menu und Leben nicht gleichzeitig erscheinen
    healthbar.SetActive(menu_active ? false : true);

    Cursor.lockState = menu_active ? CursorLockMode.None : CursorLockMode.Locked; //Lockt den Cursor
  }

  public void getDamage(int damage)
  {
    /*
     * Leben wird abgezogen und falls Leben unter oder gleich 0 ist, dann führt es die Methode 'death()' aus
     */
    current_life -= damage;
    if (current_life <= 0)
      death();
  }

  public void death()
  {
    /*
     * Falls Leben unter oder gleich 0 ist
     */
    GameObject.Find("Player").GetComponent<serialize>().delete_file();
    PlayerPrefs.SetFloat("Timer", 300);
    GameObject.Find("SceneManager").GetComponent<SceneChange>().is_dead = true;
    GameObject.Find("SceneManager").GetComponent<SceneChange>().NextScene_activate = true;
  }

  public void menu()
  {
    /*
     * Menu wird deaktiviert (Resume button)
     */
    menu_active = false;
  }

  public void exit()
  {
    /*
     * Schliesst das Spiel
     */
    Application.Quit();
  }

  public void save()
  {
    /*
     *  Funktion für 'Save' button
     */
    GameObject.Find("Player").GetComponent<serialize>().save_data();
  }

  public void reset()
  {
    /*
     *  Funktion für 'Reset' button
     */

    GameObject.Find("Player").GetComponent<serialize>().reset_data();
  }

  public void skincolor(string color)
  {
    /*
     *  Funktion für 'Skincolor' wechsel
     */
    GameObject.Find("Player").GetComponent<skincolor>().Hex2RGB(color);
  }

  public void hat(string hat)
  {
    /*
     *  Funktion für 'hat' wechsel
     */
    GameObject.Find("Player").GetComponent<hat>().setHat(hat);
  }

}

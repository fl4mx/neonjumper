using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
 
public class FPSButtonControl :MonoBehaviour
{
  /*
      SLIDER VALUES FOR REFERENCE
      DIFFICULTY (DELETION TIME) = 0 - 2
      X = 0 - 35
      Z = 0 - 35
      Y = 0 - 6
  */

  private int worldX = PlayerPrefs.GetInt("worldX");            //length
  private int worldY = PlayerPrefs.GetInt("worldY");            //height
  private int worldZ = PlayerPrefs.GetInt("worldZ");            //width
  private float difficulty = PlayerPrefs.GetFloat("difficulty");  //difficulty

  private int spaz = PlayerPrefs.GetInt("spaz");
  private int legacy = PlayerPrefs.GetInt("legacy");
  private int music = PlayerPrefs.GetInt("music");

  public GameObject Htext;
  public GameObject Wtext;
  public GameObject Ltext;
  public GameObject Dtext;

  public GameObject spazcheck;
  public GameObject legacycheck;
  public GameObject musiccheck;

  public GameObject playpausetext;

  public GameObject pausecontrollergameobject;

  void Start()
  {
    // Fix button cross/tick reset on new scene load
    int spaz = PlayerPrefs.GetInt("spaz");
    int legacy = PlayerPrefs.GetInt("legacy");
    int music = PlayerPrefs.GetInt("music");
  }


  // Function triggered when the mouse cursor is over the GameObject on which this script runs, for button functionality
  void OnMouseOver()
  {
    // If the left mouse button is pressed
    if (Input.GetMouseButtonDown(0))
    {
      switch (this.tag)
      {
      // Main Menu
      case "LevelSelect":
        SceneManager.LoadScene("levelSelect_new");
        break;
      case "temp":
        SceneManager.LoadScene("gameLevel");
        break;
      case "instruction":
        SceneManager.LoadScene("instructions");
        break;
      case "Exit":
        // Save data here
        #if UNITY_EDITOR
          // Application.Quit() does not work in the editor so
          // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
          UnityEditor.EditorApplication.isPlaying = false;
        #else
          Application.Quit();
        #endif
      break;




      //levelselect buttons
      case "back":
        SceneManager.LoadScene("mainMenu_new");
        break;
      case "easy":
        SceneManager.LoadScene("gameLevel");
        PlayerPrefs.SetFloat("deleteTime", 0.07F); //stores deleteTime in a playerPrefs key of "deleteTime" which can then be called in WorldGenerator script
        PlayerPrefs.SetInt("worldX", 20); //similarly dimensions
        PlayerPrefs.SetInt("worldZ", 20);
        PlayerPrefs.SetInt("worldY", 5);
        break;
      case "medium":
        SceneManager.LoadScene("gameLevel");
        PlayerPrefs.SetFloat("deleteTime", 0.035F);
        PlayerPrefs.SetInt("worldX", 20);
        PlayerPrefs.SetInt("worldZ", 20);
        PlayerPrefs.SetInt("worldY", 5);
        break;
      case "hard":
        SceneManager.LoadScene("gameLevel");
        PlayerPrefs.SetFloat("deleteTime", 0.015F);
        PlayerPrefs.SetInt("worldX", 15);
        PlayerPrefs.SetInt("worldZ", 15);
        PlayerPrefs.SetInt("worldY", 6);        
        break;
      case "custom":
        PlayerPrefs.SetInt("worldX", 15);
        PlayerPrefs.SetInt("worldY", 3);
        PlayerPrefs.SetInt("worldZ", 15);
        PlayerPrefs.SetFloat("difficulty", 1f);
        PlayerPrefs.SetFloat("deleteTime", 0.1f);
        SceneManager.LoadScene("customLevel_new");
        break;


      // Instruction/option buttons
      case "spazmode":
        int spaz = PlayerPrefs.GetInt("spaz");
        if (spaz == 1){
          PlayerPrefs.SetInt("spaz", 0);
          spazcheck.GetComponent<TextMesh>().text = "✗";
        }
        else{
          PlayerPrefs.SetInt("spaz", 1);
          spazcheck.GetComponent<TextMesh>().text = "✓";
        }
        break;
      case "legacymode":
        int legacy = PlayerPrefs.GetInt("legacy");
        if (legacy == 1){
          PlayerPrefs.SetInt("legacy", 0);
          legacycheck.GetComponent<TextMesh>().text = "✗";  
        }
        else{
          PlayerPrefs.SetInt("legacy", 1);
          legacycheck.GetComponent<TextMesh>().text = "✓";
        }
        break;
      case "music":
        int music = PlayerPrefs.GetInt("music");
        if (music == 1){
          PlayerPrefs.SetInt("music", 0);
          musiccheck.GetComponent<TextMesh>().text = "✗";
        }
        else{
          PlayerPrefs.SetInt("music", 1);
          musiccheck.GetComponent<TextMesh>().text = "✓";
        }
        break;



      // Custom level buttons
      case "lengthadd":
        worldX = PlayerPrefs.GetInt("worldX");
        if(worldX < 30) //30 instead of 35 for optimisation purposes
        {
          worldX += 1;
        }
        Ltext.GetComponent<TextMesh>().text = worldX.ToString();
        PlayerPrefs.SetInt("worldX", worldX);
        break;
      case "lengthminus":
        worldX = PlayerPrefs.GetInt("worldX");
        if(worldX > 3)
        {
          worldX -= 1;
        }
        Ltext.GetComponent<TextMesh>().text = worldX.ToString();
        PlayerPrefs.SetInt("worldX", worldX);
        break;
      
      case "widthadd":
        worldZ = PlayerPrefs.GetInt("worldZ");
        if(worldZ < 30) //likewise, 30 instead of 35 for optimisation purposes
        {
          worldZ += 1;
        }
        Wtext.GetComponent<TextMesh>().text = worldZ.ToString();
        PlayerPrefs.SetInt("worldZ", worldZ);
        break;
      case "widthminus":
        worldZ = PlayerPrefs.GetInt("worldZ");
        if(worldZ > 3)
        {
          worldZ -= 1;
        }
        Wtext.GetComponent<TextMesh>().text = worldZ.ToString();
        PlayerPrefs.SetInt("worldZ", worldZ);
        break;
      
      case "heightadd":
        worldY = PlayerPrefs.GetInt("worldY");
        if(worldY < 6) //6 instead of 35 for optimisation purposes
        {
          worldY += 1;
        }
        Htext.GetComponent<TextMesh>().text = worldY.ToString();
        PlayerPrefs.SetInt("worldY", worldY);
        break;
      case "heightminus":
        worldY = PlayerPrefs.GetInt("worldY");
        if(worldY > 1)
        {
          worldY -= 1;
        }
        Htext.GetComponent<TextMesh>().text = worldY.ToString();
        PlayerPrefs.SetInt("worldY", worldY);
        break;

      case "diffadd":
        difficulty = PlayerPrefs.GetFloat("difficulty");
        if(difficulty < 2)
        {
          difficulty += 0.2f;
        }
        Dtext.GetComponent<TextMesh>().text = difficulty.ToString();
        PlayerPrefs.SetFloat("difficulty", difficulty);
        PlayerPrefs.SetFloat("deleteTime", Mathf.Pow(10.0f, (-1*difficulty)));
        break;
      case "diffminus":
        difficulty = PlayerPrefs.GetFloat("difficulty");
        if(difficulty > 0.2f)
        {
          difficulty -= 0.2f;
        }
        Dtext.GetComponent<TextMesh>().text = difficulty.ToString();
        PlayerPrefs.SetFloat("difficulty", difficulty);
        PlayerPrefs.SetFloat("deleteTime", Mathf.Pow(10.0f, (-1*difficulty)));
        break;
      
      case "customgamelevel":
        SceneManager.LoadScene("gameLevel");
        break;

      

      //level resume buttons
      case "playpause":
        //grab functionality from pausecontroller class
        pausecontroller pcplaypause = pausecontrollergameobject.GetComponent<pausecontroller>();
        pcplaypause.gameIsPaused = !pcplaypause.gameIsPaused;
        pcplaypause.PauseGame();
        //switch aroud text of pause and resume button
        if (pcplaypause.gameIsPaused == false)
        {
          Debug.Log("change to pause");
          playpausetext.GetComponent<TextMesh>().text = "Pause";
        }
        else
        {
          Debug.Log("change to resume");
          playpausetext.GetComponent<TextMesh>().text = "Resume";
        }
        Debug.Log("RESUME GAME");
        break;
      case "levelback":
        //also functionality from pausecontroller class
        pausecontroller pclevelback = pausecontrollergameobject.GetComponent<pausecontroller>();
        pclevelback.gameIsPaused = true;
        pclevelback.PauseGame();
        //back button
        SceneManager.LoadScene("mainMenu_new");
        break;
      }
    }
  }
}

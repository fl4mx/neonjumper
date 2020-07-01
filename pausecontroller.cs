using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausecontroller : MonoBehaviour
{
    public Material arena;
    public Material cube;
    private Color colorActive = new Vector4 (0.28f, 0.36f, 0.7f, 6f);
    private Color colorPause = new Vector4 (0.3f, 0.4f, 0.2f, 0.2f);
    private Color blockActive = new Vector4 (0.8f, 0.6f, 0.2f, 10f);
    private Color blockPause = new Vector4 (0.3f, 0.3f, 0.3f, 0.4f);
    public bool gameIsPaused;


    public GameObject backbutton;
    public GameObject pausebutton;
    public GameObject pausetext;


    void Start(){
    	arena.SetColor("_GridColor", colorActive);
    	cube.SetColor("_BlockColor", blockActive);
        backbutton.SetActive(false);
        pausebutton.SetActive(false);
        pausetext.SetActive(false);
        gameIsPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log(gameIsPaused);
            gameIsPaused = !gameIsPaused;
            Debug.Log(gameIsPaused);
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if(gameIsPaused == false)
        {
            Time.timeScale = 0f;
            backbutton.SetActive(true);
            pausebutton.SetActive(true);
            pausetext.SetActive(true);
            arena.SetColor("_GridColor", colorPause);
            cube.SetColor("_BlockColor", blockPause);
            Debug.Log("pause exec");
        }
        else if(gameIsPaused == true)
        {
            Time.timeScale = 1f;
            arena.SetColor("_GridColor", colorActive);
            cube.SetColor("_BlockColor", blockActive);
            backbutton.SetActive(false);
            pausebutton.SetActive(false);
            pausetext.SetActive(false);
            Debug.Log("resume exec");
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
 
public class deathscreen : MonoBehaviour
{
	public GameObject timesurvivedtext;
	public GameObject blocksplacedtext;
  public GameObject currentscoretext;
  public GameObject highscoretext;

  private float currentscore;
  private float highscore;
  private int worldX;
  private int worldY;
  private int worldZ;
  private float surviveTime;
  private float blocksplaced;
  private float deleteTime;

	private void Start()
  	{
  	  worldX = PlayerPrefs.GetInt("worldX");
      worldY = PlayerPrefs.GetInt("worldY");
      worldZ = PlayerPrefs.GetInt("worldZ");
      surviveTime = PlayerPrefs.GetFloat("surviveTime");
      blocksplaced = PlayerPrefs.GetFloat("blocksplaced");
      deleteTime = PlayerPrefs.GetFloat("deleteTime");
      highscore = PlayerPrefs.GetFloat("highscore");
      //Debug.Log(worldX);
      //Debug.Log(worldY);
      //Debug.Log(worldZ);
      //Debug.Log(surviveTime);
      //Debug.Log(blocksplaced);
      //Debug.Log(deleteTime);
      //Debug.Log(PlayerPrefs.GetString("surviveTime"));
	    currentscore = ((surviveTime * (1/deleteTime))) + (blocksplaced / 2) - (((float)worldX + (float)worldY + (float)worldZ)/3);
      Debug.Log(currentscore);
      timesurvivedtext.GetComponent<TextMesh>().text = PlayerPrefs.GetFloat("surviveTime").ToString();
	    blocksplacedtext.GetComponent<TextMesh>().text = PlayerPrefs.GetFloat("blocksplaced").ToString();
      currentscoretext.GetComponent<TextMesh>().text = currentscore.ToString();
      if(currentscore > highscore)
      {
        highscore = currentscore;
        PlayerPrefs.SetFloat("highscore", highscore);
      }
      highscoretext.GetComponent<TextMesh>().text = highscore.ToString();
  	}
  /*
      SLIDER VALUES FOR REFERENCE
      DIFFICULTY (DELETION TIME) = 0 - 2
      X = 0 - 35
      Z = 0 - 35
      Y = 0 - 6
  */
}
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
      //get stats from playerprefs
  	  worldX = PlayerPrefs.GetInt("worldX");
      worldY = PlayerPrefs.GetInt("worldY");
      worldZ = PlayerPrefs.GetInt("worldZ");
      surviveTime = PlayerPrefs.GetFloat("surviveTime");
      blocksplaced = PlayerPrefs.GetFloat("blocksplaced");
      deleteTime = PlayerPrefs.GetFloat("deleteTime");
      highscore = PlayerPrefs.GetFloat("highscore");

      //score calculation algorithm
	    currentscore = ((surviveTime * (1/deleteTime))) + (blocksplaced / 2) - (((float)worldX + (float)worldY + (float)worldZ)/3);
      
      //set text of death stat text to the stats from playerprefs
      timesurvivedtext.GetComponent<TextMesh>().text = PlayerPrefs.GetFloat("surviveTime").ToString();
	    blocksplacedtext.GetComponent<TextMesh>().text = PlayerPrefs.GetFloat("blocksplaced").ToString();
      currentscoretext.GetComponent<TextMesh>().text = currentscore.ToString();
      
      //setting new highscore system
      if(currentscore > highscore)
      {
        highscore = currentscore;
        PlayerPrefs.SetFloat("highscore", highscore);
      }
      //set text of highscore
      highscoretext.GetComponent<TextMesh>().text = highscore.ToString();
  	}
}

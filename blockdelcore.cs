using UnityEngine;
using System.Collections;
 
public class blockdelcore : MonoBehaviour
{
  public Vector3 delta;
 
  void OnMouseOver()
  {
  //LMB down
  if (Input.GetMouseButtonDown(0))
  {
    if (Time.timeScale == 1f){ //to prevent loophole where you can pause game and continue building.
      // Destroy the parent of the face we clicked
      Destroy(this.transform.parent.gameObject);
    }
  }
 
  //RMB down
  if (Input.GetMouseButtonDown(1))
  {
    //Added code to fix block glitches (building into player and then getting stuck)
    if (Time.timeScale == 1f){
      Vector3 newPosition = this.transform.parent.transform.position + delta;
      

      /*GameObject FPSController = (GameObject)GameObject.Find("FPSController");
      float playerX = Mathf.Abs(FPSController.transform.position.x);
      float playerY = Mathf.Abs(FPSController.transform.position.y);
      float playerZ = Mathf.Abs(FPSController.transform.position.z);*/

      //if((Mathf.Abs(newPosition.x) < (playerX - 0.45)) && (Mathf.Abs(newPosition.x) > (playerX + 0.45)) && (Mathf.Abs(newPosition.z) < (playerZ - 0.45)) && (Mathf.Abs(newPosition.z) > (playerZ + 0.45)) && (Mathf.Abs(newPosition.y) < (playerY - 0.95)) && (Mathf.Abs(newPosition.y) > (playerY + 0.95)))
      /*if(!(((playerX + 0.4) < (newPosition.x + 0.5)) || ((playerX + 0.4) > (newPosition.x - 0.5)) || ((playerX - 0.4) < (newPosition.x + 0.5)) || ((playerX - 0.4) < (newPosition.x - 0.5)) ||  //x block check 
          ((playerY + 0.4) < (newPosition.y + 0.5)) || ((playerY + 0.4) > (newPosition.y - 0.5)) || ((playerY - 0.4) < (newPosition.y + 0.5)) || ((playerY - 0.4) < (newPosition.y - 0.5)) ||   //y block check
          ((playerZ + 0.4) < (newPosition.z + 0.5)) || ((playerZ + 0.4) > (newPosition.z - 0.5)) || ((playerZ - 0.4) < (newPosition.z + 0.5)) || ((playerZ - 0.4) < (newPosition.z - 0.5))))    //z block check*/



      if (newPosition.y < (PlayerPrefs.GetInt("worldY") + 1))
      {
        // Call from WG script
        WorldGenerator.BlockBuilder(newPosition, // N = C + delta
        this.transform.parent.gameObject);
        float blockplacecounter = PlayerPrefs.GetFloat("blocksplaced");
        blockplacecounter += 1;
        PlayerPrefs.SetFloat("blocksplaced", blockplacecounter);
      }
    }
  }
 }
}

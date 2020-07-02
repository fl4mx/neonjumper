using UnityEngine;
using System.Collections;
 
public class blockdelcore : MonoBehaviour
{
  public Vector3 delta;
 
  void OnMouseOver()
  {
  // LMB down
  if (Input.GetMouseButtonDown(0))
  {
    if (Time.timeScale == 1f){ //to prevent loophole where you can pause game and continue building, to rescue yourself
      // Destroy the parent of the face we clicked
      Destroy(this.transform.parent.gameObject);
    }
  }
 
  // RMB down
  if (Input.GetMouseButtonDown(1))
  {
    // Added code to fix block glitches (building into player and then getting stuck)
    if (Time.timeScale == 1f){
      Vector3 newPosition = this.transform.parent.transform.position + delta;
      if (newPosition.y < (PlayerPrefs.GetInt("worldY") + 1))
      {
        // Call from worldgenerator script
        WorldGenerator.BlockBuilder(newPosition, // where newPosition = oldPosition + delta
        this.transform.parent.gameObject);
        // Count the number of blocks placed for blocksplaced stat
        float blockplacecounter = PlayerPrefs.GetFloat("blocksplaced");
        blockplacecounter += 1;
        // Similarly update the playerpref value
        PlayerPrefs.SetFloat("blocksplaced", blockplacecounter);
      }
    }
  }
 }
}

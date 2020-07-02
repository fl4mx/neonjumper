using UnityEngine;
using System.Collections;
 
public class WorldGenerator : MonoBehaviour 
{
  // Public fields are visible and their values can be changed dirrectly in the editor
 
  // Drag and drop here the Voxel from the Scene
  // Used to create new instances
  public GameObject Voxel;
  public GameObject Arena;
  public GameObject player;
 
  private GameObject[] blockArray;

  //Specify the dimensions of the world
  private float SizeX;
  private float SizeZ;
  private float SizeY;


  private int legacyblockdel;
  private int music;

  public GameObject timesurvivedtext;
  public GameObject blocksplacedtext;




  private float startTime;
  private float surviveTime;


  //Start coroutines and variable inits
  void Start () 
  {
    // Grab playerprefs and start SimpleGenerator coroutine
    legacyblockdel = PlayerPrefs.GetInt("legacy");
    music = PlayerPrefs.GetInt("music");
    if (music == 0)
    {
      Debug.Log("music is off");
      player.GetComponent<AudioSource>().mute = true;
    }
    else
    {
      Debug.Log("music is on");
      player.GetComponent<AudioSource>().mute = false;
    }
    PlayerPrefs.SetFloat("blocksplaced", 0);
    SizeX = PlayerPrefs.GetInt("worldX");
    SizeZ = PlayerPrefs.GetInt("worldZ");
    SizeY = PlayerPrefs.GetInt("worldY");
    //Toggle switches between coroutine between legacy and new blockdel
    StartCoroutine(SimpleGenerator());
    if (legacyblockdel == 0)
    {
      Debug.Log("using new blockdel sys");
      InvokeRepeating("BlockDelete", 0.0f, PlayerPrefs.GetFloat("deleteTime"));
    }
    else if (legacyblockdel == 1)
    {
      Debug.Log("using legacy blockdel sys");
      InvokeRepeating("legacyBlockDelete", 0.0f, PlayerPrefs.GetFloat("deleteTime"));
    }
    startTime = Time.time;
  }
  
  // Update playerPref values on each frame tick
  void Update () 
  {  	
    surviveTime = Time.time - startTime;
    PlayerPrefs.SetFloat("surviveTime", surviveTime);
    arrayadder(); //add GameObjects to array
    //Debug.Log(blockArray[1]); check to see GameObjects are indeed getting added to the array
    timesurvivedtext.GetComponent<TextMesh>().text = PlayerPrefs.GetFloat("surviveTime").ToString();
    blocksplacedtext.GetComponent<TextMesh>().text = PlayerPrefs.GetFloat("blocksplaced").ToString();
  }

  public void arrayadder()
  {
    blockArray = GameObject.FindGameObjectsWithTag("voxel");
  }


// New blockdeletion, 3 x y x 3 matrix below player for death optimisation
  public void BlockDelete()
  {
    Destroy(blockArray[Random.Range(0, blockArray.Length)]);
  }


// Legacy block decay algorithm, full rng across entire terrain dimensions, not the available blocks.
  public void legacyBlockDelete()
  {
    int counter = 0;
    int total = Mathf.RoundToInt(Mathf.Ceil((PlayerPrefs.GetInt("worldX") + PlayerPrefs.GetInt("worldY") + PlayerPrefs.GetInt("worldZ"))/4));
    while (counter < total)
    {
      //Vector3 objpos = new Vector3(Mathf.Floor(Random.Range(0.0f, SizeX + 1)), Mathf.Floor(Random.Range(0.0f, SizeY)), Mathf.Floor(Random.Range(0.0f, SizeZ + 1)));
      //Aims to keep objpos in the range of pregenerated blocks, can extend to cover the built blocks.
      Vector3 objpos = new Vector3(Mathf.Floor(Random.Range(0.0f, SizeX + 1)), Mathf.Floor(Random.Range(0.0f, SizeY + 1)), Mathf.Floor(Random.Range(0.0f, SizeZ + 1)));
      string objname = "Cube@" + objpos;
      GameObject obj = (GameObject)GameObject.Find(objname);
      //Debug.Log(obj); check if obj has the correct value.
      Destroy(obj);
      counter++;
    }
  }

  public static void BlockBuilder(Vector3 newPosition, GameObject originalGameobject)
  {
    // Clone and Build voxel
    GameObject clone = (GameObject)Instantiate(originalGameobject, newPosition, Quaternion.identity);
    // Repositioning voxel
    clone.transform.position = newPosition;
    // Rename voxel to new position
    clone.name = "Cube@" + clone.transform.position;
  }

  IEnumerator SimpleGenerator()
  {
    //Coroutine, generate 60 voxels per frame. 60 is a relatively good amount of GameObjects to build each frame for good FPS
    //60 builds per frame also ensures the player to not spawn on a decayed block.
    uint numberOfInstances = 0;
    uint instancesPerFrame = 120;
   
    for(int x = 1; x <= SizeX; x++) //SizeX is worldX
    {
     for(int z = 1; z <= SizeZ; z++) //vice versa
     {
      //RNG terrain placements for blocks one by one
      float height = Random.Range(0, SizeY); //similarly as before, SizeY is worldY
      for (int y = 0; y <= height; y++)
      {
        //Determine position of new block
        Vector3 newPosition = new Vector3(x, y, z);
        //Call blockbuilder to build at newPosition
		    BlockBuilder(newPosition, Voxel);
    		//+ the number of times this has been done to keep count
    		numberOfInstances++;
 
        //If number of instances per frame was all good
        if(numberOfInstances == instancesPerFrame)
        {
          //Reset numberOfInstances
          numberOfInstances = 0;
          //Wait for next frame
          yield return new WaitForEndOfFrame();
      }
     }
    }
   }
  }


//Move the arena to center it around the generated island
  public void ArenaReposition()
  {
    Vector3 ArenaNewPosition = new Vector3(SizeX/2, SizeY/2, SizeZ/2);
    Arena.transform.position = ArenaNewPosition;
  }
}

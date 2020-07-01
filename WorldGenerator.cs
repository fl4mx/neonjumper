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

  //int blocksplaced;

/*
  public GameObject obj;
  public Vector3 objpos;
  public String objname;
*/

  // Use this for initialization
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
    //switch coroutine between legacy and new blockdel
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
  
  // Update is called once per frame
  void Update () 
  {  	
    surviveTime = Time.time - startTime;
    PlayerPrefs.SetFloat("surviveTime", surviveTime);
    arrayadder();
    //Debug.Log(blockArray[1]); check to see if syntax for adding gameobjects to array works
    timesurvivedtext.GetComponent<TextMesh>().text = PlayerPrefs.GetFloat("surviveTime").ToString();
    blocksplacedtext.GetComponent<TextMesh>().text = PlayerPrefs.GetFloat("blocksplaced").ToString();
  }

  public void arrayadder()
  {
    blockArray = GameObject.FindGameObjectsWithTag("voxel");
  }


//new blockdeletion, 3 x y x 3 matrix below u for death optimisation
  public void BlockDelete()
  {
    Destroy(blockArray[Random.Range(0, blockArray.Length)]);
  }


//legacy blockdeletion, full rng across terrain
  public void legacyBlockDelete()
  {
    int counter = 0;
    int total = Mathf.RoundToInt(Mathf.Ceil((PlayerPrefs.GetInt("worldX") + PlayerPrefs.GetInt("worldY") + PlayerPrefs.GetInt("worldZ"))/4));
    while (counter < total)
    {
      //Vector3 objpos = new Vector3(Mathf.Floor(Random.Range(0.0f, SizeX + 1)), Mathf.Floor(Random.Range(0.0f, SizeY)), Mathf.Floor(Random.Range(0.0f, SizeZ + 1))); keeps it in the range of pregenerated blocks, can extend to cover the built blocks.
      Vector3 objpos = new Vector3(Mathf.Floor(Random.Range(0.0f, SizeX + 1)), Mathf.Floor(Random.Range(0.0f, SizeY + 1)), Mathf.Floor(Random.Range(0.0f, SizeZ + 1)));
      string objname = "Cube@" + objpos;
      GameObject obj = (GameObject)GameObject.Find(objname);
      //Debug.Log(obj);
      Destroy(obj);
      counter++;
    }
  }

  public static void BlockBuilder(Vector3 newPosition, GameObject originalGameobject)
  {
    // Clone
    GameObject clone = (GameObject)Instantiate(originalGameobject, newPosition, Quaternion.identity);
    // Place
    clone.transform.position = newPosition;
    // Rename
    clone.name = "Cube@" + clone.transform.position;
  }

  IEnumerator SimpleGenerator()
  {
   //this is a coroutine, generate 60 voxels per frame. 60 is a nice number. unsigned int because counting
   uint numberOfInstances = 0;
   uint instancesPerFrame = 120;
   
   for(int x = 1; x <= SizeX; x++) //size x is defined in the game (is a variable for modularity)
   {
     for(int z = 1; z <= SizeZ; z++) //so is size z
     {
       //rng a height to chuck the voxel
      float height = Random.Range(0, SizeY); //and size y
      for (int y = 0; y <= height; y++)
      {
        //works out the position of the new voxel to chuck it
        Vector3 newPosition = new Vector3(x, y, z);
        //BlockBuilder is the method that builds voxels ^^^^^ up there somewhere
		    BlockBuilder(newPosition, Voxel);
    		// + the number of times this has been done to keep count
    		numberOfInstances++;
 
        // if number of instances per frame was all good, then nothing crashed and burned,
        if(numberOfInstances == instancesPerFrame)
        {
          // Reset numberOfInstances
          numberOfInstances = 0;
          // Wait for next frame
          yield return new WaitForEndOfFrame();
          //HELP IDEK WHAT IS GOING ON HERE?????????????
      }
     }
    }
   }
  }


  public void ArenaReposition()
  {
    Vector3 ArenaNewPosition = new Vector3(SizeX/2, SizeY/2, SizeZ/2);
    Arena.transform.position = ArenaNewPosition;
  }
}
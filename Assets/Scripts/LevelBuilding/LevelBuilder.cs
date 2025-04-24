using NUnit.Framework.Constraints;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

// TODO: modul-auswahl verbessern

public class LevelBuilder : MonoBehaviour
{
    private Transform parentTransform; //transform of the empty parentobject

    public int numberOfModules; //the number of modules to be spawned
    public float coinSpawnChance; //the chance for coins to spawn in each module
    public float powerupSpawnChance; //the chance for a powerup to spawn in each module

    //Array containing all prefabs that can be spawned. Will be assigned to Modules in modules[]
    public GameObject[] modulePrefabs;
    private LevelModule[] modules;

    public GameObject finalModule; //the last module to be spawned

    private LevelModule currentModule;
    private GameObject currentPrefab; //the current prefab spawn
    private LevelModule previous; //the previous prefab spawn
    private float x = 0; //the x coordinate of the current spawn

    private void Awake()
    {
        parentTransform = GetComponent<Transform>();
        modules = AssignPrefabs();
    }

    void Start()
    {
        BuildLevel();
    }

    //spawn Level Modules in a row and in random order
    private void BuildLevel()
    {
        for (int c = 0; c < numberOfModules; c++)
        {
            //pick a random Module from modules[]
            currentModule = modules[Random.Range(0, modules.Length)];

            //spawn prefab and attatch it to the parent object
            currentModule.instance = Instantiate(currentModule.prefab, new Vector3(x, 0, 0), Quaternion.identity, parentTransform);

            //calculate spawn position for next module (-0.05f for seamless blending)
            x += currentModule.GetWidth() - 0.05f;
  

            /*TODO: spawn Polaroid
            currentModule.Spawn(LevelModule.Collectible.Polaroid)
            */

            currentModule.Spawn(LevelModule.Collectible.PowerUp, ChanceOf(powerupSpawnChance));
            if(!currentModule.ContainsPowerUp)
                currentModule.Spawn(LevelModule.Collectible.Coins, ChanceOf(coinSpawnChance));

            //update previous spawn for next loop iteration
            previous = currentModule;
        }

        //spawn final module
        Instantiate(finalModule, new Vector3(x, 0, 0), Quaternion.identity, parentTransform);
    }

    private LevelModule[] AssignPrefabs()
    {
        LevelModule[] modules = new LevelModule[modulePrefabs.Length];
        int index = 0;

        foreach (GameObject modulePrefab in modulePrefabs)
        {
            modules[index] = new LevelModule(modulePrefab);

            index++;
        }

        return modules;
    }

    //return value is determined by the input float (if chance = 0.25f it will return true 25% of the time)
    private bool ChanceOf(float chance)
    {
        if (Random.Range(0f, 1f) <= chance)
            return true;

        else return false;
    }
}

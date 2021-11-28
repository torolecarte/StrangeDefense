using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WaveSpawnerController : MonoBehaviour
{
    // Inspector Properties.
    public List<GameObject> SpawnLocations;
    public int MonstersPerSpawn = 1;
    public int MonstersPerWave = 10;
    public int NumberOfWaves = 10;
    public float TimeBetweenSpawns = 2f;
    public float TimeBetweenWaves = 30f;
    public GameObject MonsterToSpawn;
    public GameObject MainTarget;
    public TMPro.TextMeshProUGUI WaveText;
    public CanvasGroup EndGameCanvasGroup;
    public Text EndGameText;
    public Image WinIndicatorImage;

    // Fields.
    private int _currentWave = 0;
    private float _timeToNextWave = 0f;
    private float _timeToNextSpawn = 0f;
    private float _monsterSpawnedThisWave = 0f;
    private bool _winGame = false;
    private List<GameObject> _listOfMonstersSpawned = new List<GameObject>();

    void Start()
    {
        MonsterToSpawn.GetComponent<EnemyMovement>().ObjectToChase = MainTarget;
    }
    void Update()
    {
        if(_winGame)
            return;

        if (Time.time > _timeToNextSpawn && MonstersPerWave > _monsterSpawnedThisWave)
        {
            _monsterSpawnedThisWave++;
            _timeToNextSpawn = Time.time + TimeBetweenSpawns;
            int indexOfSpawnLocation = Random.Range(0, SpawnLocations.Count);
            var spawnLocation = SpawnLocations[indexOfSpawnLocation];

            var gameObject = Instantiate(MonsterToSpawn, spawnLocation.transform.position, Quaternion.identity);
            _listOfMonstersSpawned.Add(gameObject);
        }

        if (Time.time > _timeToNextWave && NumberOfWaves > _currentWave)
        {
            _currentWave++;
            _monsterSpawnedThisWave = 0;
            _timeToNextWave = Time.time + TimeBetweenWaves;
            WaveText.text = _currentWave.ToString();
        }

        if (_currentWave == NumberOfWaves && _listOfMonstersSpawned.All(x => x == null))
        {
            WinGame();
        }
    }

    // Private Methods.
    private void WinGame()
    {
        _winGame = true;
        EndGameCanvasGroup.interactable = true;
        EndGameCanvasGroup.alpha = 1;
        EndGameText.text = "Você Venceu!";
        WinIndicatorImage.color = Color.green;
    }
}

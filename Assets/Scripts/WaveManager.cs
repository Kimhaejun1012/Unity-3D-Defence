using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveManager : MonoBehaviour
{
    public MonsterSpawner spawner;
    public string csvName = "waves";

    private List<IGrouping<int, WaveRow>> groupedWaves;
    public int currentWaveIndex = -1;
    public bool isWaveRunning = false;

    public float timeBetweenWaves = 5f;
    void Start()
    {
        LoadWaves();
        StartNextWave();
    }

    void LoadWaves()
    {
        List<WaveRow> rows = WaveCSVLoader.Load(csvName);

        groupedWaves = rows
            .GroupBy(r => r.wave)
            .OrderBy(g => g.Key)
            .ToList();
    }

    public void StartNextWave()
    {
        if (isWaveRunning) return;

        currentWaveIndex++;

        if (currentWaveIndex >= groupedWaves.Count)
        {
            return;
        }

        UIManager.Instance.UpdateWaveUI(
            currentWaveIndex + 1,
            groupedWaves.Count
        );

        StartCoroutine(RunWaveRoutine());
    }

    private IEnumerator RunWaveRoutine()
    {
        isWaveRunning = true;


        var waveRows = groupedWaves[currentWaveIndex];

        foreach (var row in waveRows)
        {

            for (int i = 0; i < row.count; i++)
            {
                spawner.Spawn(row.monsterType);
                yield return new WaitForSeconds(row.interval);
            }
        }

        isWaveRunning = false;

        yield return new WaitForSeconds(timeBetweenWaves);
        StartNextWave();
    }
}
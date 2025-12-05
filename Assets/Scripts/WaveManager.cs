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
            StartCoroutine(WaitForAllMonstersAndClear());
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

        yield return StartCoroutine(WaitNextWaveRoutine());
        StartNextWave();
    }
    private IEnumerator WaitNextWaveRoutine()
    {
        float timer = 0f;

        while (timer < timeBetweenWaves)
        {
            timer += Time.deltaTime;
            float progress = 1f - timer / timeBetweenWaves;

            UIManager.Instance.UpdateWaveDelayUI(progress);

            yield return null;
        }

        UIManager.Instance.UpdateWaveDelayUI(1f);
    }

    private IEnumerator WaitForAllMonstersAndClear()
    {
        while (spawner.activeMonsterCount > 0)
        {
            yield return null;
        }

        UIManager.Instance.ShowClearPanel();

        int nextStage = StageManager.selectedStage + 1;
        SaveManager.data.unlockedStage =
            Mathf.Max(SaveManager.data.unlockedStage, nextStage);
        SaveManager.Save();
    }
}

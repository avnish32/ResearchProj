using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WAB : MonoBehaviour
{
    private const int WIN_SCORE = 5, MAX_MISSES = 3;

    [SerializeField]
    RectTransform[] spawnPts;

    [SerializeField]
    BullyFaceButton bullyFaceButtonPrefab;

    [SerializeField]
    TextMeshProUGUI scoreText, livesText, startButtonText;

    [SerializeField]
    RectTransform slotParentBehind, slotParentFront, slot;

    //minSpawnWait >= maxLifetime?
    [SerializeField]
    private float minSpawnWait, maxSpawnWait, minBullyFaceLifetime, maxBullyFaceLifetime;

    private bool shouldSpawn = false;
    private int score = 0, miss = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = string.Format("Score: {0}", score);
        livesText.text = string.Format("Lives left: {0}", MAX_MISSES - miss);
    }

    private IEnumerator SpawnBullyFace()
    {
        while (shouldSpawn)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnWait, maxSpawnWait));

            if (!shouldSpawn)
            {
                continue;
            }

            RectTransform randomSpawnPt = spawnPts[UnityEngine.Random.Range(0, spawnPts.Length)];
            BullyFaceButton instantiatedBullyFace = Instantiate(bullyFaceButtonPrefab, randomSpawnPt, false);
            instantiatedBullyFace.Init(UnityEngine.Random.Range(minBullyFaceLifetime, maxBullyFaceLifetime), this,
                slotParentBehind, slotParentFront, slot);
            maxBullyFaceLifetime = Mathf.Clamp(maxBullyFaceLifetime-0.1f, 0.35f, maxBullyFaceLifetime);
        }
    }

    public void StartSpawning()
    {
        if (shouldSpawn)
        {
            return;
        }
        startButtonText.text = "...";
        shouldSpawn = true;
        StartCoroutine(SpawnBullyFace());
    }

    public void StopSpawning()
    {
        shouldSpawn = false;
    }

    public void IncrementScore()
    {
        if (++score >= WIN_SCORE)
        {
            StopSpawning();
            Debug.Log("WAB won.");
            FindObjectOfType<Bully_DialogueMgr>().OnBullyDefeated();
            Destroy(gameObject);
        }
        scoreText.text = string.Format("Score: {0}", score);
    }

    public void IncrementMiss()
    {
        Debug.Log("Bully missed.");
        if (++miss >= MAX_MISSES)
        {
            StopSpawning();
            Debug.Log("WAB lost.");
            FindObjectOfType<Bully_DialogueMgr>().OnBullyWon();
            Destroy(gameObject, 2f);
        }
        livesText.text = string.Format("Lives left: {0}", 
            Mathf.Clamp(MAX_MISSES - miss, 0, MAX_MISSES));
    }
}

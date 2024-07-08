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
    TextMeshProUGUI scoreText, livesText;

    [SerializeField]
    RectTransform slotParentBehind, slotParentFront, slot;

    //minSpawnWait >= maxLifetime
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnBullyFace()
    {
        while (shouldSpawn)
        {
            RectTransform randomSpawnPt = spawnPts[UnityEngine.Random.Range(0, spawnPts.Length)];
            BullyFaceButton instantiatedBullyFace = Instantiate(bullyFaceButtonPrefab, randomSpawnPt, false);
            instantiatedBullyFace.Init(UnityEngine.Random.Range(minBullyFaceLifetime, maxBullyFaceLifetime), this,
                slotParentBehind, slotParentFront, slot);
            maxBullyFaceLifetime = Mathf.Clamp(maxBullyFaceLifetime-0.2f, 0.5f, maxBullyFaceLifetime);

            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnWait, maxSpawnWait));
        }
    }

    public void StartSpawning()
    {
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
            Destroy(gameObject);
        }
        livesText.text = string.Format("Lives left: {0}", MAX_MISSES - miss);
    }
}

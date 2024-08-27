using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WAB : MonoBehaviour
{
    private const int WIN_SCORE = 5, MAX_MISSES = 3;

    [SerializeField]
    Canvas myCanvas;

    [SerializeField]
    RectTransform[] spawnPts;

    [SerializeField]
    BullyFaceButton bullyFaceButtonPrefab;

    [SerializeField]
    TextMeshProUGUI scoreText, livesText, startButtonText, onEndText;

    [SerializeField]
    RectTransform slotParentBehind, slotParentFront, slot;

    [SerializeField]
    AudioClip onMissSfx, onHitSfx, clickSfx, winSfx, lossSfx;

    [SerializeField]
    GameObject winPanel, losePanel;

    [SerializeField]
    Animator scoreTextAnimator, livesTextAnimator;

    //minSpawnWait >= maxLifetime?
    [SerializeField]
    private float minSpawnWait, maxSpawnWait, minBullyFaceLifetime, maxBullyFaceLifetime;

    private bool shouldSpawn = false, hasMinigameEnded = false;
    private int score = 0, miss = 0;
    private AudioController audioController;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = string.Format("Score: {0}", score);
        livesText.text = string.Format("Lives left: {0}", MAX_MISSES - miss);
        onEndText.text = string.Empty;

        winPanel.gameObject.SetActive(false);
        losePanel.gameObject.SetActive(false);
    }

    public void Init(AudioController audioController, GameController gameController)
    {
        this.audioController = audioController;
        this.gameController = gameController;
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
                slotParentBehind, slotParentFront, slot, audioController.IsGameJuicy());
            maxBullyFaceLifetime = Mathf.Clamp(maxBullyFaceLifetime-0.05f, 0.35f, maxBullyFaceLifetime);
        }
    }

    public void OnStartButtonClicked()
    {
        if (shouldSpawn || gameController.IsGamePaused() || hasMinigameEnded)
        {
            return;
        }
        audioController.PlaySound(clickSfx);
        startButtonText.text = "...";
        StartSpawning();
    }

    public void OnExitButtonClicked()
    {
        audioController.PlaySound(clickSfx);
        StopSpawning();
        if (!hasMinigameEnded)
        {
            gameController.EnablePlayerMovement();
        }
        Destroy(gameObject);
    }

    private void StartSpawning()
    {
        shouldSpawn = true;
        StartCoroutine(SpawnBullyFace());
    }

    private void StopSpawning()
    {
        shouldSpawn = false;
    }

    public void IncrementScore()
    {
        if (gameController.IsGamePaused())
        {
            return;
        }

        scoreText.text = string.Format("Score: {0}", ++score);
        if (gameController.IsGameJuicy())
        {
            /*Animator scoreTextAnimator = scoreText.GetComponent<Animator>();
            scoreTextAnimator.enabled = true;*/
            //Debug.Log("Current state OnUpdate?: " + scoreTextAnimator.GetCurrentAnimatorStateInfo(0).IsName("OnUpdate"));
            scoreTextAnimator.Play("OnUpdate");
        }
        
        audioController.PlaySound(onHitSfx);
        if (score >= WIN_SCORE)
        {
            hasMinigameEnded = true;
            myCanvas.sortingOrder = 100;
            StopSpawning();
            Debug.Log("WAB won.");
            FindObjectOfType<Bully_DialogueMgr>().OnBullyDefeated();
            //Destroy(gameObject);

            if (gameController.IsGameJuicy())
            {
                winPanel.gameObject.SetActive(true);
                audioController.PlaySound(winSfx);
            } else
            {
                onEndText.text = "Man, you can fight! Press 'exit' to leave.";
            }
        }
        
    }

    public void IncrementMiss()
    {
        ++miss;
        audioController.PlaySound(onMissSfx);
        livesText.text = string.Format("Lives left: {0}",
            Mathf.Clamp(MAX_MISSES - miss, 0, MAX_MISSES));
        //Debug.Log("Bully missed.");

        if (gameController.IsGameJuicy())
        {
            /*Animator livesTextAnimator = livesText.GetComponent<Animator>();
            livesTextAnimator.enabled = true;*/
            livesTextAnimator.Play("OnUpdate");
        }

        if (miss >= MAX_MISSES)
        {
            hasMinigameEnded = true;
            myCanvas.sortingOrder = 100;
            StopSpawning();
            Debug.Log("WAB lost.");
            
            FindObjectOfType<Bully_DialogueMgr>().OnBullyWon();
            //Destroy(gameObject, 2f);

            if (gameController.IsGameJuicy())
            {
                losePanel.gameObject.SetActive(true);
                audioController.PlaySound(lossSfx);
            }
            else
            {
                onEndText.text = "Looks like Roger was a bit too good for you. Press 'exit' to leave.";
            }
        }
        
    }

    public void OnPauseButtonClicked()
    {
        gameController.OnPauseButtonClicked();
    }

    public void OnGamePaused()
    {
        /*wasSpawningBeforePaused = shouldSpawn;
        StopSpawning();*/
    }

    public void OnGameResumed()
    {
        /*if (wasSpawningBeforePaused)
        {
            StartSpawning();
        }*/
    }
}

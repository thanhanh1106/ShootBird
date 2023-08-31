using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    enum posX
    {
        Left,
        Right
    }
    public Bird[] birds;

    public float SpawnTime;
    public float TimeLimit;
    float currentTimeLimit;

    int countBirdKilled;

    bool isGameStar = false;
    bool isGameOver = false;

    float minPosY = -1f;
    float maxPosY = 3f;
    float posxLeft = -10f;
    float posxRight = 10f;

    public int CountBirdKilled { get => countBirdKilled; set => countBirdKilled = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public bool IsGameStar { get => isGameStar; set => isGameStar = value; }

    protected override void Awake()
    {
        MakeSingleton(false);
        currentTimeLimit = TimeLimit;
    }
    private void Start()
    {
        isGameStar = false;
        GameGUIManager.Instance.ShowGameGUI(false);
        GameGUIManager.Instance.UpdateKilled(countBirdKilled);
    }
    public void Play()
    {
        isGameStar = true;
        StartCoroutine(SpawnBirdCountDown());
        StartCoroutine(GameOverCountDown());
        GameGUIManager.Instance.ShowGameGUI(true);
    }
    private void Update()
    {
        
    }

    // dùng coroutine
    // coroutine là cơ chế cho phép thực hiện các tác vụ bất đồng bộ 
    // định nghĩa 1 method dùng để làm corountine bằng từ khóa IEnumertator
    // yield để tạm dừng hoặc thực hiện 1 tác vụ nào đấy và chờ tác vụ ấy trả về (giống xử lý bất đồng bộ bằng callback)
    IEnumerator SpawnBirdCountDown()
    {
        while(!isGameOver)
        {
            SpawnBird();
            // yield là từ khóa để tạo ra trình tự trả về
            // dòng bên dưới là để tạm dừng 1 corountine 1 khoảng thời gian trước khi tiếp tục
            yield return new WaitForSeconds(SpawnTime); 
        }
    }
    IEnumerator GameOverCountDown()
    {
        while(currentTimeLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTimeLimit -= 1f;
            if(currentTimeLimit <= 0) 
            {
                isGameOver = true;
                if (countBirdKilled > Prefs.BestScore)
                    GameGUIManager.Instance.GameDialog.UpdateDialog("New Best", "Best Killed:" + countBirdKilled.ToString());
                else
                    GameGUIManager.Instance.GameDialog.UpdateDialog("Your Best", "Best Killed:" + Prefs.BestScore);
                Prefs.BestScore = countBirdKilled;
                GameGUIManager.Instance.GameDialog.Show(true);
                GameGUIManager.Instance.CurrentDialog = GameGUIManager.Instance.GameDialog;
            }
            GameGUIManager.Instance.UpdateTimer(IntToTime(currentTimeLimit));
        } 
    }

    public void SpawnBird()
    {
        Vector3 spawPos;
        posX posx = Random.Range(0f,1f) < 0.5f ? posX.Left : posX.Right;
        Thread.Sleep(2);
        float PosY = Random.Range(minPosY, maxPosY);
          if(posx == posX.Left) spawPos = new Vector3(posxLeft, PosY, 0);
          else spawPos = new Vector3(posxRight,PosY, 0);
        if(birds.Length > 0 && birds != null)
        {
            int randBird = Random.Range(0, birds.Length);
            if (birds[randBird] != null)
            {
                var birdClone = Instantiate(birds[randBird],spawPos,Quaternion.identity);
            }
        }
    }
    string IntToTime(float timeInt)
    {
        int minues = (int)timeInt / 60;
        int seconds = (int)timeInt % 60;
        string Time = minues.ToString("00") + ":" + seconds.ToString("00");
        return Time;

    }
    public void Pause()
    {
        Time.timeScale = 0f;
        if(GameGUIManager.Instance.PauseDialog)
        {
            GameGUIManager.Instance.PauseDialog.Show(true);
            GameGUIManager.Instance.PauseDialog.UpdateDialog("Your Best", "Best Killed:" + Prefs.BestScore);
            //GameGUIManager.Instance.CurrentDialog = GameGUIManager.Instance.PauseDialog;
        }
        
    }
    public void Resume()
     {
        Time.timeScale = 1f;
        if (GameGUIManager.Instance.PauseDialog) GameGUIManager.Instance.PauseDialog.Show(false);
    }
    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // GetActiveScene() lấy ra Scene hiện tại

    }
    public void Exit()
    {
        Application.Quit();
    }
}

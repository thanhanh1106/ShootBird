using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject HomeGUI;
    public GameObject GameGUI;

    public Dialog GameDialog;
    public Dialog PauseDialog;

    public Image FireRateFilled;
    public Text TimerText;
    public Text KilledCountingText;

    Dialog currentDialog;

    public Dialog CurrentDialog { get => currentDialog; set => currentDialog = value; }

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    public void ShowGameGUI(bool isShow)
    {
        if(GameGUI) GameGUI.SetActive(isShow);
        if(HomeGUI) HomeGUI.SetActive(!isShow); // nếu mà show game gui rồi thì ẩn thằng home gui đi và ngược lại
    }
    public void UpdateTimer(string time)
    {
        if(TimerText) TimerText.text = time;
    }
    public void UpdateKilled(int killed)
    {
        KilledCountingText.text = killed.ToString();
    }
    public void UpdateFireRate(float fireRate)
    {
         if(FireRateFilled) FireRateFilled.fillAmount = fireRate;
    }


}

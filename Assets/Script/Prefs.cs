using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs
{
    // PlayerPrefs là cách để unity lưu trữ và truy cập dữ liệu trong game (lưu ngay cả khi tắt game)
    // cho phép lưu lưu trữ các thông tin như cấu hình, điểm số hoặc bất kì dữ liệu nào muốn lưu giữa các phiên bản chơi 
    // lưu trữ trên cơ chế cặp key - value
    public static int BestScore
    {
        get => PlayerPrefs.GetInt(GameConst.BEST_SCORE,0);
        set
        {
            int currentScore = PlayerPrefs.GetInt(GameConst.BEST_SCORE); 
            if(currentScore < value)
            {
                PlayerPrefs.SetInt(GameConst.BEST_SCORE, value);
            }
        }
    }
}

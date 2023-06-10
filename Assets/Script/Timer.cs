using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerTxt;
    public float time = 120f;
    private float selectCountdown;
    public SimplePlayerController playerController;

    void Start() {
        selectCountdown = time;
    }

    void Update() {
        if(Mathf.Floor(selectCountdown) <= 0) {
            // Count 0일때 동작할 함수 삽입
            playerController.ChangeHealth(-playerController.player_current_health);
        } else {
            selectCountdown -= Time.deltaTime;
            timerTxt.text = Mathf.Floor(selectCountdown).ToString();
        }
    }
}

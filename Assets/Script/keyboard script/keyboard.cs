using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 키입력 상태
public enum e_KeyBord_State
{
    ING, // 진행중
    FAIL, // 실패
    SUCCESE, // 성공
}

public class keyboard : MonoBehaviour
{
    public static keyboard Instance;
    // 패턴을 표시할 UI 패널
    public GameObject keyboardbase;
    public Text txtTime;

    // 키들
    private key[] keys;

    // 키 랜덤 풀
    List<KeyCode> keyCodePool = new List<KeyCode>() { KeyCode.A,KeyCode.D,KeyCode.E,KeyCode.S, KeyCode.Q, KeyCode.W};

    // 현재 키 입력 순서
    private int idx;

    private float time;

    private void Awake()
    {
        Instance = this;
        keys = keyboardbase.GetComponentsInChildren<key>(true);
        StopPattern();
    }

    public void StartPattern(System.Action<e_KeyBord_State> callback)
    {
        keyboardbase.SetActive(true);
        // 여기에 패턴 시작 관련 코드 추가

        for (int i = 0; i < keys.Length; ++i)
        {
            int rnd = Random.Range(0, keyCodePool.Count);
            keys[i].SetData(keyCodePool[rnd]);
        }
        idx = 0;
        time = 50f;
        StartCoroutine(C_Update(callback));
    }

    public void StopPattern()
    {
        keyboardbase.SetActive(false);
        // 여기에 패턴 종료 관련 코드를 추가
    }

    IEnumerator C_Update(System.Action<e_KeyBord_State> callback)
    {
        float prevScale = Time.timeScale;
        float deltaTime = Time.deltaTime;
        Time.timeScale = 0f;
        

        e_KeyBord_State res = e_KeyBord_State.ING;
        while (true)
        {
            txtTime.text = time.ToString("F2");
            time -= deltaTime;
            // 시간초과
            if (time <= 0)
            {
                res = e_KeyBord_State.FAIL;
                break;
            }
            // 입력완료
            if (idx >= keys.Length)
            {
                res = e_KeyBord_State.SUCCESE;
                break;
            }

            // 현재 키를 누르면
            if (Input.anyKeyDown == true)
            {
                if (Input.GetKeyDown(keys[idx].code) == true)
                {
                    // 다음 키 검사
                    // idx는 후위증가로 함수 실행 후 값이 증가됨
                    keys[idx++].SetSuccese(true);
                }
                // 키가 틀리면
                else
                {
                    res = e_KeyBord_State.FAIL;
                    break;
                }
            }
            // 다 완료하지 못했으므로 리턴
            res = e_KeyBord_State.ING;
            yield return null;
        }

        Time.timeScale = prevScale;
        callback?.Invoke(res);
    }
}

    

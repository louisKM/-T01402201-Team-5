using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public string thisScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReStart() {
        thisScene=SceneManager.GetActiveScene().name;
        Time.timeScale=1f;
        SceneManager.LoadSceneAsync(thisScene);
    }

    public void GameOver() {
        SceneManager.LoadScene("GameOver");
    }
}

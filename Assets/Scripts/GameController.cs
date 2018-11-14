using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class GameController : MonoBehaviour {

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject restartText;
    [SerializeField] private Text player1ScoreText, player2ScoreText;


    private PlayerStats player1Stats;
    private PlayerStats player2Stats;
    private bool ended = false;
    private bool gameStopped = false;
    private int player1Score = 0, player2Score = 0;

	void Start () {
        player1Stats = player1.GetComponent<PlayerStats>();
        player2Stats = player2.GetComponent<PlayerStats>();
	}
	
	void Update () {
        if (player1Stats.announceEndGame() && !gameStopped)
            player2Won();
        if (player2Stats.announceEndGame() && !gameStopped)
            player1Won();
        if (ended == true && Input.GetKeyDown(KeyCode.R))
            restartGame();
        if (Input.GetKeyDown(KeyCode.Escape))
            quitToMainMenu();
	}

    void player1Won()
    {
        gameStopped = true;
        FindObjectInChilds(player2, "HealthBar").SetActive(false);
        player1Score++;
        endGame();
        
    }

    void quitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void player2Won()
    {
        gameStopped = true;
        FindObjectInChilds(player1, "HealthBar").SetActive(false);
        player2Score++;
        endGame();
        
    }

    void endGame()
    {
        player1.GetComponent<Platformer2DUserControl>().blockControlls();
        player2.GetComponent<Platformer2DUserControl>().blockControlls();
        Instantiate(restartText, restartText.transform.position, restartText.transform.rotation);
        ended = true;
        StartCoroutine(BlinkRestartText());
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
    }
    private void restartGame()
    {
        player1Stats.restartGame();
        player2Stats.restartGame();
        player1.GetComponent<Transform>().position = new Vector3(0, 0, 0);
        player2.GetComponent<Transform>().position = new Vector3(20, 0, 0);
        player1.GetComponent<Platformer2DUserControl>().unblockControlls();
        player2.GetComponent<Platformer2DUserControl>().unblockControlls();
        gameStopped = false;
        ended = false;
    }

    IEnumerator BlinkRestartText()
    {
        GameObject restart = GameObject.Find("Restart game(Clone)");
        while (ended)
        {
            restart.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            restart.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public static GameObject FindObjectInChilds(GameObject gameObject, string gameObjectName)
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform item in children)
        {
            if (item.name == gameObjectName)
            {
                return item.gameObject;
            }
        }

        return null;
    }
}

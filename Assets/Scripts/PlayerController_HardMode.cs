using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController_HardMode : MonoBehaviour
{
    private PlayerView view;
    private PlayerModel model;

    public int lives = 3;
    public GameObject[] hearts;

    [SerializeField] Text ScoreText; //refer to ScoreText UI

    private int score = 0;
    public bool hasCollidedWithBranch = false;
    public bool hasPassedBranch = false;

    void Start()
    {
        view = GetComponent<PlayerView>();
        model = new PlayerModel();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(moveInput, 0, 0);

        view.Move(moveDirection, model.speed * 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Branch"))
        {
            hasCollidedWithBranch = true;
            LostLife();
            UpdateHearts();
        }
        else if (other.CompareTag("Star_HardMode"))
        {
            AddScore(10);
            other.gameObject.SetActive(false);
            UpdateScoreText();
        }
    }

    void LostLife()
    {
        if (lives > 0)
        {
            lives--;
            UpdateHearts();
        }
        if (lives <= 0)
        {
            Debug.Log("Game Over!");
            StartCoroutine(WaitAndLoadGameOverScene());
        }
        hasCollidedWithBranch = false;
    }

    IEnumerator WaitAndLoadGameOverScene()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("GameOver");
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < lives);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        ScoreText.text = score.ToString() + " PTS";
    }
}


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
    [SerializeField] private ParticleSystem starCollectEffect;
    [SerializeField] private AudioClip starCollectSound;

    [SerializeField] private AudioClip branchHitSound;
    private Renderer playerRenderer;  // Reference to the player's renderer
    [SerializeField] private Camera mainCamera;  // Reference to the main camera
    [SerializeField] private float shakeDuration = 0.5f;  // Duration of camera shake
    [SerializeField] private float shakeMagnitude = 0.1f;  // Intensity of camera shake
    private Vector3 originalCameraPosition;  // Store the camera's original position

    private int score = 0;
    public bool hasCollidedWithBranch = false;
    public bool hasPassedBranch = false;

    void Start()
    {
        view = GetComponent<PlayerView>();
        model = new PlayerModel();
        playerRenderer = GetComponent<Renderer>();
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
            SoundManager.Instance.PlaySFX(branchHitSound);
            StartCoroutine(FlashAfterHit());
            StartCoroutine(ShakeCamera());
        }
        else if (other.CompareTag("Star_HardMode"))
        {
            AddScore(10);
            other.gameObject.SetActive(false);
            // Play star collect sound
            SoundManager.Instance.PlaySFX(starCollectSound);
            // Play particle effect at the star's position
            if (starCollectEffect != null)
            {
                // Move the particle system to the star's position
                starCollectEffect.transform.position = other.transform.position;
                // Play the particle system
                starCollectEffect.Play();
            }

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
            PlayerPrefs.SetInt("PlayerScore", score);
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

    IEnumerator FlashAfterHit()
    {
        float endTime = Time.time + 2;

        // Continue flashing until the duration is complete
        while (Time.time < endTime)
        {
            // Toggle visibility
            playerRenderer.enabled = !playerRenderer.enabled;

            // Wait for the flash interval
            yield return new WaitForSeconds(0.15f);
        }

        // Ensure player is visible when done
        playerRenderer.enabled = true;
    }

    IEnumerator ShakeCamera()
    {
        // If no camera is assigned, try to find the main camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogWarning("No camera found for screen shake effect!");
                yield break;  // Exit if no camera is found
            }
        }

        // Store the original camera position
        originalCameraPosition = mainCamera.transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // Calculate a random offset within shake magnitude
            float xOffset = Random.Range(-shakeMagnitude, shakeMagnitude);
            float yOffset = Random.Range(-shakeMagnitude, shakeMagnitude);

            // Apply the offset to the camera
            mainCamera.transform.position = originalCameraPosition + new Vector3(xOffset, yOffset, 0f);

            // Update elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Reset camera position when done shaking
        mainCamera.transform.position = originalCameraPosition;
    }
}


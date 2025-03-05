using UnityEngine;
using UnityEngine.UI;

public class PlayerLivesUI : MonoBehaviour
{
    public int maxLives = 3; // Maximum number of lives for each player
    private int currentLivesPlayer1;
    private int currentLivesPlayer2;

    public GameObject player1LifeIconPrefab; // Prefab for player 1 life icon
    public GameObject player2LifeIconPrefab; // Prefab for player 2 life icon
    public Transform livesContainerPlayer1; // Container for player 1 life icons
    public Transform livesContainerPlayer2; // Container for player 2 life icons

    void Start()
    {
        currentLivesPlayer1 = maxLives;
        currentLivesPlayer2 = maxLives;
        UpdateLivesUI();
    }
    public void InitialiseLives(int livesPlayer1, int livesPlayer2)
    {
        currentLivesPlayer1 = livesPlayer1;
        currentLivesPlayer2 = livesPlayer2;
        UpdateLivesUI();
    }
    public void LoseLife(int player)
    {
        if (player == 1 && currentLivesPlayer1 > 0)
        {
            currentLivesPlayer1--;
            UpdateLivesUI();
        }
        else if (player == 2 && currentLivesPlayer2 > 0)
        {
            currentLivesPlayer2--;
            UpdateLivesUI();
        }
    }

    public void GainLife(int player)
    {
        if (player == 1 && currentLivesPlayer1 < maxLives)
        {
            currentLivesPlayer1++;
            UpdateLivesUI();
        }
        else if (player == 2 && currentLivesPlayer2 < maxLives)
        {
            currentLivesPlayer2++;
            UpdateLivesUI();
        }
    }

    public void UpdateLivesUI()
    {
        // Clear existing icons
        ClearLivesContainer(livesContainerPlayer1);
        ClearLivesContainer(livesContainerPlayer2);

        // Create icons based on current lives
        for (int i = 0; i < currentLivesPlayer1; i++)
        {
            //Vector3 offset = new Vector3(-50 * i, 0, 0);
            //Instantiate(player1LifeIconPrefab, livesContainerPlayer1.position + offset, Quaternion.identity, livesContainerPlayer1);
            GameObject lifeIcon = Instantiate(player1LifeIconPrefab, livesContainerPlayer1);
            lifeIcon.transform.localPosition = new Vector3(-120 * i, 0, 0);
        }

        for (int i = 0; i < currentLivesPlayer2; i++)
        {
            Instantiate(player2LifeIconPrefab, livesContainerPlayer2);
        }
    }

    private void ClearLivesContainer(Transform container)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
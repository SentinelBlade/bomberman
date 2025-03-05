using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    public GameObject gameOverUI;
    public PlayerLivesUI playerLivesUI;

    private void Start()
    {
        gameOverUI.SetActive(false); // Ensure Game Over UI is initially disabled
        playerLivesUI.InitialiseLives(players[0].GetComponent<MovementController>().lives, players[1].GetComponent<MovementController>().lives);
    }

    public void CheckWinState()
    {
        int aliveCount = 0;

        foreach (GameObject player in players)
        {
            var movementController = player.GetComponent<MovementController>();
            if (movementController != null && movementController.lives > 0)
            {
                aliveCount++;
            }
        }

        if (aliveCount <= 1)
        {
            GameOver();
        }
    }

    private void NewRound()
    {
        foreach (GameObject player in players)
        {
            var movementController = player.GetComponent<MovementController>();
            if (movementController.lives > 0)
            {
                player.SetActive(true);
                movementController.Respawn();
            }
        }

        playerLivesUI.UpdateLivesUI(); // Update UI for player lives

        
    }

    public void LoseLife(GameObject player)
    {
        var movementController = player.GetComponent<MovementController>();
        if (movementController != null)
        {
            movementController.lives--;
            if (player == players[0]) // Assuming players[0] is Player 1
            {
                
                playerLivesUI.LoseLife(1); // Update UI for Player 1
            }
            else if (player == players[1]) // Assuming players[1] is Player 2
            {
                playerLivesUI.LoseLife(2); // Update UI for Player 2
            }

            if (movementController.lives <= 0)
            {
                movementController.DeathSequence();
            }
            else
            {
                playerLivesUI.UpdateLivesUI(); // Update UI after losing a life
            }
        }
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true); // Show Game Over UI
        Time.timeScale = 0; // Stop the game
    }

    private bool AllPlayersOutOfLives()
    {
        foreach (GameObject player in players)
        {
            var movementController = player.GetComponent<MovementController>();
            if (movementController.lives > 0)
            {
                return false;
            }
        }
        return true;
    }
   


}

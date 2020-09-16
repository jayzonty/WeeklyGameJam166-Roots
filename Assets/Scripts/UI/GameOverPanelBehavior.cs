using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WGJRoots
{
    public class GameOverPanelBehavior : MonoBehaviour
    {
        public GameState gameState;

        public Text gameOverText;
        public Button retryButton;

        public void RefreshText()
        {
            string text = "Game Over!";

            if (gameState.BranchPoints == 0)
            {
                text += "\n\nYou ran out of branch points!";
            }
            else
            {
                text += "\n\nAll your roots are trapped!";
            }

            gameOverText.text = text;
        }

        private void OnEnable()
        {
            retryButton.onClick.AddListener(HandleRetry);
        }

        private void OnDisable()
        {
            retryButton.onClick.RemoveListener(HandleRetry);
        }

        private void Start()
        {
        }

        private void HandleRetry()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}

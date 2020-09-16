using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WGJRoots
{
    public class StartGameButtonBehavior : MonoBehaviour
    {
        public string targetScene = "";

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(GoToTargetScene);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(GoToTargetScene);
        }

        private void GoToTargetScene()
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}

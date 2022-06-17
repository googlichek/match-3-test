using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button = default;

        void OnEnable()
        {
            _button.onClick.AddListener(HandleButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleButtonClick);
        }

        private void HandleButtonClick()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }
}

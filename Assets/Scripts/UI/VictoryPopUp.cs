using UnityEngine;
using UnityEngine.UI;

namespace TestGame.UI
{
    public class VictoryPopUp : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Text _totalScoreText;

        private LevelPoints _levelPoints;

        private void Start()
        {
            _levelPoints = FindObjectOfType<LevelPoints>();
        }

        private void Update()
        {
            _totalScoreText.text = _levelPoints.LevelScorePoints.ToString();
        }

        public void PopUp()
        {
            _canvas.gameObject.SetActive(true);
        }
    }
}
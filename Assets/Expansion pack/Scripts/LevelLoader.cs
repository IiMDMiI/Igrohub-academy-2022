using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace ExpansionPack
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private List<Level> _levels;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _next;
        [SerializeField] private WinLoseUI _winLoseUI;
        private const string CompletedLevels = "CompletedLevels";
        private int _currentLevelIndex;
        private Level _currentLevel;


        private void Awake()
        {
            _restart.onClick.AddListener(() => LoadLevel(_currentLevelIndex));
            _next.onClick.AddListener(LoadNextLevel);

            PlayerPrefs.SetInt(CompletedLevels, 0);
            _currentLevelIndex = PlayerPrefs.GetInt(CompletedLevels, 0);
            LoadLevel(_currentLevelIndex);
        }
        private void LoadLevel(int index)
        {
            _currentLevel = Instantiate(_levels[index]);
            SubscribeToLevelEvents(_currentLevel);
        }

        private void LoadNextLevel()
        {
            if (_currentLevel != null)
            {
                UnsubscribeFromLevelEvents(_currentLevel);
                Destroy(_currentLevel.gameObject);
            }

            _currentLevelIndex = _currentLevelIndex + 1 >= transform.childCount ? 0 : _currentLevelIndex + 1;
            SaveLevel();
            LoadLevel(_currentLevelIndex);
        }
        private void SaveLevel() =>
            PlayerPrefs.SetInt(CompletedLevels, _currentLevelIndex);

        private void SubscribeToLevelEvents(Level level)
        {
            level.OnLose += OnLoseHandler;
            level.OnWin += OnWinHandler;
        }
        private void UnsubscribeFromLevelEvents(Level level)
        {
            level.OnLose -= OnLoseHandler;
            level.OnWin -= OnWinHandler;
        }

        private void OnWinHandler()
        {
            _winLoseUI.Show(true);
        }
        private void OnLoseHandler()
        {
            _winLoseUI.Show(false);
            //LoadNextLevel();
        }

        private IEnumerator ChangeLevel(float delay = 0.5f)
        {
            _winLoseUI.Hide();
            yield return new WaitForSeconds(delay);
            
        }


    }

}

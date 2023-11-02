using System.Collections;
using ComponentsAndTags;
using TMPro;
using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI ScoreText;
    public GameObject GameOverUI;
    public TextMeshProUGUI FinalScoreText;

    private Entity _playerEntity;
    private EntityManager _entityManager;

    private bool _finishedGame, _shouldUpdateUI;
    
    private IEnumerator Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        yield return new WaitForSeconds(.2f);
        _playerEntity = _entityManager.CreateEntityQuery(typeof(PlayerTag)).GetSingletonEntity();

        _shouldUpdateUI = true;
    }

    void Update()
    {
        if (_shouldUpdateUI)
        {
            var currentPlayerHealth = _entityManager.GetComponentData<Health>(_playerEntity).Value;
            var currentPlayerScore = _entityManager.GetComponentData<Score>(_playerEntity).Value;

            HealthText.text = $"Health: {currentPlayerHealth}";
            ScoreText.text = $"Score: {currentPlayerScore}";

            if (currentPlayerHealth <= 0 && !_finishedGame)
            {
                _finishedGame = true;
            
                HealthText.enabled = false;
                ScoreText.enabled = false;
            
                GameOverUI.SetActive(true);
            
                FinalScoreText.text = $"Final Score: {currentPlayerScore}";
                
                Time.timeScale = 0;
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

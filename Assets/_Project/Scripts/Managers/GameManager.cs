using System.Collections.Generic;
using Scripts.Utilities.Singletons;
using UnityEngine;

public class GameManager : NonPersistentSingleton<GameManager>
{

    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private GameObject _playerInScene;
    [SerializeField] private float _deathYLevel = -25f;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {

        CheckErrors();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _playerInScene.transform.position = _playerSpawnPoint.position;
    }

    void Update()
    {

        if (_playerInScene.transform.position.y > _deathYLevel)
        {
            return;
        }

        RespawnPlayer();

    }

    private void RespawnPlayer()
    {


        _playerInScene.transform.position = _playerSpawnPoint.position;
    }

    private void CheckErrors()
    {
        List<string> errors = new();

        if (_playerSpawnPoint == null)
        {
            errors.Add("GameManager: Player Spawn Point is not assigned.");
        }

        if (_playerInScene == null)
        {
            errors.Add("GameManager: Player In Scene is not assigned.");
        }
        if (errors.Count > 0)
        {
            foreach (var error in errors)
            {
                Debug.LogError(error);
            }
            throw new System.Exception("GameManager initialization failed due to errors.");
        }

    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public UnityEvent<Color, int> ChangeEnemiesCountWithColor;
    
    [SerializeField] 
    private EnemyController _enemyPrefab;

    [SerializeField] 
    private int _enemiesCount = 6;
    
    [SerializeField]
    private int _randomRadius = 10;

    private ColorsProvider _colorsProvider;
    
    private readonly Dictionary<Color, int> _enemiesCountsByColor = new();

    public void Initialize(ColorsProvider colorsProvider)
    {
        _colorsProvider = colorsProvider;
        var colors = _colorsProvider.GetAllColors();
        
        foreach (var color in colors)
        {
            _enemiesCountsByColor.Add(color, 0);
        }

        for (var i = 0; i < _enemiesCount; i++)
        {
            var enemy = Instantiate(_enemyPrefab);
            SetEnemyPosition(enemy);
            
            var color = _colorsProvider.GetColor();
            enemy.Initialize(color);
            _enemiesCountsByColor[color] += 1;
            
            ChangeEnemiesCountWithColor.Invoke(color, _enemiesCountsByColor[color]);
            enemy.EnemyDiedEvent.AddListener(EnemyDied);
        }
    }

    private void EnemyDied(EnemyController enemyController)
    {
        _enemiesCountsByColor[enemyController.Color]--;
        ChangeEnemiesCountWithColor.Invoke(enemyController.Color, _enemiesCountsByColor[enemyController.Color]);
    }

    private void SetEnemyPosition(EnemyController enemyController)
    {
        var randomPosition = Random.insideUnitCircle * _randomRadius;
        
        var enemyPosition = enemyController.transform.position;
        
        enemyPosition.x += randomPosition.x;
        enemyPosition.z += randomPosition.y;
        
        enemyController.transform.position = enemyPosition;
    }
}
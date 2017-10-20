using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWorld : MonoBehaviour
{
    private const float MinSpawnInterval = 3f;
    private const float MaxSpawnInterval = 4f;
    private const float MaxObstacleSpawnHeightDeviation = 1f;
    
    [SerializeField] private BackgroundScroller _backScroller;
    [SerializeField] private Transform _obstacleSpawnPosition;
    [SerializeField] protected GameObject ObstaclePrefab;

    protected List<BaseObstacle> Obstacles = new List<BaseObstacle>();
    protected float TimeSinceLastSpawned;
    protected float GravityValue = 1;
    protected float LeftScreenBorderXValue; 
    
    protected virtual void Start()
    {
        Physics2D.gravity = Vector2.down * GravityValue;

        var dist = (transform.position - Camera.main.transform.position).z;
        LeftScreenBorderXValue = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
    }

    public void Move(float value)
    {
        _backScroller.Move(value);

        for (int i = 0; i < Obstacles.Count; i++)
        {
            Obstacles[i].Move(value);
        }
    }

    private void DespawnInvisibleObstacles()
    {
        for (int i = 0; i < Obstacles.Count; i++)
        {
            if (Obstacles[i].transform.position.x < LeftScreenBorderXValue && !Obstacles[i].GetComponent<Renderer>().isVisible)
            {
                PoolManager.Despawn(Obstacles[i].gameObject);
                Obstacles.Remove(Obstacles[i]);
                i--;
            }
        }
    }

    public virtual void CallUpdate(float dt)
    {
        DespawnInvisibleObstacles();

        TimeSinceLastSpawned += dt;
        if (TimeSinceLastSpawned >= Random.Range(MinSpawnInterval, MaxSpawnInterval))
        {
            TimeSinceLastSpawned = 0f;
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        var spawnPosition = new Vector2(_obstacleSpawnPosition.position.x, Random.Range(_obstacleSpawnPosition.position.y, _obstacleSpawnPosition.position.y + MaxObstacleSpawnHeightDeviation));
        var obstacle = PoolManager.Spawn(ObstaclePrefab, spawnPosition, Quaternion.identity);
        obstacle.transform.SetParent(transform);
        Obstacles.Add(obstacle.GetComponent<BaseObstacle>());
    }
}

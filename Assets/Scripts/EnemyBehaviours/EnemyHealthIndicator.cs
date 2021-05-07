using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnemyHealthIndicator : MonoBehaviour
{
    private Camera _mainCamera;
    private EnemyInfo _enemyInfo;
    [SerializeField] private Slider _healthSlider;

    [Inject]
    private void Construct(Camera mainCamera)
    {
        _mainCamera = mainCamera;
       
    }
    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = _mainCamera;
    }
    private void Start()
    {
        _enemyInfo = transform.parent.GetComponent<EnemyInfo>();
    }
    private void Update()
    {
        _healthSlider.value = _enemyInfo._health;
    }
}

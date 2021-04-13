using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    [SerializeField] private AllEnemies _allEnemies;
    [SerializeField] private Player _player;
    [SerializeField] private Camera _mainCamera;


    public override void InstallBindings()
    {
        BindEnemyFactory();
        BindEnemiesWithBehaviour();
        BindCamera();
        BindPlayerWithArrow();
    }
    private void BindCamera()
    {
        Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();
    }

    private void BindPlayerWithArrow()
    {
        Container.Bind<Player>().FromInstance(_player).AsSingle();
    }

    private void BindEnemiesWithBehaviour()
    {
        Container.Bind<AllEnemies>().FromInstance(_allEnemies).AsSingle();
    }

    private void BindEnemyFactory()
    {
        Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
    }
}
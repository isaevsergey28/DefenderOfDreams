using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    [SerializeField] private AllEnemies _allEnemies;

    public override void InstallBindings()
    {
        BindEnemyFactory();
        BindEnemiesWithBehaviour();
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
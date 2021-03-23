using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindEnemyFactory();
    }

    private void BindEnemyFactory()
    {
        Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
    }
}
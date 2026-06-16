using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MyMoleClickerLifeTimeScope : LifetimeScope
{
    [SerializeField]
    private MoleGameConfig moleGameConfig = new MoleGameConfig();

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(moleGameConfig);

        builder.Register<IScoreServiceM, MyScoreServiceM>(Lifetime.Singleton);
        builder.Register<ISaveServiceM, MyPlayerPrefsSaveServiceM>(Lifetime.Singleton);

        builder.RegisterComponentInHierarchy<MyAudioManagerM>().As<IAudioServiceM>();
        builder.RegisterComponentInHierarchy<MyMoleSpawner>();
        builder.RegisterComponentInHierarchy<MyGameHudControllerM>();
    }
}

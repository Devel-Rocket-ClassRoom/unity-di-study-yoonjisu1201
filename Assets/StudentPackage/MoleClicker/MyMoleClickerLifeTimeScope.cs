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

        //.As<인터페이스>() → 인터페이스 타입으로 꺼내 쓰겠다.
        builder.RegisterComponentInHierarchy<MyAudioManagerM>().As<IAudioServiceM>();
        builder.RegisterComponentInHierarchy<MyMoleSpawner>();
        builder.RegisterComponentInHierarchy<MyGameHudControllerM>();

        //.AsSelf() → 클래스 자기 자신 타입으로 꺼내 쓰겠다.
        builder.RegisterEntryPoint<MoleGameDirector>(Lifetime.Singleton).AsSelf();
    }
}

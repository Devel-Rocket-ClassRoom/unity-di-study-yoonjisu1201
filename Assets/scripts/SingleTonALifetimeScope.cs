using VContainer;
using VContainer.Unity;

public class SingleTonALifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<UISingleton>();
    }
}

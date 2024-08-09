using Zenject;
public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Client>().FromNew().AsSingle();
        Container.Bind<Server>().FromNew().AsSingle();
        Container.Bind<EmeraldsHandler>().FromNew().AsSingle();
        Container.Bind<HPBar>().FromNew().AsSingle();
        Container.Bind<PointsManager>().FromNew().AsSingle();
    }
}

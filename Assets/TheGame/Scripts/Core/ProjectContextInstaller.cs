using Zenject;
using Gameplay;
using Services;
using Tools;
using TheGame;

public class ProjectContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindServices();
        BindGameplay();
    }

    private void BindServices()
    {
        Container.Bind<IInputService>().To<InputService>().AsSingle();
        Container.Bind<IRandomizeService>().To<RandomizeService>().AsSingle();
    }

    private void BindGameplay()
    {
        Container.Bind<ISpellCardMediator>().To<SpellCardMediator>().AsSingle();
        Container.Bind<ISpellCardController>().To<SpellCardController>().AsTransient();
        Container.Bind<Locator>().To<Locator>().AsTransient();
    }
}

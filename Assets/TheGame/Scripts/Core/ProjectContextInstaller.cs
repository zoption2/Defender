using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Gameplay;
using Services;

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
    }

    private void BindGameplay()
    {
        Container.Bind<ISpellCardMediator>().To<SpellCardMediator>().AsSingle();
        Container.Bind<ISpellCardController>().To<SpellCardController>().AsTransient();
    }
}

using Themely.Managers;
using Zenject;

namespace Themely.Installers
{
    public class GameInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PauseMenuGameManager>().AsSingle();
        }
    }
}

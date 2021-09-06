using IPA;
using IPA.Config;
using SiraUtil.Zenject;
using Themely.Installers;
using IPALogger = IPA.Logging.Logger;

namespace Themely
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static IPALogger Log { get; private set; }

        [Init]
        public void Init(IPALogger logger, Config conf, Zenjector zenjector)
        {
            Log = logger;
            //Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();

            zenjector.OnGame<GameInstaller>();
        }

        [OnStart]
        public void OnApplicationStart()
        {
        }

        [OnExit]
        public void OnApplicationQuit()
        {
        }
    }
}

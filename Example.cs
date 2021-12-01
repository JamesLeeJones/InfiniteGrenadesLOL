

namespace Exiled.Example
{
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.Example.Events;

    public class Example : Plugin<Config>
    {
        private static readonly Example Singleton = new Example();


        private Example()
        {
        }

      
        public static Example Instance => Singleton;

  
        public override PluginPriority Priority { get; } = PluginPriority.Last;

  
        public override void OnEnabled()
        {
            RegisterEvents();

            Log.Warn($"I correctly read the string config, its value is: {Config.String}");
            Log.Warn($"I correctly read the int config, its value is: {Config.Int}");
            Log.Warn($"I correctly read the float config, its value is: {Config.Float}");

            base.OnEnabled();
        }

        
        public override void OnDisabled()
        {
            UnregisterEvents();
            base.OnDisabled();
        }

        public class EventHandlers
        {
            public void PlayerJoined(JoinedEventArgs ev)
            {
                ev.Player.Broadcast(5, "<color=lime>This server sucks, please leave now lol!</color>");
            }
        }
        public class EventHandlers
        {
            public void GrenadeThrown()
            {
                ev.Player.giveitem(1,GrenadeHe)
            }
        }
    }

}

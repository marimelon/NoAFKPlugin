using Dalamud.Hooking;
using Dalamud.Plugin;
using System.Runtime.InteropServices;

namespace NoAFKPlugin
{

    [StructLayout(LayoutKind.Explicit)]
    public struct Timer
    {
        [FieldOffset(0x276c8)] public float AFKTimer1;   // kick timer?
        [FieldOffset(0x276cc)] public float AFKTimer2;   // ???
        [FieldOffset(0x276d0)] public float AutoLogoutTimer;
        [FieldOffset(0x275a8)] public float AFKStatusLimit;
    }

    public class Plugin : IDalamudPlugin
    {
        public string Name => "NoAFK";

        private DalamudPluginInterface pi;
        private AddressResolver resolver;


        private delegate void AfkTimerHandlerDelegate(long param_1, float param_2, long param_3);
        private Hook<AfkTimerHandlerDelegate> AFKTimerHandlerHook;


        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            this.pi = pluginInterface;

            this.resolver = new AddressResolver();
            this.resolver.Setup(this.pi.TargetModuleScanner);

            this.AFKTimerHandlerHook = new(resolver.AFKTimeHandlerAddress, AfkTimerHandler);
            this.AFKTimerHandlerHook.Enable();
        }

        public void Dispose()
        {
            this.AFKTimerHandlerHook.Dispose();
            this.pi.Dispose();
        }

        private void AfkTimerHandler(long param_1, float param_2, long param_3)
        {
            this.AFKTimerHandlerHook.Original(param_1, 0.0f, param_3);
        }
    }
}

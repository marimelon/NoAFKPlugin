using Dalamud.Game;
using Dalamud.Game.Internal;
using System;

namespace NoAFKPlugin
{
    class AddressResolver : BaseAddressResolver
    {
        public IntPtr AFKTimeHandlerAddress { get; private set; }

        public IntPtr CameraPotisionAddress { get; private set; }

        protected override void Setup64Bit(SigScanner sig)
        {
            this.AFKTimeHandlerAddress = sig.ScanText("4c 8b dc 49 89 5b 18 49 89 73 20 55 57 41 54 41 55 41 57 49 8d 6b c8");
        }
    }
}

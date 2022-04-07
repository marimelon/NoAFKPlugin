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
            this.AFKTimeHandlerAddress = sig.ScanText("48 8B C4 48 89 58 18 48 89 70 20 55 57 41 55");
        }
    }
}

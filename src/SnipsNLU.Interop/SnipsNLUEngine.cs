using System;
using System.Collections.Generic;
using System.Text;

namespace SnipsNLU
{
    public class SnipsNLUEngine
    {
        public SnipsNLUEngine(string rootDir)
        {
            Initialized = SnipsApi.CreateEngineFromDir(rootDir, out IntPtr enginePtr);
            if (Initialized)
            {
                Ptr = enginePtr;
            }
            else
            {
                Ptr = IntPtr.Zero;
            }
        }

        public bool Initialized { get; } = false;

        public IntPtr Ptr { get; }
    }
}

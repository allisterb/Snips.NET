using System;
using System.Collections.Generic;
using System.Text;

using static SnipsNLU.ffi;

namespace SnipsNLU
{
    public static class SnipsApi
    {
        public unsafe static bool CreateEngineFromDir(string rootDir)
        {
            void** ptr = (void**) IntPtr.Zero;
            SNIPS_RESULT r = ffi.snips_nlu_engine_create_from_dir(rootDir, ptr);
            return false;
        }
    }
}

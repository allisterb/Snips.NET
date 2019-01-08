using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using static SnipsNLU.ffi;

namespace SnipsNLU
{
    public static class SnipsApi
    {
        public unsafe static bool CreateEngineFromDir(string rootDir)
        {
            IntPtr _ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)));
            SNIPS_RESULT r = ffi.snips_nlu_engine_create_from_dir(rootDir, ref _ptr);
            return true;
        }
    }
}

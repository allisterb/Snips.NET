using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using static SnipsNLU.ffi;

namespace SnipsNLU
{
    public static class SnipsApi
    {
        public static bool CreateEngineFromDir(string rootDir, out IntPtr enginePtr)
        {
            try
            {
                enginePtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)));
                SNIPS_RESULT r = ffi.snips_nlu_engine_create_from_dir(rootDir, ref enginePtr);
                return r == SNIPS_RESULT.SNIPS_RESULT_OK;
            }
            catch (Exception)
            {
                enginePtr = IntPtr.Zero;
                return false;
            }
            
        }

        public static bool GetModelVersion(out string version)
        {
            try
            {
                IntPtr versionPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)));
                SNIPS_RESULT r = ffi.snips_nlu_engine_get_model_version(ref versionPtr);
                version = r == SNIPS_RESULT.SNIPS_RESULT_OK ? Marshal.PtrToStringAnsi(versionPtr) : string.Empty;
                return r == SNIPS_RESULT.SNIPS_RESULT_OK;
            }
            catch  (Exception)
            {
                version = string.Empty;
                return false;
            }
        }
    }
}

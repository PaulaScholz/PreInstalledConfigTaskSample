using System;
using System.Diagnostics;

namespace Launcher
{
    class Program
    {
        // HRESULT 80004005 is E_FAIL
        const int E_FAIL = unchecked((int)0x80004005);

        static int Main(string[] args)
        {

            ProcessStartInfo info = new ProcessStartInfo();

            info.UseShellExecute = true;
            
            // this is the protocol
            info.FileName =  @"sample://";

            Process process = new Process();
            process.StartInfo = info;
            int exitCode = 0;

            try
            {
                process.Start();
            }
            catch (Exception ex)
            {
                // default exception exitCode
                exitCode = 3;

                if (ex.HResult == E_FAIL)
                {
                    // the user cancelled the elevated process
                    // by clicking "No" on the Windows elevation dialog
                    exitCode = 1;
                }
            }

            return exitCode;
        }
    }
}

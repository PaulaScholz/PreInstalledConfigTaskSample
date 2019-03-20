//***********************************************************************
//
// Copyright (c) 2019 Microsoft Corporation. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//**********************************************************************​

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
                    // there was some sort of general failure
                    exitCode = 1;
                }
            }

            return exitCode;
        }
    }
}

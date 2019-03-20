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
using Windows.ApplicationModel;
using Windows.ApplicationModel.Background;



namespace BackgroundTasks
{
    public sealed class SessionConnectedTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            try
            {
                Debug.WriteLine("Inside SessionConnectedTask");

                string message = "Task has run";

                try
                {
                    // launch the UWP app
                    await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }               

                ToastHelper.ShowToast("SessionConnectedTask", message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Exception in SessionConnectedTask, message is {0}", ex.Message));
            }
            finally
            {
                // this must always be called, even if there is an exception
                deferral.Complete();
            }
        }
    }
}

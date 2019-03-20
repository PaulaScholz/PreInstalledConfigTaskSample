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
using Windows.ApplicationModel.Background;
using System.Diagnostics;


namespace BackgroundTasks
{
    /// <summary>
    /// To debug this Update task, use the procedure here:
    /// https://docs.microsoft.com/en-us/windows/uwp/launch-resume/run-a-background-task-during-updatetask
    /// 
    /// This task is triggered by updating the build number in the last tab of the Packaging project's Package.appxmanifest
    /// </summary>
    public sealed class UpdateTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            try
            {
                Debug.WriteLine("Inside UpdateTask");

                ToastHelper.ShowToast("UpdateTask", "Task has run.");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Exception in UpdateTask, message is {0}", ex.Message));
            }
            finally
            {
                // this must always be called, even if there is an exception
                deferral.Complete();
            }            
        }
    }
}
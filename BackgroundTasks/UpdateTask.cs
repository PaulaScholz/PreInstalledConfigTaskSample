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
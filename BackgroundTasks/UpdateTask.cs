using System;
using Windows.ApplicationModel.Background;
using System.Diagnostics;


namespace BackgroundTasks
{
    /// <summary>
    /// To debug this Update task, use the procedure here:
    /// https://docs.microsoft.com/en-us/windows/uwp/launch-resume/run-a-background-task-during-updatetask
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
                deferral.Complete();
            }            
        }
    }
}
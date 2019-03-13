using System;
using System.Diagnostics;
using Windows.ApplicationModel.Background;


namespace BackgroundTasks
{
    public sealed class SessionConnectedTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            try
            {
                Debug.WriteLine("Inside UpdateTask");

                ToastHelper.ShowToast("SessionConnectedTask", "Task has run.");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Exception in SessionConnectedTask, message is {0}", ex.Message));
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}

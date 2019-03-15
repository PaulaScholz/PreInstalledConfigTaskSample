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
                Debug.WriteLine("Inside SessionConnectedTask");

                //await Windows.System.Launcher.LaunchUriAsync(new Uri("sample:"));

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

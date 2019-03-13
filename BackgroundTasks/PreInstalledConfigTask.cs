using System;
using System.Diagnostics;
using Windows.ApplicationModel.Background;

namespace BackgroundTasks
{
    public sealed class PreInstalledConfigTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            try
            {
                Debug.WriteLine("Inside PreInstalledConfigTask");

                ToastHelper.ShowToast("PreInstalledConfigTask", "Task has run.");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Exception in PreInstalledConfigTask, message is {0}", ex.Message));
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}

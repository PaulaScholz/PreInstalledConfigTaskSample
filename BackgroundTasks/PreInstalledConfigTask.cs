using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Background;

namespace BackgroundTasks
{
    public sealed class PreInstalledConfigTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            try
            {
                string message = "Task has run.";

                Debug.WriteLine("Inside PreInstalledConfigTask");

                try
                {
                    // launch the UWP app
                    await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }

                ToastHelper.ShowToast("PreInstalledConfigTask", message);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Exception in PreInstalledConfigTask, message is {0}", ex.Message));
            }
            finally
            {
                // this must always be called, even if there is an exception
                deferral.Complete();
            }
        }
    }
}

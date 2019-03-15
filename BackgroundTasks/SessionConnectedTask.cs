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
                deferral.Complete();
            }
        }
    }
}

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
using System.Threading.Tasks;

namespace TargetUWPApplication
{
    /// <summary>
    /// If this class isn't sealed, BackgroundTask registration will fail.
    /// </summary>
    public sealed class BackgroundTasksClass
    {
        /// <summary>
        /// Register a SessionConnectedTask.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static async Task<BackgroundTaskRegistration> RegisterSessionConnectedBackgroundTask()
        {
            // see if we're able to register a background task
            BackgroundAccessStatus permissionGranted = await BackgroundExecutionManager.RequestAccessAsync();

            BackgroundTaskRegistration backgroundTaskRegistration = null;

            // If denied access, the task will not run.
            if (permissionGranted != BackgroundAccessStatus.DeniedBySystemPolicy &&
                 permissionGranted != BackgroundAccessStatus.DeniedByUser &&
                 permissionGranted != BackgroundAccessStatus.Unspecified)
            {
                // it has to be unregistered first, even if never registered before
                UnregisterBackgroundTasks(App.SessionConnectedBackgroundTaskName);

                // the trigger for the task, in this case, the SessionConnected event
                SystemTrigger trigger = new SystemTrigger(SystemTriggerType.SessionConnected, false);

                // now register it with the static parameters declared in App.xaml.cs
                backgroundTaskRegistration = RegisterBackgroundTask(App.SessionConnectedBackgroundTaskEntryPoint,
                                                                       App.SessionConnectedBackgroundTaskName,
                                                                       trigger,
                                                                       null);
            }

            return backgroundTaskRegistration;
        }

        /// <summary>
        /// Register a background task with the specified taskEntryPoint, name, trigger,
        /// and condition (optional).
        /// </summary>
        /// <param name="taskEntryPoint">Task entry point for the background task.</param>
        /// <param name="name">A name for the background task.</param>
        /// <param name="trigger">The trigger for the background task.</param>
        /// <param name="condition">An optional conditional event that must be true for the task to fire.</param>
        public static BackgroundTaskRegistration RegisterBackgroundTask(String taskEntryPoint, String name, IBackgroundTrigger trigger, IBackgroundCondition condition, BackgroundTaskRegistrationGroup group = null)
        {
            var builder = new BackgroundTaskBuilder();

            builder.Name = name;
            builder.TaskEntryPoint = taskEntryPoint;
            builder.SetTrigger(trigger);

            if (condition != null)
            {
                builder.AddCondition(condition);

                //
                // If the condition changes while the background task is executing then it will
                // be canceled.
                //
                builder.CancelOnConditionLoss = true;
            }

            if (group != null)
            {
                builder.TaskGroup = group;
            }

            BackgroundTaskRegistration task = builder.Register();

            return task;
        }

        /// <summary>
        /// Unregister background tasks with specified name.
        /// </summary>
        /// <param name="name">Name of the background task to unregister.</param>
        public static void UnregisterBackgroundTasks(String name, BackgroundTaskRegistrationGroup group = null)
        {
            //
            // If the given task group is registered then loop through all background tasks associated with it
            // and unregister any with the given name.
            //
            if (group != null)
            {
                foreach (var cur in group.AllTasks)
                {
                    if (cur.Value.Name == name)
                    {
                        cur.Value.Unregister(true);
                    }
                }
            }
            else
            {
                //
                // Loop through all ungrouped background tasks and unregister any with the given name.
                //
                foreach (var cur in BackgroundTaskRegistration.AllTasks)
                {
                    if (cur.Value.Name == name)
                    {
                        cur.Value.Unregister(true);
                    }
                }
            }
        }
    }
}

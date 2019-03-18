# PreInstallConfigTask Sample
## Windows - Developer Incubation and Learning - Paula Scholz

<figure>
  <img src="docimages/PreInstallConfigTaskBadge.png" alt="Pre-Install Config Task"/>
</figure>
Original Equipment Manufacturers (OEMs) and enterprises that create their own operating system images for deployment sometimes need to ship pre-installed applications.  Preinstall and update tasks provide the mechanisms that allow these tasks to run in the background before the app is installed or when it is updated.

Here are the general rules that govern these tasks.
  *  Your app manifest can contain only one `PreInstallConfigTask` and one `UpdateTask`.
  *  Deployment tasks are applicable to any platform type.
  *  Deployment tasks can execute after the deployment operation has been completed and committed.
  *  Failed deployment tasks are not restarted.
  *  Failed deployment tasks do not affect the successful deployment of the app.
  *  Deployment tasks are not restarted after reboot.
  *  Deployment tasks should not depend on one another.

In a UWP application, normal [BackgroundTasks](https://docs.microsoft.com/en-us/windows/uwp/launch-resume/support-your-app-with-background-tasks "Support your app with background tasks") must be registered with the operating system in code by API, but the `PreInstallConfigTask` and `UpdateTask` are deployment tasks that are registered by their manifest before the application ever runs.

In this sample, we will demonstrate the use of the deployment `PreInstallConfigTask` task to automatically run a target UWP application on first startup.  The act of running the target application can register any regular BackgroundTasks and we will show how to do this.

We will show how to use the `UpdateTask` to display a Toast notification when the application's build number is incremented during a software update.  You may augment this task with your own operations if needed.

We will show how to register a "`Protocol`" in the application manifest so the application can be started by referencing a URI, either from a browser or another application.  We will use this capability to support our auto-run operations.

Finally, we will use the `SessionConnected` task, a normal UWP BackgroundTask fired by a `SystemEvent` trigger, to auto-run the target UWP application after user login by using the `Protocol`.  This task will also show a Toast notification.

# Visual Studio Solution

The Visual Studio solution is shown below.  There are four projects:
<figure>
  <img src="docimages/VisualStudioSolution.png" alt="Visual Studio Solution"/>
</figure>

From the top down, we first see the `BackgroundTasks` project, a Universal Windows project. As the name suggests, this is the out-of-process [BackgroundTasks](https://docs.microsoft.com/en-us/windows/uwp/launch-resume/create-and-register-a-background-task "Create and register an out-of-process background task") project of our solution and runs as a separate process when the main UWP application is not in the foreground.

Next, we see the `Launcher` project.  This is a simple "runFullTrust" Win32 application that has the sole purpose of launching the main UWP application by URI [protocol](https://msdn.microsoft.com/en-us/magazine/mt842502.aspx "Protocol Registration and Activation in UWP Apps").  This pure Win32 application is launched by the PreInstallConfigTask and SessionConnected background tasks in response to triggering events.

Next, the solution contains `Packaging`, the start-up project, which is the [Windows Application Packaging Project](https://docs.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-packaging-dot-net "Package a desktop application by using Visual Studio") for our solution.  It is within the context of this project that our solution will be packaged for sideloading and deployment to the [Windows Store](https://www.microsoft.com/en-us/store/apps/windows "Windows Store main page").  This is the project where package capabilities, store logos, and configuration are set.

Finally, the `TargetUWPApplication` project, which is our very simple UWP application that is supported by the BackgroundTasks and Launcher projects.  This is a very simple single-page UWP application that does nothing. We have modified it to respond to protocol activation by the `sample:` URI protocol defined in the `Package.appxmanifest` file of the `Packaging` project.

Let's look at how these projects relate to each other:
<figure>
  <img src="docimages/ProjectLayers.png" alt="Project Layers"/>
</figure>
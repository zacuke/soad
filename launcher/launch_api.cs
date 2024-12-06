using System.Diagnostics;

static class launch_api
{
    public static void execute_launch_api(){
        // Start the Python API process
        var pythonProcessStartInfo = new ProcessStartInfo
        {
            FileName = "python",                          // Specify the command to run
            Arguments = "main.py --mode api",             // Pass in the arguments
            WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), ".."), // Set the working directory
            RedirectStandardOutput = true,               // Optionally read output
            RedirectStandardError = true,                // Optionally read errors
            UseShellExecute = false,                     // Required for redirection
            CreateNoWindow = true                        // Don't create a visible console
        };

        try
        {
            Console.WriteLine("Starting Python API process...");
            var pythonProcess = Process.Start(pythonProcessStartInfo);

            // Optionally, attach event handlers to track process output/errors
            if (pythonProcess != null)
            {
                pythonProcess.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                        Console.WriteLine($"[Python API] {e.Data}");
                };
                pythonProcess.ErrorDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                        Console.Error.WriteLine($"[Python API - Error] {e.Data}");
                };

                pythonProcess.BeginOutputReadLine();
                pythonProcess.BeginErrorReadLine();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to start the Python API process: {ex.Message}");
            Environment.Exit(-1);
        }
    }
}
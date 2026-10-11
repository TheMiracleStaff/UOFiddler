using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Ultima;

namespace UoFiddler.Controls.Classes
{
    /// <summary>
    /// A running ClassicUO based client (such as The Miracle's TmClient) that the Items tab can type commands into.
    /// </summary>
    public sealed class ClientProcess
    {
        private const string _clientFilter = "ClassicUO";

        private ClientProcess(int processId, string processName, string title)
        {
            ProcessId = processId;
            ProcessName = processName;
            Title = title;
        }

        public int ProcessId { get; }

        public string ProcessName { get; }

        public string Title { get; }

        /// <summary>
        /// Lists the running processes with a window whose process name or window title contains "ClassicUO".
        /// The Miracle's TmClient.exe only says so in its title.
        /// </summary>
        public static List<ClientProcess> FindAll()
        {
            var clients = new List<ClientProcess>();

            foreach (Process process in Process.GetProcesses())
            {
                using (process)
                {
                    try
                    {
                        if (process.MainWindowHandle == IntPtr.Zero)
                        {
                            continue;
                        }

                        if (process.ProcessName.IndexOf(_clientFilter, StringComparison.OrdinalIgnoreCase) < 0 &&
                            process.MainWindowTitle.IndexOf(_clientFilter, StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            continue;
                        }

                        clients.Add(new ClientProcess(process.Id, process.ProcessName, process.MainWindowTitle));
                    }
                    catch (InvalidOperationException)
                    {
                        // The process exited while we were looking at it
                    }
                }
            }

            return clients.OrderBy(c => c.Title, StringComparer.OrdinalIgnoreCase).ThenBy(c => c.ProcessId).ToList();
        }

        /// <summary>
        /// Types <paramref name="text" /> into the client's window and presses Enter.
        /// </summary>
        /// <returns>False if the client is no longer running.</returns>
        public bool SendText(string text)
        {
            try
            {
                using Process process = Process.GetProcessById(ProcessId);
                if (process.HasExited)
                {
                    return false;
                }

                return Client.SendText(process.MainWindowHandle, text, sdlClient: true);
            }
            catch (ArgumentException)
            {
                // No process with that id any more
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Title)
                ? $"{ProcessName} (PID {ProcessId})"
                : $"{Title} (PID {ProcessId})";
        }
    }
}

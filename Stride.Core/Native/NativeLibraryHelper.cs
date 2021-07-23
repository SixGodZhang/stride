using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Core
{
    public static class NativeLibraryHelper
    {
        private static readonly Dictionary<string, IntPtr> LoadedLibraries = new Dictionary<string, IntPtr>();

        public static void PreloadLibrary(string libraryName, Type owner)
        {
            lock(LoadedLibraries)
            {
                if (LoadedLibraries.ContainsKey(libraryName))
                {
                    return;
                }

                string cpu;
                string platform;

                switch (RuntimeInformation.ProcessArchitecture)
                {
                    case Architecture.X86:
                        cpu = "x86";
                        break;
                    case Architecture.X64:
                        cpu = "x64";
                        break;
                    case Architecture.Arm:
                        cpu = "ARM";
                        break;
                    default:
                        throw new PlatformNotSupportedException();
                }

                switch(Platform.Type)
                {
                    case PlatformType.Windows:
                        platform = "win";
                        break;
                    default:
                        throw new PlatformNotSupportedException();
                }

                // We are trying to load the all dll from a shadow path if it is already registered, otherwise we use it diectly from the folder
                {
                    foreach (var libraryPath in new[]
                    {
                        Path.Combine(Path.GetDirectoryName(owner.GetTypeInfo().Assembly.Location) ?? string.Empty, $"{platform}-{cpu}"),
                        Path.Combine(Environment.CurrentDirectory?? string.Empty, $"{platform}-{cpu}"),
                        Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) ?? string.Empty, $"{platform}-{cpu}"),
                        // Also try without platform for Windows-only packages (backward compat for editor packages)
                        Path.Combine(Path.GetDirectoryName(owner.GetTypeInfo().Assembly.Location) ?? string.Empty, $"{cpu}"),
                        Path.Combine(Environment.CurrentDirectory??string.Empty, $"{cpu}"),
                    })
                    {
                        var libraryFilename = Path.Combine(libraryPath, libraryName);
                        if(NativeLibrary.TryLoad(libraryFilename, out var result))
                        {
                            LoadedLibraries.Add(libraryName.ToLowerInvariant(), result);
                            return;
                        }
                    }
                }

                // Attempt to load it from PATH
                foreach (var p in Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator))
                {
                    var libraryFilename = Path.Combine(p, libraryName);
                    if(NativeLibrary.TryLoad(libraryFilename, out var result))
                    {
                        LoadedLibraries.Add(libraryName.ToLowerInvariant(), result);
                        return;
                    }
                }

                throw new InvalidOperationException($"Could not load native library {libraryName} using CPU architecture {cpu}.");
            }
        }

        /// <summary>
        /// Unload a specific native dynamic library loaded previously by LoadLibrary
        /// </summary>
        /// <param name="libraryName"></param>
        public static void UnLoad(string libraryName)
        {
            lock (LoadedLibraries)
            {
                IntPtr libHandle;
                if (LoadedLibraries.TryGetValue(libraryName, out libHandle))
                {
                    NativeLibrary.Free(libHandle);
                    LoadedLibraries.Remove(libraryName);
                }
            }
        }


        /// <summary>
        /// UnLoad all native dynamic library loaded previously by LoadLibrary.
        /// </summary>
        public static void UnLoadAll()
        {
            lock(LoadedLibraries)
            {
                foreach (var libraryItem in LoadedLibraries)
                {
                    NativeLibrary.Free(libraryItem.Value);
                }

                LoadedLibraries.Clear();
            }
        }


    }
}

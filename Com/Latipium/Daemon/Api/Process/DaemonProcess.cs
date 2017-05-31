//
// DaemonProcess.cs
//
// Author:
//       Zach Deibert <zachdeibert@gmail.com>
//
// Copyright (c) 2017 Zach Deibert
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Com.Latipium.Daemon.Api.Process {
    /// <summary>
    /// The class that controls how the daemon process works.
    /// </summary>
    public static class DaemonProcess {
        private static Dictionary<string, IService> Services;
        /// <summary>
        /// Gets the daemon connection.
        /// </summary>
        /// <value>The daemon.</value>
        public static Daemon Daemon {
            get;
            private set;
        }

        /// <summary>
        /// Runs the application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Run(string[] args) {
            Services = Assembly.GetEntryAssembly().GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract && typeof(IService).IsAssignableFrom(t))
                .Select(t => t.GetConstructor(new Type[0]).Invoke(new object[0]))
                .Cast<IService>()
                .ToDictionary(a => a.Id);
            AppDomain.CurrentDomain.DomainUnload += (sender, e) => {
                foreach (IService service in Services.Values) {
                    if (service is IDisposable) {
                        ((IDisposable) service).Dispose();
                    }
                }
            };
            if (args.Length != 2) {
                throw new ArgumentException("Invalid arguments to application");
            }
            Daemon = new Daemon(args[0], Guid.Parse(args[1]));
            // TODO connect to the server and start the module
        }
    }
}


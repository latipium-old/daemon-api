//
// AbstractService.cs
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
using Newtonsoft.Json;
using Com.Latipium.Daemon.Api.Model;

namespace Com.Latipium.Daemon.Api.Process {
    /// <summary>
    /// Abstract base class for most services.
    /// </summary>
    public abstract class AbstractService<TRequest, TResponse> : IService, IDisposable where TResponse : ResponseObject {
        /// <summary>
        /// Gets the identifier for the service.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id {
            get;
            private set;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        public abstract TResponse Handle(TRequest req);

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        public string HandleRequest(string request) {
            return JsonConvert.SerializeObject(Handle(JsonConvert.DeserializeObject<TRequest>(request)));
        }

        /// <summary>
        /// Releases all resource used by the <see cref="Com.Latipium.Daemon.Api.Process.AbstractService"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="Com.Latipium.Daemon.Api.Process.AbstractService"/>. The <see cref="Dispose"/> method leaves the
        /// <see cref="Com.Latipium.Daemon.Api.Process.AbstractService"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the
        /// <see cref="Com.Latipium.Daemon.Api.Process.AbstractService"/> so the garbage collector can reclaim the
        /// memory that the <see cref="Com.Latipium.Daemon.Api.Process.AbstractService"/> was occupying.</remarks>
        public void Dispose() {
            Dispose(true);
        }

        /// <summary>
        /// Releases all resource used by the <see cref="Com.Latipium.Daemon.Api.Process.AbstractService"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="Com.Latipium.Daemon.Api.Process.AbstractService"/>. The <see cref="Dispose"/> method leaves the
        /// <see cref="Com.Latipium.Daemon.Api.Process.AbstractService"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the
        /// <see cref="Com.Latipium.Daemon.Api.Process.AbstractService"/> so the garbage collector can reclaim the
        /// memory that the <see cref="Com.Latipium.Daemon.Api.Process.AbstractService"/> was occupying.</remarks>
        protected virtual void Dispose(bool disposing) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Com.Latipium.Daemon.Api.Process.AbstractService"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        protected AbstractService(string id) {
            Id = id;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Com.Latipium.Daemon.Api.Process.AbstractService"/> is reclaimed by garbage collection.
        /// </summary>
        ~AbstractService() {
            Dispose(false);
        }
    }
}


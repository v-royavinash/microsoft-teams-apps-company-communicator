// <copyright file="Program.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// </copyright>

namespace Microsoft.Teams.Apps.CompanyCommunicator
{
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Program class of the company communicator application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main function of the company communicator application.
        /// It builds a web host, then launches the company communicator into it.
        /// </summary>
        /// <param name="args">Arguments passed in to the function.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);
            var app = builder.Build();
            startup.Configure(app, app.Environment);
            app.MapRazorPages();
            app.Run();
        }
    }
}
using System;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Reflection;
using Autodesk.DesignScript.Geometry;
using Dynamo.Models;
using Dynamo;

using DynamoUtilities;
using SuperSocket.SocketBase.Logging;

namespace DynamoWebServer
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            DynamoPathManager.Instance.InitializeCore(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            // It is not yet known to me why, but this encantation is
            // necessary to make the geometry library work

            // Occasionally it fails with a NullReferenceException
            HostFactory.Instance.StartUp();
            var res = Point.ByCoordinates(0, 0);
            var sphere = Sphere.ByCenterPointRadius(res, 1);
            Console.WriteLine(sphere.ToString());

            var model = DynamoModel.Start(
                new DynamoModel.StartConfiguration()
                {
                    Preferences = PreferenceSettings.Load()
                });
            model.MaxTesselationDivisions = int.Parse(ConfigurationManager.AppSettings["MaxTesselationDivisions"]);

            var webSocketServer = new WebServer(model, new WebSocket());

            webSocketServer.Start();

            Console.WriteLine("Started DynamoWebServer!");

            Process.GetCurrentProcess().Exited += webSocketServer.ProcessExited;

            while (true) {}
        }
    }
}

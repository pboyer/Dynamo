using System;
using System.Windows;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using ProtoCore.Utils;

namespace DynamoSandbox
{
    internal class Program
    {
        private static string dynamopath;

        static IEnumerable<string> DirSearch(string dir)
        {
            foreach (string d in Directory.GetDirectories(dir))
            {
                foreach (var ff in DirSearch(d))
                {
                    yield return ff;
                }
            }

            foreach (string f in Directory.GetFiles(dir))
            {
                if (f.EndsWith(".cs"))
                    yield return f;
            }
        }

        [STAThread]
        public static void Main(string[] args)
        {
            var prefix = @"C:\Users\boyerp\Dynamo2\test\Engine";

            foreach (var test in DirSearch(prefix)) 
            {
                System.Console.WriteLine(test);
                var src = File.ReadAllText(test);

                var startPts = new List<int>();
                var endPts = new List<int>();

                var pos = 0;

                char? Peek()
                {
                    for (var n = pos; n < src.Length; n++)
                    {
                        if (!Char.IsWhiteSpace(src[n]))
                        {
                            return src[n];
                        }
                    }

                    return null;
                }

                char? Advance()
                {
                    for (; pos < src.Length; pos++)
                    {
                        if (!Char.IsWhiteSpace(src[pos]))
                        {
                            return src[pos++];
                        }
                    }

                    return null;
                }

                var state = 0;

                while (pos < src.Length)
                {
                    var s = Advance();

                    switch (state)
                    {
                        case 0:
                            if (s == '@')
                            {
                                if (Peek() == '"')
                                {
                                    Advance();
                                    startPts.Add(pos);
                                }
                                state = 1;
                            }
                            break;
                        case 1:
                            if (s == '"')
                            {
                                var n = Peek();
                                if (n == '"') // inner quote
                                {
                                    Advance();
                                    break;
                                }
                                else
                                {
                                    endPts.Add(pos);
                                    state = 0;
                                }
                            }

                            break;
                    }
                }

                var finalCodes = new List<string>();

                for (var i = 0; i < endPts.Count; i++)
                {
                    var start = startPts[i];
                    var end = endPts[i];
                    var code = src.Substring(start, end - start - 1).Replace("\"\"", "\"");
                    try
                    {
                        // convert all deprecated list types to the new syntax
                        var cb = ParserUtils.ParseWithDeprecatedListSyntax(code);
                        
                        var nodes = ParserUtils.FindExprListNodes(cb);

                        var codeList = code.ToCharArray();

                        foreach (var n in nodes)
                        {
                            // ignore nodes not part of original code
                            if (n.charPos < 0 || n.charPos >= codeList.Length ||
                                n.endCharPos < 0 || n.endCharPos >= codeList.Length)
                            {
                                continue;
                            }

                            codeList[n.charPos] = '[';
                            codeList[n.endCharPos - 1] = ']';
                        }

                        finalCodes.Add(new String(codeList));
                        //finalCodes.Add(code);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Code that failed:");
                        Console.WriteLine(code);
                 
                        Console.WriteLine("Stack trace:");
                        Console.WriteLine(e.StackTrace);

                        finalCodes.Add(null);
                    }
                }

                Console.WriteLine("FinalCodes: {0}, EndPts.Count: {1}", finalCodes.Count, endPts.Count);

                {
                    var srcArray = src.ToCharArray();

                    var i = 0;
                    foreach (var finalCode in finalCodes)
                    {
                        if (finalCode == null)
                        {
                            i++;
                            continue;
                        }
                        var finalCode2 = finalCode.Replace("\"", "\"\"");
                         
                        var start = startPts[i];
                        var end = endPts[i];
                        var oldCodeLen = end - start;
                        if (finalCode2.Length + 1 != oldCodeLen)
                        {
                            Console.WriteLine("FAIL");
                            Console.WriteLine(finalCode2);
                            Console.WriteLine(new String(srcArray.Skip(startPts[i]).Take(oldCodeLen).ToArray()));
                            return;
                        }

                        Console.WriteLine("NewCodeLength: {0}, OldCodeLength: {1}", finalCode2.Length, oldCodeLen);

                        for (var j = 0; j < finalCode2.Length; j++)
                        {
                            srcArray[start + j] = finalCode2[j];
                        }

                        i++;
                    }
                    
                    var finalResult = new String(srcArray);

                    Console.WriteLine(finalResult);

                    File.WriteAllText(test, finalResult);
                }

     
            }





            /*
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;

            //Display a message box and exit the program if Dynamo Core is unresolved.
            if (string.IsNullOrEmpty(DynamoCorePath)) return;

            //Include Dynamo Core path in System Path variable for helix to load properly.
            UpdateSystemPathForProcess();

            var setup = new DynamoCoreSetup(args);
            var app = new Application();
            setup.RunApplication(app);*/
        }

        /// <summary>
        /// Handler to the ApplicationDomain's AssemblyResolve event.
        /// If an assembly's location cannot be resolved, an exception is
        /// thrown. Failure to resolve an assembly will leave Dynamo in 
        /// a bad state, so we should throw an exception here which gets caught 
        /// by our unhandled exception handler and presents the crash dialogue.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            var assemblyPath = string.Empty;
            var assemblyName = new AssemblyName(args.Name).Name + ".dll";

            try
            {
                assemblyPath = Path.Combine(DynamoCorePath, assemblyName);
                if (File.Exists(assemblyPath))
                    return Assembly.LoadFrom(assemblyPath);

                var assemblyLocation = Assembly.GetExecutingAssembly().Location;
                var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

                assemblyPath = Path.Combine(assemblyDirectory, assemblyName);
                return (File.Exists(assemblyPath) ? Assembly.LoadFrom(assemblyPath) : null);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("The location of the assembly, {0} could not be resolved for loading.", assemblyPath), ex);
            }
        }

        /// <summary>
        /// Returns the path of Dynamo Core installation.
        /// </summary>
        public static string DynamoCorePath
        {
            get
            {
                if (string.IsNullOrEmpty(dynamopath))
                {
                    dynamopath = GetDynamoCorePath();

                    if (string.IsNullOrEmpty(dynamopath))
                    {
                        NotifyUserDynamoCoreUnresolved();
                    }
                }
                return dynamopath;
            }
        }
        
        /// <summary>
        /// Finds the Dynamo Core path by looking into registery or potentially a config file.
        /// </summary>
        /// <returns>The root folder path of Dynamo Core.</returns>
        private static string GetDynamoCorePath()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version;
            var dynamoRoot = Path.GetDirectoryName(assembly.Location);

            try
            {
                return DynamoInstallDetective.DynamoProducts.GetDynamoPath(version, dynamoRoot);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Add Dynamo Core location to the PATH system environment variable.
        /// This is to make sure dependencies (e.g. Helix assemblies) can be located.
        /// </summary>
        private static void UpdateSystemPathForProcess()
        {
            var path =
                    Environment.GetEnvironmentVariable(
                        "Path",
                        EnvironmentVariableTarget.Process) + ";" + DynamoCorePath;
            Environment.SetEnvironmentVariable("Path", path, EnvironmentVariableTarget.Process);
        }
        
        /// <summary>
        /// If Dynamo Sandbox fails to acquire Dynamo Core path, show a dialog that
        /// redirects to download DynamoCore.msi, and the program should exit.
        /// </summary>
        private static void NotifyUserDynamoCoreUnresolved()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var shortversion = version.Major + "." + version.Minor;

            // Hard-coding the strings in English, since in order to access the
            // Resources files we would need prior resolution to Dynamo Core itself
            if (MessageBoxResult.OK ==
                MessageBox.Show(
                    string.Format(
                        "Dynamo Sandbox {0} is not able to find an installation of " +
                        "Dynamo Core version {0} or higher.\n\nWould you like to download the " +
                        "latest version of DynamoCore.msi from http://dynamobim.org now?", shortversion),
                    "Dynamo Core component missing",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Error))
            {
                Process.Start("http://dynamobim.org/download/");
            }
        }
    }
}

using System.Collections.Specialized;
using System.Diagnostics;

namespace GitMergeDriverSpike
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Debugger.Launch();

            var inputArgs = new CmdInputArgs(args[1], args[0], args[2], int.Parse(args[3]), args[4]);

            Console.WriteLine($"A: {inputArgs.AFileName}");
            Console.WriteLine($"O: {inputArgs.OFileName}");
            Console.WriteLine($"B: {inputArgs.BFileName}");

            Console.WriteLine($"L: {inputArgs.ConflictMarkerSize}");
            Console.WriteLine($"P: {inputArgs.OutputFileName}");

            GitMerge(inputArgs);

            // Figure out branch names.
            // Potential: MERGE_MSG in .git
            // .git can be found through command `git rev-parse --git-dir`
            // Potentially broken w/ configuration: http://git-scm.com/docs/git-merge#Documentation/git-merge.txt-mergesuppressDest

            Console.WriteLine($"Full array: [{string.Join(", ", args)}]");
        }

        private static string GitMerge(CmdInputArgs args)
        {
            Process p = new Process();

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.FileName = "git";
            p.StartInfo.Arguments = $"merge-file --stdout {args.AFileName} {args.OFileName} {args.BFileName}";
            p.Start();

            string stdOut = p.StandardOutput.ReadToEnd();
            string stdErr = p.StandardError.ReadToEnd();

            p.WaitForExit();

            if (stdErr.Length != 0)
            {
                throw new Exception(stdErr);
            }
            return stdOut;
        }
    }

    public class CmdInputArgs
    {
        public string AFileName { get; }
        public string OFileName { get; }
        public string BFileName { get; }

        public int ConflictMarkerSize { get; }

        public string OutputFileName { get; }

        public CmdInputArgs(string aFileName, string oFileName, string bFileName, int conflictMarkerSize, string outputFileName)
        {
            AFileName = aFileName;
            OFileName = oFileName;
            BFileName = bFileName;
            ConflictMarkerSize = conflictMarkerSize;
            OutputFileName = outputFileName;
        }
    }
}
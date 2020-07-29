using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace image2meshinelearning.Controllers
{
    public static class ShellHelper
    {
        public static Task WhenFileCreated(string path)
        {
            if (File.Exists(path))
                return Task.FromResult(true);

            var tcs = new TaskCompletionSource<bool>();
            FileSystemWatcher watcher = new FileSystemWatcher(Path.GetDirectoryName(path));

            FileSystemEventHandler createdHandler = null;
            RenamedEventHandler renamedHandler = null;
            createdHandler = (s, e) =>
            {
                if (e.Name == Path.GetFileName(path))
                {
                    tcs.TrySetResult(true);
                    watcher.Created -= createdHandler;
                    watcher.Dispose();
                }
            };

            renamedHandler = (s, e) =>
            {
                if (e.Name == Path.GetFileName(path))
                {
                    tcs.TrySetResult(true);
                    watcher.Renamed -= renamedHandler;
                    watcher.Dispose();
                }
            };

            watcher.Created += createdHandler;
            watcher.Renamed += renamedHandler;

            watcher.EnableRaisingEvents = true;

            return tcs.Task;
        }
        public static Task<int> Bash(this string cmd)//, ILogger logger)
        {
            var source = new TaskCompletionSource<int>();
            var escapedArgs = cmd.Replace("\"", "\\\"");
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };
            process.Exited += (sender, args) =>
            {
                //logger.LogWarning(process.StandardError.ReadToEnd());
                //logger.LogInformation(process.StandardOutput.ReadToEnd());
                if (process.ExitCode == 0)
                {
                    source.SetResult(0);
                }
                else
                {
                    source.SetException(new Exception($"Command `{cmd}` failed with exit code `{process.ExitCode}`"));
                }

                process.Dispose();
            };

            try
            {
                process.Start();
            }
            catch (Exception e)
            {
                //logger.LogError(e, "Command {} failed", cmd);
                source.SetException(e);
            }

            return source.Task;
        }
    }

    //[Route("api/image2meshinelearning")]
    //[ApiController]
    public class FileUploadController : ControllerBase
    {
        //private ILogger logger;

        [HttpPost]
        public async Task<ActionResult> FileAsync(IFormFile file)
        {
            //Console.WriteLine
            //IFormFile file2 = null;
            //var file = Request.Form.Files[0];
            //await _context.SaveChangesAsync();
            if (file != null)
            {
                //work
                Console.WriteLine("read file");
                var tmpPath = "./abc.png";
                using (var fileStream = System.IO.File.Exists(tmpPath) ? System.IO.File.Open(tmpPath, FileMode.Append) : System.IO.File.Open(tmpPath, FileMode.CreateNew))
                {
                    file.CopyTo(fileStream);
                }
                //this.logger = " ";
                //run_pyt("..\\..\\abc.png", "");
                //await $"scripts/00magic.sh ".Bash();
                //--param { arg}
                Console.WriteLine("ran");
            }
            else
            {
                Console.WriteLine("is null");
            }
            //await ShellHelper.WhenFileCreated(@"./dgl.png");
            Console.WriteLine(".");
            var fileName = "dgl.png";
            var mimeType = "application/octet-stream";
            byte[] ff = System.IO.File.ReadAllBytes("./dgl.png");
            //byte[] ff = null;
            return new FileContentResult(ff, mimeType)
            {
                FileDownloadName = fileName
            };

            //return File(file, "application/octet-stream");
        }
    }
}

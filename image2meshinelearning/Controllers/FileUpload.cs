using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace image2meshinelearning.Controllers
{

    //[Route("api/image2meshinelearning")]
    //[ApiController]
    public class FileUploadController : ControllerBase
    {
        [HttpPost]
        public ActionResult File(IFormFile file)
        {
            //Console.WriteLine
            IFormFile file2 = null;
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
                //run_pyt("..\\..\\abc.png", "");
                Console.WriteLine("ran");
            }
            else
            {
                Console.WriteLine("is null");
            }
            Console.WriteLine(".");
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _2019SignalRandCoravel.Models;
using Coravel.Queuing.Interfaces;
using Microsoft.AspNetCore.SignalR;
using _2019SignalRandCoravel.SignalR;

namespace _2019SignalRandCoravel.Controllers
{

    //STEP 3 - INCORPORATING THE SIGNAL R HUB
    public class HomeController : Controller
    {
        private readonly IQueue _queue;
        private readonly IHubContext<JobProgressHub> _hubContext;

        public HomeController(IQueue queue, IHubContext<JobProgressHub> hubContext)
        {
            _queue = queue;
            _hubContext = hubContext;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StartProgress()
        {
            string jobId = Guid.NewGuid().ToString("N");
            _queue.QueueAsyncTask(() => PerformBackgroundJob(jobId));

            return RedirectToAction("Progress", new { jobId });
        }

        public IActionResult Progress(string jobId)
        {
            ViewBag.JobId = jobId;
            return View();
        }

        private async Task PerformBackgroundJob(string jobId)
        {
            for (int i = 0; i <= 100; i += 5)
            {
                await _hubContext.Clients.Group(jobId).SendAsync("progress", i);

                await Task.Delay(100);
            }
        }
    }

    //STEP 2 - WITHOUT THE FULL SIGNALR HUB YET INCORPORATED
    //public class HomeController : Controller
    //{
    //    private readonly IQueue _queue;

    //    public HomeController(IQueue queue)
    //    {
    //        _queue = queue;

    //    }

    //    public IActionResult Index()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    public IActionResult StartProgress()
    //    {
    //        string jobId = Guid.NewGuid().ToString("N");
    //        _queue.QueueAsyncTask(() => PerformBackgroundJob(jobId));

    //        return RedirectToAction("Progress", new {jobId});
    //    }

    //    public IActionResult Progress(string jobId)
    //    {
    //        ViewBag.JobId = jobId;
    //        return View();
    //    }

    //    private async Task PerformBackgroundJob(string jobId)
    //    {
    //        for (int i = 0; i <= 100; i += 5)
    //        {
    //            await Task.Delay(100);
    //        }
    //    }
    //}

    ////STEP 1 - Use Step1 - to see app without Visuals/Redirects - look at Application Output to see progress
    //    public class HomeController : Controller
    //    {
    //        private readonly IQueue _queue;

    //        public HomeController(IQueue queue)
    //        {
    //            _queue = queue;
    //        }

    //        public IActionResult Index()
    //        {
    //            _queue.QueueAsyncTask(async () =>
    //            {
    //                for (int i = 0; i <= 100; i += 5)
    //                {
    //                    Debug.WriteLine($"Background job progress: {i}");

    //                    await Task.Delay(100);
    //                }
    //            });
    //            return View();
    //        }

    //        public IActionResult Privacy()
    //        {
    //            return View();
    //        }

    //        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //        public IActionResult Error()
    //        {
    //            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //        }
    //    }
    //END STEP 1

}

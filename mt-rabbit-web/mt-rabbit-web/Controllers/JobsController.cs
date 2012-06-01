using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using Core;
using MassTransit;

namespace mt_rabbit_web.Controllers
{
    public class JobsController : Controller
    {
        private readonly IServiceBus _bus;
        private static int _id = 0;

        public JobsController(IServiceBus bus)
        {
            _bus = bus;
        }

        [GET("/")]
        public ActionResult Index()
        {
            return View();
        }

        [POST("/")]
        public ActionResult Index(string jobName)
        {
            _bus.Publish<IStartJob>(new StartJob { CorrelationId = _id, JobName = jobName});
            _id++;
            return RedirectToAction("Index");
        }
    }
}

using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using EventRegistration.Data;
using EventRegistration.Data.Model;

namespace EventRegistration.Controllers
{
	public class HomeController : Controller
	{
		public HomeController()
		{
			string connStr = ConfigurationManager.AppSettings["StorageConnectionString"];

			DataRepository = new RegistrationRepository(connStr);
		}

		public RegistrationRepository DataRepository { get; set; }

		public ActionResult Index()
		{
			ViewBag.RegistrationCount = DataRepository.GetTotalRegistrations();

			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Index(Registration registration)
		{
			await DataRepository.AddRegistrationAsync(registration);

			ViewBag.RegistrationCount = DataRepository.GetTotalRegistrations();

			return View();
		}
	}
}
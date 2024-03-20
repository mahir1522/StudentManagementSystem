using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using System.Diagnostics;

namespace StudentManagementSystem.Controllers
{
	public class HomeController : Controller
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public HomeController(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		public IActionResult Index()
		{
			var httpContext = _httpContextAccessor.HttpContext;

			if(httpContext != null && httpContext.Request.Cookies["FirstVisit"] == null)
			{
				var welMessage = $"Welcome to your first visit!{DateTime.Now}";

				httpContext.Response.Cookies.Append("FirstVisit", DateTime.Now.ToString(), new CookieOptions
				{
					Expires = DateTime.Now.AddDays(365),
					HttpOnly = true,
				});
				ViewData["welMessage"] = welMessage;
			}
			else
			{
				var firstVisitTime = httpContext != null ? DateTime.Parse(httpContext.Request.Cookies["FirstVisit"]) : DateTime.MinValue;
				var welMessage = $"Welcom back! You first started using the app on  {firstVisitTime}";

				ViewData["welMessage"] = welMessage;
			}

			return View();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
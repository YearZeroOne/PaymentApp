using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentApp.Data;
using PaymentApp.Models;
using PaymentApp.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace PaymentApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly PaymentService _paymentService;
		private readonly IWebHostEnvironment _env;
		private readonly PaymentDbContext _ctx;


		public HomeController(ILogger<HomeController> logger, PaymentService paymentService, IWebHostEnvironment env, PaymentDbContext ctx)
		{
			_logger = logger;
			_paymentService = paymentService;
			_env = env;
			_ctx = ctx;

		}
		[HttpGet]
		public async Task<IActionResult> Test(Class filter)
		{
	
			var vm = new Class1();
			int monthsDifference = 0;

		

			vm.Date = filter;
			IQueryable<Payment> baseQuery = _ctx.Payments.Include(g => g.Member).Where(p => p.InOut == false);


			if (filter.ToDate is null && filter.FromDate is null)
			{
				var fromDate = baseQuery.OrderBy(p => p.PaymentDate).FirstOrDefault();
				var toDate = baseQuery.OrderByDescending(p => p.PaymentDate).FirstOrDefault();
				monthsDifference += ((toDate.PaymentDate.Year - fromDate.PaymentDate.Year) * 12) + toDate.PaymentDate.Month - fromDate.PaymentDate.Month;
			}

			if (filter.FromDate is not null && filter.ToDate is null)
			{
				var toDate = baseQuery.OrderByDescending(p => p.PaymentDate).FirstOrDefault();
				monthsDifference += ((toDate.PaymentDate.Year - filter.FromDate.Value.Year) * 12) + toDate.PaymentDate.Month - filter.FromDate.Value.Month;
			}

			if (filter.ToDate is not null && filter.FromDate is null)
			{
				var fromDate = baseQuery.OrderBy(p => p.PaymentDate).FirstOrDefault();
				monthsDifference += ((filter.ToDate.Value.Year - fromDate.PaymentDate.Year) * 12) + filter.ToDate.Value.Month - fromDate.PaymentDate.Month;
			}

			if (filter.ToDate is not null && filter.FromDate is not null)
			{
				monthsDifference += ((filter.ToDate.Value.Year - filter.FromDate.Value.Year) * 12) + filter.ToDate.Value.Month - filter.FromDate.Value.Month;
			}

			if (filter.FromDate != null)
			{

				baseQuery = baseQuery.Where(g => g.PaymentDate >= filter.FromDate);
			
			}

			if (filter.ToDate != null)
			{
				baseQuery = baseQuery.Where(g => g.PaymentDate <= filter.ToDate);

			}








			var payments = await baseQuery.ToListAsync();

			return View(vm);



		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var dbMembers = await _paymentService.GetMembers();
			var vmList = new List<ListMembersVm>();

			foreach (var member in dbMembers)
			{


				var vm = new ListMembersVm
				{
					Id = member.Id,
					FirstName = member.FirstName,
					LastName = member.LastName,
					Image = member.Image,
					IsActive = member.IsActive,
				};

				vmList.Add(vm);
			}

			return View(vmList);
		}
		[HttpGet]
		public async Task<IActionResult> ListPayments()
		{
			var dbPayments = await _paymentService.GetPayments();
			var vmList = new List<ListPaymentsVm>();

			foreach (var member in dbPayments)
			{
				var vm = new ListPaymentsVm
				{
					Id = member.Id,
					Amount = member.Amount,
					Description = member.Description,
					PaymentTime = member.PaymentDate,
					FirstName = member.Member.FirstName,
					LastName = member.Member.LastName,
					InOut = member.InOut

				};
				vmList.Add(vm);

			}

			return View(vmList);
		}
#nullable enable

		[HttpGet]
		public async Task<IActionResult> Homepage(FilterInfo filter)
		{
			var dbPayment = await _paymentService.GetFilteredPayments(filter);
			var dbMember = await _paymentService.GetMembers();
			var vm = new PaymentOverviewVm();
			vm.Members = new List<MemberVm>();
			vm.Filter = filter;

			var vmList = dbPayment.Select(dbPayment => new PaymentVm
			{
				Id = dbPayment.Id,
				Amount = dbPayment.Amount,
				Description = dbPayment.Description,
				PaymentDate = dbPayment.PaymentDate,
				InOut = dbPayment.InOut,
				FirstName = dbPayment.Member.FirstName,
				LastName = dbPayment.Member.LastName,
				MemberId = dbPayment.MemberId,
				Image = dbPayment.Member.Image

			}
			).ToList();
			vm.Payments = vmList;

			foreach (var item in dbMember)
			{
				vm.Members.Add(new MemberVm
				{
					FirstName = item.FirstName,
					LastName = item.LastName,
					MemberId = item.Id,
				});
			}


			return View(vm);
		}

#nullable disable

		[HttpPost]
		public async Task<IActionResult> EditPayment(string amount, string description, DateTime paymentTime, int memberId, int id, bool inOut)
		{

			string amountString = amount.ToString();
			string newString = amountString.Replace(',', '.');

			decimal decimalFloat = decimal.Parse(newString, CultureInfo.InvariantCulture);
			await _paymentService.EditPayment(decimalFloat, description, paymentTime, memberId, id, inOut);


			return RedirectToAction("ListPayments");
		}



		public async Task<IActionResult> EditMember(int id)
		{
			var dbMember = await _paymentService.GetMember(id);
			var vm = new CreateMemberVm();
			vm.FirstName = dbMember.FirstName;
			vm.LastName = dbMember.LastName;
			vm.Image = dbMember.Image;
			vm.IsActive = dbMember.IsActive;
			vm.Id = id;


			return View(vm);


		}
		[HttpPost]
		public async Task<IActionResult> EditMember(bool isActive, int id)
		{

			await _paymentService.UpdateStatus(isActive, id);


			return RedirectToAction("Index");


		}

		public async Task<IActionResult> EditPayment(int id)
		{
			var dbPayment = await _paymentService.GetPayment(id);
			var dbMembers = await _paymentService.GetMembers();
			var vm = new EditPaymentVm();

			vm.Id = dbPayment.Id;
			vm.Amount = dbPayment.Amount;
			vm.Description = dbPayment.Description;
			vm.PaymentTime = dbPayment.PaymentDate;
			vm.MemberId = dbPayment.MemberId;
			vm.FirstName = dbPayment.Member.FirstName;
			vm.LastName = dbPayment.Member.LastName;
			vm.InOut = dbPayment.InOut;


			vm.Members = dbMembers;




			return View(vm);


		}





		public async Task<IActionResult> DeletePayment(int id)
		{
			await _paymentService.DeletePayment(id);


			return RedirectToAction("ListPayments");
		}

		[HttpGet]
		public async Task<IActionResult> Stat(StatFilterObj statFilter)
		{

			var dbMember = await _paymentService.GetMembers();
			var vm = new StatVm();
			vm.StatFilterObj = statFilter;

			vm.Members = new List<MemberVm>();
			var dbStats = await _paymentService.GetStats(statFilter);
			var topDeposits = await _paymentService.Top5Deposits(statFilter);
			var topPayments = await _paymentService.Top5Payments(statFilter);
			var averageDeposits = await _paymentService.AverageDeposits(statFilter);
			var averagePayments = await _paymentService.AveragePayments(statFilter);

			StatFilterObj overall = new StatFilterObj();

			var dbStatsOverall = await _paymentService.GetStats(overall);
			var topDepositsOverall = await _paymentService.Top5Deposits(overall);
			var topPaymentsOverall = await _paymentService.Top5Payments(overall);
			var averageDepositsOverall = await _paymentService.AverageDeposits(overall);
			var averagePaymentsOverall = await _paymentService.AveragePayments(overall);




			foreach (var item in topDepositsOverall)
			{
				vm.OverallTopDepositMembers.Add(item.Member.LastName + " " + item.Member.FirstName);
				vm.OverallTopDeposits.Add(item.Amount);

			}

			foreach (var item in topPaymentsOverall)
			{
				vm.OverallTopPaymentMembers.Add(item.Member.LastName + " " + item.Member.FirstName);
				vm.OverallTopPayments.Add(item.Amount);
			}



			foreach (var item in topDeposits)
			{
				vm.TopDepositMembers.Add(item.Member.LastName + " " + item.Member.FirstName);
				vm.TopDeposits.Add(item.Amount);

			}

			foreach (var item in topPayments)
			{
				vm.TopPaymentMembers.Add(item.Member.LastName + " " + item.Member.FirstName);
				vm.TopPayments.Add(item.Amount);
			}


			foreach (var item in dbStatsOverall)
			{
				if (item.InOut)
				{
					vm.OverallDeposits += item.Amount;

				}
				else
				{
					vm.OverallPayments += item.Amount;
				}


			}

			foreach (var item in dbStats)
			{
				if (item.InOut)
				{
					vm.Deposits += item.Amount;

				}
				else
				{
					vm.Payments += item.Amount;
				}


			}

			vm.CurrentAmount = Math.Round(vm.Deposits - vm.Payments, 2, MidpointRounding.AwayFromZero);
			vm.AveragePayments = Math.Round(averagePayments, 2, MidpointRounding.AwayFromZero);
			vm.AverageDeposits = Math.Round(averageDeposits, 2, MidpointRounding.AwayFromZero);


			vm.OverallCurrentAmount = Math.Round(vm.OverallDeposits - vm.OverallPayments, 2, MidpointRounding.AwayFromZero);
			vm.OverallAveragePayments = Math.Round(averagePaymentsOverall, 2, MidpointRounding.AwayFromZero);
			vm.OverallAverageDeposits = Math.Round(averageDepositsOverall, 2, MidpointRounding.AwayFromZero);

			foreach (var item in dbMember)
			{
				vm.Members.Add(new MemberVm
				{
					FirstName = item.FirstName,
					LastName = item.LastName,
					MemberId = item.Id,
				});
			}

			return View(vm);
		}

		public IActionResult Privacy()
		{
			return View();
		}


		[HttpGet]
		public IActionResult CreateMember()
		{

			return View();
		}

#nullable enable
		[HttpPost]
		public async Task<IActionResult> CreateMember(string firstName, string lastName, IFormFile? image, bool isActive)
		{
			var rootPath = _env.ContentRootPath;
			string? relativeFilePath = null;

			if (image != null && image.Length > 0)
			{
				relativeFilePath = $"\\MemberImages\\{Path.GetFileName(image.FileName)}";
				var filePath = $"{rootPath}\\wwwroot{relativeFilePath}";

				using (var stream = System.IO.File.Create(filePath))
				{
					await image.CopyToAsync(stream);
				}
			}

			await _paymentService.CreateMember(firstName, lastName, relativeFilePath, isActive);
			return View();
		}
#nullable disable


		[HttpGet]
		public async Task<IActionResult> CreatePayment()
		{
			var dbMembers = await _paymentService.GetMembers();
			var vm = new CreatePaymentVm();
			foreach (var member in dbMembers)
			{
				if (member.IsActive == false)
				{
					continue;
				}
				vm.Members.Add(member);
			}

			return View(vm);
		}


		[HttpPost]
		public async Task<IActionResult> CreatePayment(string amount, DateTime paymentDate, string description, int id, bool inOut)
		{
			string amountString = amount.ToString();
			string newString = amountString.Replace(',', '.');

			decimal decimalFloat = decimal.Parse(newString, CultureInfo.InvariantCulture);
			await _paymentService.CreatePayment(decimalFloat, paymentDate, description, id, inOut);
			return RedirectToAction("CreatePayment");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

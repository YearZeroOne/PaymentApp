using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PaymentApp.Data;
using PaymentApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApp.Services
{
    public class PaymentService
    {
        private readonly PaymentDbContext _ctx;

        public PaymentService(PaymentDbContext ctx)
        {
            _ctx = ctx;
        }


        internal async Task<List<Member>> GetMembers()
        {
            return await _ctx.Members
                .ToListAsync();
        }

        internal async Task<List<Payment>> GetPayments()
        {
            return await _ctx.Payments.Include(x => x.Member)
                .ToListAsync();
        }

        internal async Task<List<Payment>> GetSortedPayments(int? number)
        {
            IQueryable<Payment> query = _ctx.Payments.Include(x => x.Member).OrderByDescending(p => p.Id);

            if (!number.HasValue)
            {
                return await query.ToListAsync();
            }
            else
            {
                return await query.Take(number.Value).ToListAsync();
            }

        }

        internal async Task DeletePayment(int id)
        {
            var payment = await _ctx.Payments.Where(x => x.Id == id).FirstOrDefaultAsync();
            _ctx.Payments.Remove(payment);
            await _ctx.SaveChangesAsync();

        }
        internal async Task EditPayment(decimal amount, string description, DateTime paymentDate, int memberId, int id, bool inOut)
        {
            var payment = await _ctx.Payments.Where(x => x.Id == id).FirstOrDefaultAsync();
            payment.Amount = amount;
            payment.Description = description;
            payment.PaymentDate = paymentDate;
            payment.MemberId = memberId;
            payment.InOut = inOut;
            _ctx.SaveChanges();


            await _ctx.SaveChangesAsync();

        }

        internal async Task<Payment> GetPayment(int id)
        {

            return await _ctx.Payments.Where(x => x.Id == id).Include(y => y.Member).FirstOrDefaultAsync();



        }
#nullable enable
        internal async Task CreateMember(string firstName, string lastName, string? image, bool isActive)
        {
            var newMember = new Member
            {
                FirstName = firstName,
                LastName = lastName,
                Image = image,
                IsActive = isActive
            };
            _ctx.Members.Add(newMember);

            await _ctx.SaveChangesAsync();
        }
#nullable disable

        internal async Task CreatePayment(decimal amount, DateTime paymentDate, string description, int id, bool inOut)
        {
            var newPayment = new Payment
            {
                Amount = amount,
                PaymentDate = paymentDate,
                Description = description,
                MemberId = id,
                InOut = inOut

            };

            _ctx.Payments.Add(newPayment);

            await _ctx.SaveChangesAsync();

        }

        internal async Task<Member> GetMember(int id)
        {
            return await _ctx.Members.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        internal async Task UpdateStatus(bool isActive, int id)
        {
            var memberDb = await _ctx.Members.Where(x => x.Id == id).FirstOrDefaultAsync();
            memberDb.IsActive = isActive;
            await _ctx.SaveChangesAsync();
        }

        internal async Task<List<Payment>> GetFilteredPayments(FilterInfo filter)
        {
            IQueryable<Payment> baseQuery = _ctx.Payments.Include(g => g.Member);


            if (!string.IsNullOrEmpty(filter.Search))
            {
                baseQuery = baseQuery.Where(g => g.Description.Contains(filter.Search) || g.Member.FirstName.Contains(filter.Search));
            }
            if (filter.InOut != null)
            {
                if (filter.InOut == 1)
                {
                    baseQuery = baseQuery.Where(g => g.InOut);
                }
                else
                {
                    baseQuery = baseQuery.Where(g => g.InOut == false);

                }
            }

            if (filter.MemberId != null)
            {
                baseQuery = baseQuery.Where(g => g.MemberId == filter.MemberId);
            }

            baseQuery = baseQuery.OrderByDescending(g => g.Id);

            if (filter.Number.HasValue)
            {
                var result = baseQuery.Take(filter.Number.Value).ToListAsync();
                return await result;
            }
            else
            {
                var result = baseQuery.ToListAsync();
                return await result;
            }

        }
        internal async Task<List<Payment>> Top5Payments(StatFilterObj statFilter)
        {

            IQueryable<Payment> baseQuery = _ctx.Payments.Include(g => g.Member).Where(p => p.InOut == false);
            if (statFilter.MemberId is null)
            {
                if (statFilter.FromDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate >= statFilter.FromDate);
                }

                if (statFilter.ToDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate <= statFilter.ToDate);
                }
            }
            else
            {
                baseQuery = baseQuery.Where(g => g.MemberId == statFilter.MemberId);
                if (statFilter.FromDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate >= statFilter.FromDate);
                }

                if (statFilter.ToDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate <= statFilter.ToDate);
                }
            }

            return await baseQuery.OrderByDescending(g => g.Amount)
                .Take(5).ToListAsync();
        }

        internal async Task<List<Payment>> Top5Deposits(StatFilterObj statFilter)
        {
            IQueryable<Payment> baseQuery = _ctx.Payments.Include(g => g.Member).Where(p => p.InOut); 
            if (statFilter.MemberId is null)
            {
                if (statFilter.FromDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate >= statFilter.FromDate);
                }

                if (statFilter.ToDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate <= statFilter.ToDate);
                }
            }
            else
            {
                baseQuery = baseQuery.Where(g => g.MemberId == statFilter.MemberId);
                if (statFilter.FromDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate >= statFilter.FromDate);
                }

                if (statFilter.ToDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate <= statFilter.ToDate);
                }
            }

            return await baseQuery.OrderByDescending(g => g.Amount).Take(5).ToListAsync();


        }


        internal async Task<List<Payment>> GetStats(StatFilterObj statFilter)
        {
            IQueryable<Payment> baseQuery = _ctx.Payments.Include(g => g.Member);
            if (statFilter.MemberId is null)
            {
                if (statFilter.FromDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate >= statFilter.FromDate);
                }

                if (statFilter.ToDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate <= statFilter.ToDate);
                }
            }
            else
            {
                baseQuery = baseQuery.Where(g => g.MemberId == statFilter.MemberId);
                if (statFilter.FromDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate >= statFilter.FromDate);
                }

                if (statFilter.ToDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate <= statFilter.ToDate);
                }
            }



            return await baseQuery.ToListAsync();


        }

        internal async Task<decimal>AverageDeposits(StatFilterObj statFilter)
        {

            IQueryable<Payment> baseQuery = _ctx.Payments.Include(g => g.Member).Where(x => x.InOut);
            int monthsDifference = 1;



          

            if (statFilter.MemberId is null)
            {
                if (statFilter.FromDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate >= statFilter.FromDate);
                }

                if (statFilter.ToDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate <= statFilter.ToDate);
                }
            }
            else
            {
                baseQuery = baseQuery.Where(g => g.MemberId == statFilter.MemberId);
                if (statFilter.FromDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate >= statFilter.FromDate);
                }

                if (statFilter.ToDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate <= statFilter.ToDate);
                }
            }



            if (statFilter.ToDate is null && statFilter.FromDate is null)
            {
                var fromDate = baseQuery.OrderBy(p => p.PaymentDate).FirstOrDefault();
                var toDate = baseQuery.OrderByDescending(p => p.PaymentDate).FirstOrDefault();
                monthsDifference += ((toDate.PaymentDate.Year - fromDate.PaymentDate.Year) * 12) + toDate.PaymentDate.Month - fromDate.PaymentDate.Month;
            }

            else if (statFilter.FromDate is not null && statFilter.ToDate is null)
            {
                var toDate = baseQuery.OrderByDescending(p => p.PaymentDate).FirstOrDefault();
                monthsDifference += ((toDate.PaymentDate.Year - statFilter.FromDate.Value.Year) * 12) + toDate.PaymentDate.Month - statFilter.FromDate.Value.Month;
            }

            else if (statFilter.ToDate is not null && statFilter.FromDate is null)
            {
                var fromDate = baseQuery.OrderBy(p => p.PaymentDate).FirstOrDefault();
                monthsDifference += ((statFilter.ToDate.Value.Year - fromDate.PaymentDate.Year) * 12) + statFilter.ToDate.Value.Month - fromDate.PaymentDate.Month;
            }

            else if (statFilter.ToDate is not null && statFilter.FromDate is not null)
            {
                monthsDifference += ((statFilter.ToDate.Value.Year - statFilter.FromDate.Value.Year) * 12) + statFilter.ToDate.Value.Month - statFilter.FromDate.Value.Month;
            }
            var payments = await baseQuery.Select(p => p.Amount).SumAsync();
            decimal averageAmount = payments / monthsDifference;

            return averageAmount;
        }

        internal async Task<decimal> AveragePayments(StatFilterObj statFilter)
        {
            IQueryable<Payment> baseQuery = _ctx.Payments.Include(g => g.Member).Where(x => x.InOut == false);
            int monthsDifference = 1;

            if (statFilter.MemberId is null)
            {
                if (statFilter.FromDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate >= statFilter.FromDate);
                }

                if (statFilter.ToDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate <= statFilter.ToDate);
                }
            }
            else
            {
                baseQuery = baseQuery.Where(g => g.MemberId == statFilter.MemberId);
                if (statFilter.FromDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate >= statFilter.FromDate);
                }

                if (statFilter.ToDate != null)
                {
                    baseQuery = baseQuery.Where(g => g.PaymentDate <= statFilter.ToDate);
                }
            }

            if (statFilter.ToDate is null && statFilter.FromDate is null)
            {
                var fromDate = baseQuery.OrderBy(p => p.PaymentDate).FirstOrDefault();
                var toDate = baseQuery.OrderByDescending(p => p.PaymentDate).FirstOrDefault();
                monthsDifference += ((toDate.PaymentDate.Year - fromDate.PaymentDate.Year) * 12) + toDate.PaymentDate.Month - fromDate.PaymentDate.Month;
            }

            else if (statFilter.FromDate is not null && statFilter.ToDate is null)
            {
                var toDate = baseQuery.OrderByDescending(p => p.PaymentDate).FirstOrDefault();
                monthsDifference += ((toDate.PaymentDate.Year - statFilter.FromDate.Value.Year) * 12) + toDate.PaymentDate.Month - statFilter.FromDate.Value.Month;
            }

            else if (statFilter.ToDate is not null && statFilter.FromDate is null)
            {
                var fromDate = baseQuery.OrderBy(p => p.PaymentDate).FirstOrDefault();
                monthsDifference += ((statFilter.ToDate.Value.Year - fromDate.PaymentDate.Year) * 12) + statFilter.ToDate.Value.Month - fromDate.PaymentDate.Month;
            }

            else if (statFilter.ToDate is not null && statFilter.FromDate is not null)
            {
                monthsDifference += ((statFilter.ToDate.Value.Year - statFilter.FromDate.Value.Year) * 12) + statFilter.ToDate.Value.Month - statFilter.FromDate.Value.Month;
            }


            var payments = await baseQuery.Select(p => p.Amount).SumAsync();
            decimal averageAmount = payments / monthsDifference;

            return averageAmount;
        }


    }



}

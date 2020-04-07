    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using userprice.Models;

namespace userprice.Controllers
{
    public class MoneyTransactionsController : Controller
    {
        private readonly BankAppContext _context;
        public MoneyTransactionsController(BankAppContext context)
        {
            _context = context;
        }

        // GET: MoneyTransactions
        public async Task<IActionResult> Index()
        {
            var bankAppContext = _context.MoneyTransactions.Include(m => m.BalanceGr).Include(m => m.BalenceTip).Include(m => m.Opaoperation).Include(m => m.User);
            return View(await bankAppContext.ToListAsync());
        }

        // GET: MoneyTransactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneyTransactions = await _context.MoneyTransactions
                .Include(m => m.BalanceGr)
                .Include(m => m.BalenceTip)
                .Include(m => m.Opaoperation)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MoneyTransactionsId == id);
            if (moneyTransactions == null)
            {
                return NotFound();
            }

            return View(moneyTransactions);
        }

        // GET: MoneyTransactions/Create
        public IActionResult Create()
        {
            ViewData["BalanceGrId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId");
            ViewData["BalenceTipId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId");
            ViewData["OpaoperationId"] = new SelectList(_context.Operation, "OperationId", "OperationName");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: MoneyTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoneyTransactionsId,UserId,AmountMoney,BalenceTipId,BalanceGrId,OpaoperationId,BalanceCont,TrDate")] MoneyTransactions moneyTransactions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moneyTransactions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BalanceGrId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId", moneyTransactions.BalanceGrId);
            ViewData["BalenceTipId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId", moneyTransactions.BalenceTipId);
            ViewData["OpaoperationId"] = new SelectList(_context.Operation, "OperationId", "OperationName", moneyTransactions.OpaoperationId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserLastname", moneyTransactions.UserId);
            return View(moneyTransactions);
        }

        // GET: MoneyTransactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneyTransactions = await _context.MoneyTransactions.FindAsync(id);
            if (moneyTransactions == null)
            {
                return NotFound();
            }
            ViewData["BalanceGrId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId", moneyTransactions.BalanceGrId);
            ViewData["BalenceTipId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId", moneyTransactions.BalenceTipId);
            ViewData["OpaoperationId"] = new SelectList(_context.Operation, "OperationId", "OperationName", moneyTransactions.OpaoperationId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserLastname", moneyTransactions.UserId);
            return View(moneyTransactions);
        }

        // POST: MoneyTransactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MoneyTransactionsId,UserId,AmountMoney,BalenceTipId,BalanceGrId,OpaoperationId,BalanceCont,TrDate")] MoneyTransactions moneyTransactions)
        {
            if (id != moneyTransactions.MoneyTransactionsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moneyTransactions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoneyTransactionsExists(moneyTransactions.MoneyTransactionsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BalanceGrId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId", moneyTransactions.BalanceGrId);
            ViewData["BalenceTipId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId", moneyTransactions.BalenceTipId);
            ViewData["OpaoperationId"] = new SelectList(_context.Operation, "OperationId", "OperationName", moneyTransactions.OpaoperationId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserLastname", moneyTransactions.UserId);
            return View(moneyTransactions);
        }

        // GET: MoneyTransactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneyTransactions = await _context.MoneyTransactions
                .Include(m => m.BalanceGr)
                .Include(m => m.BalenceTip)
                .Include(m => m.Opaoperation)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MoneyTransactionsId == id);
            if (moneyTransactions == null)
            {
                return NotFound();
            }

            return View(moneyTransactions);
        }

        // POST: MoneyTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moneyTransactions = await _context.MoneyTransactions.FindAsync(id);
            _context.MoneyTransactions.Remove(moneyTransactions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
                // GET: MoneyTransactions/Usermoney
        public  IActionResult Usermoney(int? id)
        {

            ViewData["UserId"] = id;
            ViewData["BalanceGrId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId");
            ViewData["BalenceTipId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId");
            ViewData["OpaoperationId"] = new SelectList(_context.Operation, "OperationId", "OperationName");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");
            
            return View();
        }
        // POST: MoneyTransactions/Usermoney
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Usermoney(int id, [Bind("MoneyTransactionsId,UserId,AmountMoney,BalenceTipId,BalanceGrId,OpaoperationId,BalanceCont,TrDate")] MoneyTransactions moneyTransactions)
        {
            //string UserIds = HttpContext.Request.Form["UserId"];
            if (ModelState.IsValid)
            {
                moneyTransactions.UserId = id;
                moneyTransactions.OpaoperationId = 3;
                moneyTransactions.BalanceCont = 1;
                moneyTransactions.BalenceTipId = 1;
                moneyTransactions.BalanceGrId = 2;
                _context.Add(moneyTransactions);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserD", "Users", new { id });
            }
            
            return View(moneyTransactions);
        }



        public IActionResult Moneyuser(int? id)
        {

            ViewData["UserId"] = id;
            ViewData["BalanceGrId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId");
            ViewData["BalenceTipId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId");
            ViewData["OpaoperationId"] = new SelectList(_context.Operation, "OperationId", "OperationName");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");

            return View();
        }
        // POST: MoneyTransactions/Usermoney
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Moneyuser(int id, [Bind("MoneyTransactionsId,UserId,AmountMoney,BalenceTipId,BalanceGrId,OpaoperationId,BalanceCont,TrDate")] MoneyTransactions moneyTransactions)
        {
            //string UserIds = HttpContext.Request.Form["UserId"];
            if (ModelState.IsValid)
            {
                moneyTransactions.UserId = id;
                moneyTransactions.OpaoperationId = 3;
                moneyTransactions.BalanceCont = 1;
                moneyTransactions.BalenceTipId = 2;
                moneyTransactions.BalanceGrId = 1;
                _context.Add(moneyTransactions);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserD", "Users", new { id });
            }

            return View(moneyTransactions);
        }

        public IActionResult pmoney(int? id)
        {

            ViewData["UserId"] = id;
            ViewData["BalanceGrId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId");
            ViewData["BalenceTipId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId");
            ViewData["OpaoperationId"] = new SelectList(_context.Operation, "OperationId", "OperationName");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");

            return View();
        }
        // POST: MoneyTransactions/Usermoney
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> pmoney(int id, [Bind("MoneyTransactionsId,UserId,AmountMoney,BalenceTipId,BalanceGrId,OpaoperationId,BalanceCont,TrDate")] MoneyTransactions moneyTransactions)
        {
            //string UserIds = HttpContext.Request.Form["UserId"];
            if (ModelState.IsValid)
            {
                moneyTransactions.UserId = id;
                moneyTransactions.OpaoperationId = 2;
                moneyTransactions.BalanceCont = 0;
                moneyTransactions.BalenceTipId = 1;
                moneyTransactions.BalanceGrId = 1;
                _context.Add(moneyTransactions);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserD", "Users", new { id });
            }

            return View(moneyTransactions);
        }
        public IActionResult tmoney(int? id)
        {

            ViewData["UserId"] = id;
            ViewData["BalanceGrId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId");
            ViewData["BalenceTipId"] = new SelectList(_context.ExchangeRate, "ExchangeRateId", "ExchangeRateId");
            ViewData["OpaoperationId"] = new SelectList(_context.Operation, "OperationId", "OperationName");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");

            return View();
        }
        // POST: MoneyTransactions/Usermoney
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> tmoney(int id, [Bind("MoneyTransactionsId,UserId,AmountMoney,BalenceTipId,BalanceGrId,OpaoperationId,BalanceCont,TrDate")] MoneyTransactions moneyTransactions)
        {
            //string UserIds = HttpContext.Request.Form["UserId"];
            if (ModelState.IsValid)
            {
                moneyTransactions.UserId = id;
                moneyTransactions.OpaoperationId = 2;
                moneyTransactions.BalanceCont = 0;
                moneyTransactions.BalenceTipId = 2;
                moneyTransactions.BalanceGrId = 2;
                _context.Add(moneyTransactions);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserD", "Users", new { id });
            }

            return View(moneyTransactions);
        }
        private bool MoneyTransactionsExists(int id)
        {
            return _context.MoneyTransactions.Any(e => e.MoneyTransactionsId == id);
        }
    }
}

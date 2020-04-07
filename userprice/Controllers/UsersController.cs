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
    public class UsersController : Controller
    {
        private readonly BankAppContext _context;


        public UsersController(BankAppContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> UserD(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

        
            float tr=0, usd=0, a=0,f;
            var user = await _context.User
            .Include(s => s.MoneyTransactions)
                .ThenInclude(e => e.Opaoperation)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.UserId == id);
            f= (float)6.731;
            foreach (var item in user.MoneyTransactions)
            {
                if (item.BalanceCont == 0)
                {
                    if ((item.OpaoperationId == 2) && (item.BalenceTipId == 1)) { tr += item.AmountMoney; }
                    if ((item.OpaoperationId == 2) && (item.BalenceTipId == 2)) { usd += item.AmountMoney; }
                    if ((item.OpaoperationId == 1) && (item.BalenceTipId == 1)) { tr -= item.AmountMoney; }
                    if ((item.OpaoperationId == 1) && (item.BalenceTipId == 2)) { usd -= item.AmountMoney; }
                }
                else
                {
                    if ((item.OpaoperationId == 3) && (item.BalenceTipId == 1) && (item.BalanceGrId == 2)) 
                    { 
                        tr -= Convert.ToInt32(item.AmountMoney);
                        usd +=  (float)item.AmountMoney / (float)f;
                    }
                    if ((item.OpaoperationId == 3) && (item.BalenceTipId == 2) && (item.BalanceGrId == 1))
                    {
                        usd -= Convert.ToInt32(item.AmountMoney);
                        tr += (float)item.AmountMoney * (float)f;
                    }

                }
                
                a += Convert.ToInt32(item.AmountMoney);
            }

                if (user == null)
            {
                return NotFound();
            }
            ViewData["trmoney"] = tr;
            ViewData["usdmoney"] = usd;
            ViewData["ms1"] = "TL Hesabına Para Yatırma";
            ViewData["ms2"] = "USD Hesabına Para Yatırma";
            ViewData["ms3"] = "TL to USD";
            ViewData["ms4"] = "USD to TL";
            ViewData["ms5"] = "TL Hesabından Para Çekme";
            ViewData["ms6"] = "USD Hesabından Para Çekme";
            return View(user);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // GET: deneme/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: deneme/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,UserLastname,UserPpNo,UserPass")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,UserLastname,UserPpNo,UserPass")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
    }
}

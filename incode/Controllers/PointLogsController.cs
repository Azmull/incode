using incode.Models;
using incode.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace incode.Controllers
{
    public class PointLogsController : Controller
    {
        private readonly incodedatabaseContext _context;

        public PointLogsController(incodedatabaseContext context)
        {
            _context = context;
        }

        // GET: PointLogs
        public async Task<IActionResult> Index()
        {
            var incodedatabaseContext = _context.PointLogs.Include(p => p.PointTypeDefinition).Include(p => p.User);
            return View(await incodedatabaseContext.ToListAsync());
        }

        // GET: PointLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointLog = await _context.PointLogs
                .Include(p => p.PointTypeDefinition)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PointLogId == id);
            if (pointLog == null)
            {
                return NotFound();
            }

            return View(pointLog);
        }

        // GET: PointLogs/Create
        public IActionResult Create()
        {
            ViewData["PointTypeDefinitionId"] = new SelectList(_context.PointTypeDefinitions, "PointTypeDefinitionId", "DisplayName");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname");
            return View();
        }

        // POST: PointLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PointLogId,UserId,SourceType,SourceId,PointTypeDefinitionId,ChangeAmount,Description,CreatedAt")] PointLog pointLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pointLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointTypeDefinitionId"] = new SelectList(_context.PointTypeDefinitions, "PointTypeDefinitionId", "DisplayName", pointLog.PointTypeDefinitionId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", pointLog.UserId);
            return View(pointLog);
        }

        // GET: PointLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointLog = await _context.PointLogs.FindAsync(id);
            if (pointLog == null)
            {
                return NotFound();
            }
            ViewData["PointTypeDefinitionId"] = new SelectList(_context.PointTypeDefinitions, "PointTypeDefinitionId", "DisplayName", pointLog.PointTypeDefinitionId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", pointLog.UserId);
            return View(pointLog);
        }

        // POST: PointLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PointLogId,UserId,SourceType,SourceId,PointTypeDefinitionId,ChangeAmount,Description,CreatedAt")] PointLog pointLog)
        {
            if (id != pointLog.PointLogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pointLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointLogExists(pointLog.PointLogId))
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
            ViewData["PointTypeDefinitionId"] = new SelectList(_context.PointTypeDefinitions, "PointTypeDefinitionId", "DisplayName", pointLog.PointTypeDefinitionId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", pointLog.UserId);
            return View(pointLog);
        }

        // GET: PointLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointLog = await _context.PointLogs
                .Include(p => p.PointTypeDefinition)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PointLogId == id);
            if (pointLog == null)
            {
                return NotFound();
            }

            return View(pointLog);
        }

        // POST: PointLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pointLog = await _context.PointLogs.FindAsync(id);
            if (pointLog != null)
            {
                _context.PointLogs.Remove(pointLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PointLogExists(int id)
        {
            return _context.PointLogs.Any(e => e.PointLogId == id);
        }

        //----------------------------------------------------------------------------
        // GET: point_log/UserDetail   ← 這是給後台看的明細頁
        public async Task<IActionResult> PointLogDetail(int? userId = null)  //目前後台查看任意使用者點數明細 //之後改成登入者 ID
        {

            //載入所有使用者到下拉選單
            var users = await _context.Users
           .Select(u => new { u.UserId, u.Nickname })
           .OrderBy(u => u.UserId)
           .ToListAsync();

            ViewBag.UserList = new SelectList(users, "UserId", "Nickname");
            // ★ 加這行傳總數給 debug 用
            ViewBag.UserCount = users.Count;

            // 如果沒有選擇使用者，預設顯示第一個
            if (!userId.HasValue && users.Any())
            {
                userId = users.First().UserId;
            }

            if (!userId.HasValue || !users.Any())
            {
                return View(new List<PointLogViewModel>());
            }

            // 取出該使用者的所有點數異動紀錄（舊 → 新）
            var logs = await _context.PointLogs
                .Include(l => l.PointTypeDefinition)
                .Where(l => l.UserId == userId.Value)
                .OrderBy(l => l.CreatedAt)
                .ToListAsync();

            if (!logs.Any())
            {
                ViewBag.SelectedUserId = userId.Value;
                ViewBag.CurrentTotalPoints = 0;
                return View(new List<PointLogViewModel>());
            }

            // 收集各來源類型的 ID
            var missionIds = logs.Where(l => l.SourceType == "mission").Select(l => l.SourceId).Distinct().ToList();
            var entrustMissionIds = logs.Where(l => l.SourceType == "entrust_mission").Select(l => l.SourceId).Distinct().ToList();
            var purchaseLogIds = logs.Where(l => l.SourceType == "point_purchase_log").Select(l => l.SourceId).Distinct().ToList();
            var orderIds = logs.Where(l => l.SourceType == "order").Select(l => l.SourceId).Distinct().ToList();

            // 批量載入相關資料
            var missions = await _context.Missions
                .Where(m => missionIds.Contains(m.MissionId))
                .ToDictionaryAsync(m => m.MissionId, m => m.MissionName ?? "未知任務");

            var entrustMissions = await _context.EntrustMissions
                .Where(em => entrustMissionIds.Contains(em.EntrustMissionId))
                .ToDictionaryAsync(em => em.EntrustMissionId, em => em.Title ?? "未知委託");

            var purchaseLogs = await _context.PointPurchaseLogs
                .Where(p => purchaseLogIds.Contains(p.PointPurchaseLogId))
                .ToDictionaryAsync(p => p.PointPurchaseLogId, p => p.OrderNo ?? "未知訂單");

            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Where(o => orderIds.Contains(o.OrderId))
                .ToDictionaryAsync(o => o.OrderId, o => o);

            // 建立 ViewModel
            var viewModels = new List<PointLogViewModel>();
            int runningBalance = 0;

            foreach (var log in logs)
            {
                runningBalance += log.ChangeAmount;

                string description = log.SourceType switch
                {
                    "mission" => missions.GetValueOrDefault(log.SourceId, "未知任務"),

                    "entrust_mission" => entrustMissions.GetValueOrDefault(log.SourceId, "未知委託"),

                    "point_purchase_log" => purchaseLogs.TryGetValue(log.SourceId, out var orderNo)
                        ? $"儲值訂單 {orderNo}"
                        : "未知儲值",

                    _ => string.IsNullOrEmpty(log.SourceType) ? "系統異動" : log.SourceType
                };

                viewModels.Add(new PointLogViewModel
                {
                    CreatedAt = log.CreatedAt ?? DateTime.Now,
                    ChangeAmount = log.ChangeAmount,
                    Description = description,
                    NewBalance = runningBalance,
                    TypeDisplayName = log.PointTypeDefinition?.DisplayName ?? "未知類型"
                });
            }

            // 最新紀錄顯示在上
            viewModels.Reverse();

            ViewBag.CurrentTotalPoints = runningBalance;
            ViewBag.SelectedUserId = userId.Value;

            return View(viewModels);
        }

        // 輔助方法：美化點數商店訂單顯示文字
        private string GetOrderDisplayText(Order order)
        {
            if (order?.OrderDetails == null || !order.OrderDetails.Any())
                return $"點數商店消費（訂單 #{order?.OrderId})";

            var details = order.OrderDetails;
            var firstProductName = details.First().Product?.Name ?? "商品";

            if (details.Count == 1)
            {
                var qty = details.First().Quantity;
                return qty == 1
                    ? $"購買 {firstProductName}（訂單 #{order.OrderId}）"
                    : $"購買 {firstProductName} × {qty}（訂單 #{order.OrderId}）";
            }

            // 多項商品
            var totalItems = details.Sum(d => d.Quantity);
            return $"購買 {firstProductName} 等 {totalItems} 件（訂單 #{order.OrderId}）";
        }
    }


}




using Intergeration_With_Agora.Constants;
using Intergeration_With_Agora.DTOs;
using Intergeration_With_Agora.Models;
using Intergeration_With_Agora.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Intergeration_With_Agora.Controllers
{
    public class LiveController(IAgoraService agoraService,AppDbContext dbContext,IOptions<IgoreSettings> options) : Controller
    {

        [HttpPost]
        public IActionResult StartSession()
        {
            var channelName = $"test-channel";

            var token = agoraService.GenerateRtcToken(channelName, "INST-123-ABC");

            return Json(new
            {
                appId = options.Value.AppId,
                channelName,
                token
            });
        }
        [HttpGet]
        public IActionResult Create(Guid courseId)
        {
            ViewBag.CourseId = courseId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSession( SessionRequest dto)
        {
            var courseExists = await dbContext.Courses.AnyAsync(c => c.Id == dto.CourseId);
            if (!courseExists) return BadRequest("الـ CourseId ده مش موجود في الداتا بيز");



            var session = new LiveSession
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                CourseId = dto.CourseId,
                InstructorId = "INST-123-ABC",
                ScheduledStart = dto.ScheduledStart,
                Duration = dto.DurationInMinutes,
                Status = LiveSessionStatus.Scheduled,
                CreatedAt = DateTime.UtcNow,
                ChannelName = $"ch_{Guid.NewGuid().ToString().Substring(0, 8)}"
            };
            dbContext.LiveSessions.Add(session);
            var result = dbContext.SaveChanges();
            if(result > 0)
                return RedirectToAction("Room", new { id = session.Id });
            else
                return StatusCode(500, "حصلت مشكلة أثناء إنشاء الجلسة");

        }
        [HttpPost]
        public async Task<IActionResult> LeaveSession(Guid sessionId)
        {
            var session = await dbContext.LiveSessions.FindAsync(sessionId);

            if (session == null)
            {
                return NotFound("الجلسة غير موجودة");
            }

            session.Status = LiveSessionStatus.Completed; 

            await dbContext.SaveChangesAsync();

            // 4. توجيه المدرس لصفحة تحتوي على ملخص أو قائمة الحصص
            // هنا بنرجعه لصفحة الـ Index الخاصة بالكورس اللي لسه مخلص الحصة فيه
            return RedirectToAction("Index", "Home");
            // أو RedirectToAction("Details", "Courses", new { id = session.CourseId });
        }
        [HttpGet]
        public async Task<IActionResult> Room(Guid id)
        {
            var session = await dbContext.LiveSessions.FindAsync(id);
            if (session == null) return NotFound();

            if (session.Status == LiveSessionStatus.Scheduled)
            {
                session.Status = LiveSessionStatus.Live;
                await dbContext.SaveChangesAsync();
            }

            // حالياً هنعمل Comment لسطر الـ Recording عشان AWS مش معاكي
            // _ = Task.Run(async () => await StartAutomaticRecording(session.Id, session.ChannelName));

            var userId = "INST-123-ABC"; // رقم تعريفي للمدرس
            var token = agoraService.GenerateRtcToken(session.ChannelName, userId);

            ViewBag.Token = token;
            ViewBag.ChannelName = session.ChannelName;
            ViewBag.AppId = options.Value.AppId;
            ViewBag.UserId = userId;

            return View(session);
        }

        [HttpGet]
        public async Task<IActionResult> Recordings(Guid courseId)
        {
            var recordings = await dbContext.SessionRecordings
                .Where(r => r.Session.CourseId == courseId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return View(recordings);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

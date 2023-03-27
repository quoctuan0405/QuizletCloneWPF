using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizletClone.API.Presenter;
using Server.Model;
using QuizletClone.API.Payload;
using Server.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/set")]
    [ApiController]
    public class SetController : ControllerBase
    {
        private readonly SetService _setService;
        private readonly UserService _userService;

        public SetController(SetService setService, UserService userService)
        {
            _setService = setService;
            _userService = userService;
        }

        // GET: api/set
        [HttpGet]
        public async Task<IEnumerable<SetPresenter>> Get(string? keyword)
        {
            IEnumerable<Set> sets = await _setService.GetList(keyword);

            List<SetPresenter> setPresenters = new List<SetPresenter>();
            foreach (var set in sets)
            {
                setPresenters.Add(new SetPresenter()
                {
                    Id = set.Id,
                    Name = set.Name,
                    TermCount = set.Terms.Count(),
                    Author = new UserPresenter()
                    {
                        Id = set.Author.Id,
                        Username = set.Author.UserName
                    }
                });
            }

            return setPresenters;
        }

        // GET: api/set/my
        [Authorize]
        [HttpGet("my")]
        public async Task<IEnumerable<SetPresenter>> GetMySets(string? keyword)
        {
            // Get user
            var username = User.Identity.Name;
            ApplicationUser user = await _userService.GetByUsername(username);

            // Get my set
            IEnumerable<Set> sets = await _setService.GetMySets(user.Id, keyword);

            List<SetPresenter> setPresenters = new List<SetPresenter>();
            foreach (var set in sets)
            {
                setPresenters.Add(new SetPresenter()
                {
                    Id = set.Id,
                    Name = set.Name,
                    TermCount = set.Terms.Count(),
                    Author = new UserPresenter()
                    {
                        Id = set.Author.Id,
                        Username = set.Author.UserName
                    }
                });
            }

            return setPresenters;
        }

        // GET api/set/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("Must provide an id");
            }

            Set set = await _setService.Get(id);

            SetDetailPresenter setDetailPresenter = new SetDetailPresenter()
            {
                Id = set.Id,
                Name = set.Name,
                Author = new UserPresenter()
                {
                    Id = set.Author.Id,
                    Username = set.Author.UserName
                },
            };

            List<TermPresenter> termPresenters = new List<TermPresenter>();
            foreach (Term term in set.Terms)
            {
                termPresenters.Add(new TermPresenter()
                {
                    Id = term.Id,
                    Answer = term.Answer,
                    Explanation = term.Explanation,
                    Question = term.Question,
                });
            }

            setDetailPresenter.Terms = termPresenters;

            return Ok(setDetailPresenter);
        }

        // POST api/set
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SetPayload payload)
        {
            // Get user
            var username = User.Identity.Name;
            ApplicationUser user = await _userService.GetByUsername(username);

            // Create new set
            Set set = new Set()
            {
                Name = payload.Name,
                AuthorId = user.Id
            };

            List<Term> terms = new List<Term>();

            foreach(TermPayload term in payload.Terms)
            {
                terms.Add(new Term()
                {
                    Question = term.Question,
                    Answer = term.Answer,
                    Explanation = term.Explanation,
                });
            }

            set.Terms = terms;

            await _setService.Create(set);

            // Return new set
            set = await _setService.Get(set.Id);

            SetDetailPresenter setDetailPresenter = new SetDetailPresenter()
            {
                Id = set.Id,
                Name = set.Name,
                Author = new UserPresenter()
                {
                    Id = set.Author.Id,
                    Username = set.Author.UserName
                },
            };

            List<TermPresenter> termPresenters = new List<TermPresenter>();
            foreach (Term term in set.Terms)
            {
                termPresenters.Add(new TermPresenter()
                {
                    Id = term.Id,
                    Answer = term.Answer,
                    Explanation = term.Explanation,
                    Question = term.Question,
                });
            }

            setDetailPresenter.Terms = termPresenters;

            return Ok(setDetailPresenter);
        }

        // PUT api/set/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] SetPayload payload)
        {
            // Get user
            var username = User.Identity.Name;
            ApplicationUser user = await _userService.GetByUsername(username);

            // Find set
            Set set = await _setService.Get(id);
            if (set.AuthorId != user.Id)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Forbidden) { ReasonPhrase = "Forbidden" };
                throw new System.Web.Http.HttpResponseException(msg);
            }

            // Update set
            set.Name = payload.Name;

            List<Term> terms = new List<Term>();

            foreach (TermPayload term in payload.Terms)
            {
                terms.Add(new Term()
                {
                    Id = term.Id,
                    Question = term.Question,
                    Answer = term.Answer,
                    Explanation = term.Explanation,
                });
            }

            set.Terms = terms;

            await _setService.Update(id, set);
        }

        // DELETE api/set/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task Delete(int id)
        {
            await _setService.Delete(id);
        }

        // GET: api/set/5/learning/term
        [Authorize]
        [HttpGet("{setId}/learning/term")]
        public async Task<TermPresenter> GetLearningTerm(int setId)
        {
            // Get user
            var username = User.Identity.Name;
            ApplicationUser user = await _userService.GetByUsername(username);

            var term = await _setService.GetRandomLearningTerm(setId, user.Id);

            if (term == null)
            {
                return null;
            }

            TermPresenter termPresenter = new TermPresenter()
            {
                Id = term.Id,
                Answer = term.Answer,
                Question = term.Question,
                Explanation = term.Explanation,
                Choices = term.Choices,
            };

            return termPresenter;
        }

        // POST api/set/5/learning/report
        [HttpPost("{id}/learning/report")]
        [Authorize]
        public async Task ReportLearningProgress([FromBody] ReportTermPayload payload)
        {
            // Get user
            var username = User.Identity.Name;
            ApplicationUser user = await _userService.GetByUsername(username);

            // Report
            await _setService.ReportLearningProgress(payload.TermId, user.Id, payload.Correct);
        }

        // GET api/set/5/learning/term/progress
        [Authorize]
        [HttpGet("{setId}/learning/term/progress")]
        public async Task<CountLearningTermPresenter> CountLearningTermProgress(int setId)
        {
            // Get user
            var username = User.Identity.Name;
            ApplicationUser user = await _userService.GetByUsername(username);

            // Count
            int count = await _setService.CountLearningTermProgress(setId, user.Id);

            return new CountLearningTermPresenter()
            {
                Count = count
            };
        }
    }
}

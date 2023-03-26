using Newtonsoft.Json;
using QuizletClone.API.Payload;
using QuizletClone.API.Presenter;
using QuizletClone.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.WPF.State
{
    public class Store
    {
        // Service
        private readonly SetService _setService;
        private readonly UserService _userService;
        private readonly ExcelService _excelService;

        // State
        public string Keyword { get; set; }
        public UserPresenter Me { get; set; }
        public List<TermPayload> ImportedTermPayloads { get; set; } = new List<TermPayload>();
        public List<SetPresenter> Sets { get; set; } = new List<SetPresenter>();
        public List<SetPresenter> MySets { get; set; } = new List<SetPresenter>();
        public SetDetailPresenter SetDetail { get; set; }
        public TermPresenter? RandomLearningTerm { get; set; }
        public CountLearningTermPresenter CountLearningTerm { get; set; }

        private bool _isAnswered;
        public bool IsAnswered { 
            get
            {
                return _isAnswered;
            }
            set
            {
                _isAnswered = value;
                IsAnsweredChanged?.Invoke();
            } 
        }

        // Action
        public event Action ListSetChanged;

        public event Action ListMySetChanged;

        public event Action SetDetailChanged;

        public event Action MeChanged;

        public event Action RandomLearningTermChanged;

        public event Action CountLearningTermChanged;

        public event Action IsAnsweredChanged;

        public event Action ImportedTermPayloadsChanged;

        public Store(SetService setService, UserService userService, ExcelService excelService)
        {
            _setService = setService;
            _userService = userService;
            _excelService = excelService;
        }

        public async Task<TokenPresenter> Login(string username, string password)
        {
            LoginPayload payload = new LoginPayload()
            {
                Username = username,
                Password = password
            };

            return await _userService.Login(payload);
        }

        public async Task<TokenPresenter> Signup(string username, string password)
        {
            SignupPayload payload = new SignupPayload()
            {
                Username = username,
                Password = password
            };

            return await _userService.Signup(payload);
        }

        public async Task GetMe()
        {
            Me = await _userService.GetMe();

            MeChanged?.Invoke();
        }

        public async Task CreateNewSet(SetPayload setPayload)
        {
            SetDetail = await _setService.Create(setPayload);
        }

        public async Task UpdateSet(int id, SetPayload setPayload)
        {
            await _setService.Update(id, setPayload);
        }

        public async Task DeleteSet(int id)
        {
            await _setService.Delete(id);
        }

        public async Task FetchListSet(string? keyword)
        {
            await _setService.GetListSet(keyword).ContinueWith(task => {
                if (task.Result == null)
                {
                    Sets = new List<SetPresenter>();
                }
                else
                {
                    Sets = task.Result;
                }

                ListSetChanged?.Invoke();
            });
        }

        public async Task FetchListMySet(string? keyword)
        {
            await _setService.GetMySets(keyword).ContinueWith(task => {
                if (task.Result == null)
                {
                    MySets = new List<SetPresenter>();
                }
                else
                {
                    MySets = task.Result;
                }

                ListMySetChanged?.Invoke();
            });
        }

        public void GetDetailSet(int setId)
        {
            _setService.GetDetailSet(setId).ContinueWith(task =>
            {
                SetDetail = task.Result;

                SetDetailChanged?.Invoke();
            });
        }

        public async void GetRandomLearningTerm(int setId)
        {
            await _setService.GetRandomLearningTerm(setId).ContinueWith(task =>
            {
                RandomLearningTerm = task.Result;

                RandomLearningTermChanged?.Invoke();
            });
        }

        public async void CountLearningTermProgress(int setId)
        {
            await _setService.CountLearningTermProgress(setId).ContinueWith(task =>
            {
                CountLearningTerm = task.Result;

                CountLearningTermChanged?.Invoke();
            });
        }

        public async Task ReportLearningProgress(int setId, int termId, bool correct)
        {
            ReportTermPayload payload = new ReportTermPayload()
            {
                TermId = termId,
                Correct = correct
            };

            await _setService.ReportLearningProgress(setId, payload);
        }

        public async Task ReadTermPayloadFromFile(string filePath)
        {
            ImportedTermPayloads = await _excelService.ReadTermPayloadFromFile(filePath);

            ImportedTermPayloadsChanged?.Invoke();
        }
    }
}

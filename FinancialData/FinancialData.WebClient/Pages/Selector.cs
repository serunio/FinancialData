using FinancialData.Shared.DTOs;
using FinancialData.Shared.Models;
using System.Text;
using FinancialData.WebClient.Services;
using Microsoft.AspNetCore.Components;

namespace FinancialData.WebClient.Pages
{
    public abstract class Selector : ComponentBase
    {
        [Parameter] public Viewer? PageRef { get; set; }
        [Inject] public DataParamsService DataParamsService { get; set; } = null!;
        internal SelectionResult SelectionResult = new();
        public SelectionResult GetParams()
        {
            var sb = new StringBuilder();
            foreach (var d in DataParams)
            {
                sb.Append(d.Value[ParamsIds[d.Key]] + " ");
            }
            SelectionResult = new SelectionResult
            {
                DataTypeId = ParamsIds[typeof(DataType)],
                FrequencyId = ParamsIds[typeof(Frequency)],
                PresentationTypeId = ParamsIds[typeof(PresentationType)],
                Caption = sb.ToString()
            };
            return SelectionResult;
        }

        public bool Ready => ParamsIds.Values.All(x => x != 0);

        internal Dictionary<Type, Dictionary<int, string>> DataParams = new()
    {
        { typeof(DataType), new Dictionary<int, string> { { 0, "Loading..." } } },
        { typeof(Frequency), new Dictionary<int, string> { { 0, "Loading..." } } },
        { typeof(PresentationType), new Dictionary<int, string> { { 0, "Loading..." } } }
    };

        internal Dictionary<Type, int> ParamsIds = new()
    {
        { typeof(DataType), 0 },
        { typeof(Frequency), 0 },
        { typeof(PresentationType), 0 }
    };

        internal Dictionary<Type, string> DefaultValues = new()
    {
        { typeof(DataType), "Wybierz dane" },
        { typeof(Frequency), "Wybierz częstotliwość" },
        { typeof(PresentationType), "Wybierz sposób prezentacji" }
    };

        protected override async Task OnInitializedAsync()
        {
            await LoadDataParamsAsync();
        }

        internal async Task LoadDataParamsAsync()
        {
            //DataParams.Clear();
            await LoadDictionaryAsync<DataType>();
            await LoadDictionaryAsync<Frequency>();
            await LoadDictionaryAsync<PresentationType>();
        }

        internal async Task LoadDictionaryAsync<T>(int DTID = 0, int FID = 0, int PTID = 0) where T : class, IParam
        {
            var data = await DataParamsService.GetDictAsync<T>(DTID, FID, PTID);
            data.TryAdd(0, DefaultValues[typeof(T)]);
            DataParams[typeof(T)] = data;
            if (!data.ContainsKey(ParamsIds[typeof(T)]))
                ParamsIds[typeof(T)] = 0;
        }

        internal abstract Task UpdateSelection(Type key, int id);

        internal string HideOverflow(string s)
        {
            const int maxLength = 29;
            if (s.Length > maxLength)
                s = s[..(maxLength - 3)] + "...";
            return s;
        }
    }
}

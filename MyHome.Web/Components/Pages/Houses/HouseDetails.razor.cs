using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyHome.Web.Data;
using MyHome.Web.Services;

namespace MyHome.Web.Components.Pages.Houses
{
    public partial class HouseDetails
    {
        Data.House? _house;
        Data.Family? _family;
        const string PopoverDelete = "popover-house-delete";

        [Inject] public NavigationManager NavigationManager { get; set; } = null!;
        [Inject] public IJSRuntime JsRuntime { get; set; } = null!;
        [Inject] public IHouseService HouseService { get; set; } = null!;
        [Inject] public IFamilyService FamilyService { get; set; } = null!;
        [Parameter] public string? Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (string.IsNullOrEmpty(Id)) return;
            _house = await HouseService.GetHouseAndReadingsAsync(Id);
            if (_house is null) return;
            _family = await FamilyService.GetFamily(_house.FamilyId);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                foreach (var readingType in _house.MeterReadings.Select(r => r.ReadingType).Distinct())
                {
                    RenderChart(readingType);
                }
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task RenderChart(MeterReadingType readingType)
        {
            var data = _house.MeterReadings
                        .Where(r => r.ReadingType.Equals(readingType))
                        .Select(r => new { Year = r.Year, Value = r.Value });
            await JsRuntime.InvokeVoidAsync("ShowChart", $"chart-{readingType.Name}", "line", data, readingType.Name);
        }

        private async Task DeleteAsync()
        {
            await HouseService.DeleteAsync(_house.Id);
            _family.Houses.Remove(_house);
            NavigationManager.NavigateTo("/Houses");
        }

        private async Task CloseAsync()
        {
            await JsRuntime.InvokeVoidAsync("HidePopover", PopoverDelete);
        }

        private void MeterReadingCreated(Data.MeterReading model)
        {
            if (model is not null)
            {
                _house.MeterReadings.Add(model);
                _ = RenderChart(model.ReadingType);
            }
        }
    }
}

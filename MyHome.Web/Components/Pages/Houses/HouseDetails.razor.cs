using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
            }
        }
    }
}

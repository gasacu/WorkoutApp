using Microsoft.AspNetCore.Components;

namespace WorkoutApp.Components.Pages
{
    public partial class DeleteConfirmationDialog : ComponentBase
    {
        [Parameter]
        public EventCallback<bool> OnConfirm { get; set; }

        private bool IsVisible { get; set; } = false;

        public void Show()
        {
            IsVisible = true;
            StateHasChanged();
        }

        private void CloseModal()
        {
            IsVisible = false;
            StateHasChanged();
        }

        private async Task ConfirmDelete()
        {
            await OnConfirm.InvokeAsync(true);
            CloseModal();
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WorkoutApp.Components.Pages
{
    public partial class Counter : ComponentBase
    {

        DateTime? value;

        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount += 2;
        }
    }
    
}

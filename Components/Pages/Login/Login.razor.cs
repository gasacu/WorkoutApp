
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WorkoutApp.Authorization;
using WorkoutApp.Context;
using WorkoutApp.DTOs;


namespace WorkoutApp.Components.Pages.Login
{
	public partial class Login : ComponentBase
	{

		[SupplyParameterFromForm]
		UserDto userDto { get; set; } = new UserDto();

		[Inject]
		WorkoutAppContext context { get; set; }

		[Inject]
		private NavigationManager NavigationManager { get; set; }

		[Inject]
		public IAuthorizationService AuthorizationService { get; set; }

		[Required]
		[PasswordPropertyText]
		public string? Password { get; set; }

		public Boolean FailedLogin = false;

		public void TryLogin()
		{
			try
			{
				AuthorizationService.Login(Password, userDto.Email);
                NavigationManager.NavigateTo("/", true);
			}
			catch (Exception e)
			{
				{
					Console.WriteLine(e);
				}
			}
		}

	}
}

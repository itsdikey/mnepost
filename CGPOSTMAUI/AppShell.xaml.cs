using CGPost.Views;
using DKIMVVM;
using System.Diagnostics;
using System.Windows.Input;

namespace CGPOSTMAUI;

public partial class AppShell : Shell, IPage
{

	public ICommand BackButtonCommand { get; private set; }

	public AppShell()
	{
        BackButtonCommand = new ActionCommand((_) =>
        {
            Debug.WriteLine("GOO BACK");
        });
        InitializeComponent();
	}
}
